using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using System.Net;
using Vuforia;
using TMPro;

public class APIManager : MonoBehaviour
{

    // uncomment this if you are working on the same machine where the server is hosted
    private string server = "localhost";

    // uncomment this if you are working on a client that is not the server host
    // private string server = "192.168.43.225";

    private string port = "8080";
    public Task[] currentTasks;
    public User[]  currentUsers;
    public GameObject[] taskObjects; 
    public GameObject taskPrefab;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateAndRegenerateTasks", 0.5f, 5.0f);   
    }

    // Update is called once per frame
    void Update()
    {
        // uncomment to start polling the server
        // UpdateCurrentState(); 
    }

    private void UpdateAndRegenerateTasks() {
        UpdateCurrentState();   
        RegenerateTasks();
    }

    public void RegenerateTasks() {
        //taskObjects = new GameObject[currentTasks.Length];
        for(int i=0; i< currentTasks.Length; i++)
        {
            //GameObject task = Instantiate(taskPrefab, new Vector3(i * 0.32f,0,0), Quaternion.identity) as GameObject;
            GameObject task = taskObjects[i];
            task.GetComponent<TaskFieldsManager>().SetFields(currentTasks[i], currentTasks[i].title, currentTasks[i].description, returnUserName(currentTasks[i].userId));
            //taskObjects[i] = task;
        }
    }

    public void UpdateCurrentState() {
        currentUsers = GetUsers();
        currentTasks = GetTasks(); 
    }

    // this method isn't use anymore because inefficient
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

    private Task[] GetTasks()
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(String.Format("http://{0}:{1}/tasks/all",  server, port));
        //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(String.Format("http://api.openweathermap.org/data/2.5/weather?id={0}&APPID={1}", 
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string jsonResponse = fixJson(reader.ReadToEnd());
        Debug.Log("Task Response" + jsonResponse);
        Task[] tasks = JsonHelper.FromJson<Task>(jsonResponse);
        return tasks;
    }

    private User[] GetUsers()
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(String.Format("http://{0}:{1}/users/all",  server, port));
        //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(String.Format("http://api.openweathermap.org/data/2.5/weather?id={0}&APPID={1}", 
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string jsonResponse = fixJson(reader.ReadToEnd());
        Debug.Log("User Response" + jsonResponse);
        User[] users = JsonHelper.FromJson<User>(jsonResponse);
        return users;
    }

    public IEnumerator UpdateTaskForUser(Task task, long userId)
    {
        Debug.Log("Updating task for user" + task.ToString());
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
            UpdateCurrentState();
            RegenerateTasks();   
        }
    }

    public IEnumerator UpdateTask(Task task)
    {
        Debug.Log("Updating task" + task.ToString());
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
            UpdateCurrentState();
            RegenerateTasks();   
        }
    }

    string fixJson(string value)
    {
        value = "{\"Items\":" + value + "}";
        return value;
    }

    public string returnUserName(long id) {
        for(int i=0; i< currentUsers.Length; i++) {
            if(id == currentUsers[i].id) {
                return currentUsers[i].username;
            }
        }
        return "(not assigned)";
    }
}
