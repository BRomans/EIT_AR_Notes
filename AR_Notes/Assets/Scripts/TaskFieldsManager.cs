using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Class that manages the mapping between the Task data objects and the Task game objects
/// </summary>
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

    /// <summary>
    /// Setup the input text box
    /// </summary>
    public void SetInputField() {
        this.descriptionInput.text = this.task.description;
    }

    /// <summary>
    /// Setup the main fields of the game object
    /// </summary>
    /// <param name="task">Task data object</param>
    /// <param name="title">Task title</param>
    /// <param name="description">Task description</param>
    /// <param name="username">User username</param>
    public void SetFields(Task task, String title, String description, String username) {
        this.title.SetText(title);
        this.description.SetText(description);
        this.user.SetText(username);
        this.task = task;
    }

    /// <summary>
    /// Updates the description of a task then calls the APIManager to update the server
    /// </summary>
    public void UpdateTaskDescription() {
        this.task.description = this.descriptionInput.text;
        if(this.task.userId != 0) {
            StartCoroutine(apiManager.UpdateTaskForUser(this.task, this.task.userId));
        } else {
            StartCoroutine(apiManager.UpdateTask(this.task));
        }
    }

    /// <summary>
    /// Ulear the fields of a task then calls the APIManager to update the server
    /// </summary>
    public void ClearTaskContent() {        
        StartCoroutine(apiManager.ClearTask(this.task));
    }

    /// <summary>
    /// Updates the status of a task then calls the APIManager to update the server
    /// <param name="status">New Task status</param>
    /// </summary>
    public void UpdateTaskStatus(string status) {
        this.task.status = status;
        if(this.task.userId != 0) {
            StartCoroutine(apiManager.UpdateTaskForUser(this.task, this.task.userId));
        } else {
            StartCoroutine(apiManager.UpdateTask(this.task));
        }
    }

    /// <summary>
    /// Assign the Task to the current user then calls the APIManager to update the server
    /// <param name="newUserId">User id of the new owner</param>
    /// </summary>
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
