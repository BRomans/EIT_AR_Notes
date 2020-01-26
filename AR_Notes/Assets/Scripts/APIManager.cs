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
    public Task[] currentTasks;
    public User[]  currentUsers;

    // Start is called before the first frame update
    void Start()
    {
        updateCurrentState();       
    }

    // Update is called once per frame
    void Update()
    {
        updateCurrentState();
    }

    void updateCurrentState() {
        currentUsers = GetUsers();
        currentTasks = GetTasks(); 
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

    string fixJson(string value)
    {
        value = "{\"Items\":" + value + "}";
        return value;
    }
}
