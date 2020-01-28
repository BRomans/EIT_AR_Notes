using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaskFieldsManager : MonoBehaviour
{
    public TextMeshPro title;
    public TextMeshPro description;
    public TextMeshPro user;
    public Task task;
    public string status;
    private APIManager apiManager; 
    private GameObject apiManagerObj;

    // Start is called before the first frame update
    void Start()
    {
        apiManager= GameObject.FindObjectOfType<APIManager>();
        //apiManager = apiManagerObj.GetComponent<APIManager>();
        Debug.Log(apiManager);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeStatus(string status) {
        this.status = status;
    }

    public void SetFields(Task task, String title, String description, String username) {
        this.title.SetText(title);
        this.description.SetText(description);
        this.user.SetText(username);
        this.task = task;
    }

    public void UpdateTaskDescription() {
        this.task.description = this.description.text;
        if(this.task.userId != 0) {
            StartCoroutine(apiManager.UpdateTaskForUser(this.task, this.task.userId));
        } else {
            StartCoroutine(apiManager.UpdateTask(this.task));
        }
    }

    public void UpdateTaskStatus() {
        this.task.status = this.status;
        if(this.task.userId != 0) {
            StartCoroutine(apiManager.UpdateTaskForUser(this.task, this.task.userId));
        } else {
            StartCoroutine(apiManager.UpdateTask(this.task));
        }
    }

    public void ClaimTask(long newUserId) {
        long oldUserId = this.task.userId;
        this.task.userId = newUserId;
        if(this.task.userId != 0) {
            StartCoroutine(apiManager.UpdateTaskForUser(this.task, oldUserId));
        } else {
            StartCoroutine(apiManager.UpdateTaskForUser(this.task, newUserId));
        }
    }
}
