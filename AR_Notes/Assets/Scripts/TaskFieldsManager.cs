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
    public TMP_InputField descriptionInput;
    public Task task;
    public string status;
    private APIManager apiManager; 
    private GameObject apiManagerObj;

    // Start is called before the first frame update
    void Start()
    {
        apiManager= GameObject.FindObjectOfType<APIManager>();
        Debug.Log(apiManager);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetInputField() {
        this.descriptionInput.text = this.task.description;
    }

    public void SetFields(Task task, String title, String description, String username) {
        this.title.SetText(title);
        this.description.SetText(description);
        this.user.SetText(username);
        this.task = task;
    }

    public void UpdateTaskDescription() {
        this.task.description = this.descriptionInput.text;
        if(this.task.userId != 0) {
            StartCoroutine(apiManager.UpdateTaskForUser(this.task, this.task.userId));
        } else {
            StartCoroutine(apiManager.UpdateTask(this.task));
        }
    }

    public void UpdateTaskStatus(string status) {
        this.task.status = status;
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
