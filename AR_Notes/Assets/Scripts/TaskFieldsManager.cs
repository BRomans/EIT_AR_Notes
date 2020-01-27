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
           apiManagerObj= GameObject.Find("APIManager");
           apiManager = apiManagerObj.GetComponent<APIManager>();

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
        apiManager.UpdateTask(this.task, this.task.userId);
    }

    public void UpdateTaskStatus() {
        this.task.status = this.status;
        apiManager.UpdateTask(this.task, this.task.userId);
    }

    public void ClaimTask(long newUserId) {
        long oldUserId = this.task.userId;
        this.task.userId = newUserId;
        apiManager.UpdateTask(this.task, oldUserId);
    }
}
