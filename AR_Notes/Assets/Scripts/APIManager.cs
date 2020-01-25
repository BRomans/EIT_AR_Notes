using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;

public class APIManager : MonoBehaviour
{

    private string server = "localhost";
    private string port = "8080";
    public TasksList currentTasks;
    public UsersList currentUsers;

    // Start is called before the first frame update
    void Start()
    {
        currentUsers = GetUsers();
        currentTasks = GetTasks();

        Debug.Log("Tasks: " + currentTasks);
        Debug.Log("Users: " + currentUsers);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private TasksList GetTasks()
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(String.Format("http://{0}:{1}/tasks/all",  server, port));
        //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(String.Format("http://api.openweathermap.org/data/2.5/weather?id={0}&APPID={1}", 
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string jsonResponse = reader.ReadToEnd();
        Debug.Log("Task Response" + jsonResponse);
        TasksList tasks = JsonUtility.FromJson<TasksList>(jsonResponse);
        return tasks;
    }

    private UsersList GetUsers()
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(String.Format("http://{0}:{1}/users/all",  server, port));
        //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(String.Format("http://api.openweathermap.org/data/2.5/weather?id={0}&APPID={1}", 
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string jsonResponse = reader.ReadToEnd();
        Debug.Log("User Response" + jsonResponse);
        UsersList users = JsonUtility.FromJson<UsersList>(jsonResponse);
        return users;
    }
}
