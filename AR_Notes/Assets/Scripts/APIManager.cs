using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;
using Vuforia;
using TMPro;

/// <summary>
/// This class manages the connection with the server and implements RESTful methods to call the APIs.
/// </summary>
public class APIManager : MonoBehaviour
{

    // uncomment this if you are working on the same machine where the server is hosted
    private string server = "localhost";

    // uncomment this if you are working on a client that is not the server host
    //private string server = "192.168.43.225";

    private string port = "8080";
    public Task[] currentTasks;
    public User[]  currentUsers;
    public GameObject[] taskObjects; 
    public GameObject taskPrefab;
    private float refreshRate = 5.0f;
    Thread thread;

    /// <summary>
    /// Setup a scheduler that fetches the Tasks after 0.5 seconds and refresh them every 5 seconds.
    /// </summary>
    void Start()
    {
        InvokeRepeating("UpdateAndRegenerateTasks", 0.5f, refreshRate);   
    }

    /// <summary>
    /// Can be used for realtime refresh from the server, NOT SAFE for the actual system, calls the function EVERY frame
    /// </summary>
    void Update()
    {
        // uncomment to start polling the server
        // UpdateCurrentState(); 
    }

    /// <summary>
    /// Refresh the state of the system by fetching Users, Tasks and regenerating the objects
    /// </summary>
    private void UpdateAndRegenerateTasks() {
        //thread = new Thread(UpdateCurrentState);
        //thread.Start();
        UpdateCurrentState();
        RegenerateTasks();
  
    }

    /// <summary>
    /// Regenerates all the Task game objects using the Tasks data retrieved from the server
    /// </summary>
    public void RegenerateTasks() {
        //taskObjects = new GameObject[currentTasks.Length];
        for(int i=0; i< currentTasks.Length; i++)
        {
            GameObject task = taskObjects[i];
            task.GetComponent<TaskFieldsManager>().SetFields(currentTasks[i], currentTasks[i].title, currentTasks[i].description, returnUserName(currentTasks[i].userId));
        }
    }

    /// <summary>
    /// Fetches all the Users and all the Tasks from the server
    /// </summary>
    public void UpdateCurrentState() {
        currentUsers = GetUsers();
        currentTasks = GetTasks(); 
    }

    /// <summary>
    /// This method was used to assign procedurally Tasks to augmented ImageTargets, every frame.
    /// This method is DEPRECATED because inefficient.
    /// </summary>
    public void SetupTrackables() 
    {
        IEnumerable<TrackableBehaviour> trackableBehaviours = TrackerManager.Instance.GetStateManager().GetActiveTrackableBehaviours();
 
		// Loop over all TrackableBehaviours.
        int iterator = 0;
		foreach (TrackableBehaviour trackableBehaviour in trackableBehaviours)
		{
            taskObjects[iterator].transform.parent = trackableBehaviour.transform;
			string name = trackableBehaviour.TrackableName;
			Debug.Log ("Trackable name: " + name);
            taskObjects[iterator].transform.localPosition = new Vector3(0,0.2f,0);
			taskObjects[iterator].transform.localScale = new Vector3(1f, 1f, 1f);
			taskObjects[iterator].transform.localRotation = Quaternion.identity;
            iterator++;
		}
    }

    /// <summary>
    /// Send a GET request to retrieve all the Tasks
    /// </summary>
    /// <returns>list of the Tasks</returns>
    private Task[] GetTasks()
    {
        Task[] tasks = {};
        try {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(String.Format("http://{0}:{1}/tasks/all",  server, port));
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string jsonResponse = fixJson(reader.ReadToEnd());
            Debug.Log("Task Response" + jsonResponse);
            tasks = JsonHelper.FromJson<Task>(jsonResponse);
        } catch (Exception exception) {
            Debug.LogError("Couldn't load tasks due to the following exception:" + exception);
        }
        return tasks;
    }

    /// <summary>
    /// Send a GET request to retrieve all the Users
    /// </summary>
    /// <returns>list of the Users</returns>
    private User[] GetUsers()
    {
        User[] users = {};
        try {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(String.Format("http://{0}:{1}/users/all",  server, port));
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(String.Format("http://api.openweathermap.org/data/2.5/weather?id={0}&APPID={1}", 
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string jsonResponse = fixJson(reader.ReadToEnd());
            Debug.Log("User Response" + jsonResponse);
            users = JsonHelper.FromJson<User>(jsonResponse);
        } catch (Exception exception) {
            Debug.LogError("Couldn't load users due to the following exception:" + exception);
        }
        return users;
    }

    /// <summary>
    /// Send a POST request to update a Task for a specific User, then updates the state of the system
    /// </summary>
    /// <param name="task">The Task body</param>
    /// <param name="userId">User Id</param>
    /// <returns>A response</returns>
    public IEnumerator UpdateTaskForUser(Task task, long userId)
    {
        var uwr = new UnityWebRequest(String.Format("http://{0}:{1}/users/{2}/tasks/update/{3}",  server, port, userId, task.id), "PUT");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes( JsonUtility.ToJson(task));
        uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");
        //Send the request then wait here until it returns
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
            UpdateAndRegenerateTasks();  
        }
    }

    /// <summary>
    /// Send a POST request to update a Task, then updates the state of the system
    /// </summary>
    /// <param name="task">The Task body</param>
    /// <returns></returns>
    public IEnumerator UpdateTask(Task task)
    {
        var uwr = new UnityWebRequest(String.Format("http://{0}:{1}/tasks/update/{2}",  server, port, task.id), "PUT");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes( JsonUtility.ToJson(task));
        uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");
        //Send the request then wait here until it returns
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
            UpdateAndRegenerateTasks(); 
        }
    }

    /// <summary>
    /// Send a POST request to clear a Task, then updates the state of the system
    /// </summary>
    /// <param name="task">The Task body</param>
    /// <returns></returns>
    public IEnumerator ClearTask(Task task)
    {
        var uwr = new UnityWebRequest(String.Format("http://{0}:{1}/tasks/clear/{2}",  server, port, task.id), "PUT");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes( JsonUtility.ToJson(task));
        uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");
        //Send the request then wait here until it returns
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
            UpdateAndRegenerateTasks(); 
        }
    }

    /// <summary>
    /// Support method to parse a list of JSON objects
    /// </summary>
    /// <param name="value">JSON string</param>
    /// <returns>A parsed JSON string</returns>
    string fixJson(string value)
    {
        value = "{\"Items\":" + value + "}";
        return value;
    }

    /// <summary>
    /// Return the username of an user using its id
    /// </summary>
    /// <param name="id">The id of the User</param>
    /// <returns>The username of the User</returns>
    public string returnUserName(long id) {
        for(int i=0; i< currentUsers.Length; i++) {
            if(id == currentUsers[i].id) {
                return currentUsers[i].username;
            }
        }
        return "(not assigned)";
    }
}
