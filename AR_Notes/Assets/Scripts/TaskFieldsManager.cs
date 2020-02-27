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
    public TMP_InputField titleInput;
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
    /// Setup the description input text box
    /// </summary>
    public void SetDescriptionInputField() {
        this.descriptionInput.text = this.task.description;
    }

    /// <summary>
    /// Setup the title input text box
    /// </summary>
    public void SetTitleInputField() {
        this.titleInput.text = this.task.title;
    }

    /// <summary>
    /// Setup the description text box
    /// </summary>
    public void SetDescriptionField() {
        this.description.SetText(this.task.description);
    }

    /// <summary>
    /// Setup the title text box
    /// </summary>
    public void SetTitleField() {
        this.title.SetText(this.titleInput.text);
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
        bool modified = false;
        Debug.Log("text:" + this.descriptionInput.text);
        if(this.descriptionInput.text != null || this.descriptionInput.text != "") {
            this.task.description = this.descriptionInput.text;
            modified = true;
        }
        if(this.titleInput.text != null || this.titleInput.text != "") {
            this.task.title = this.titleInput.text;
            modified = true;
        }
        if(modified) {
            this.task.empty = false;
            if(this.task.userId != 0) {
                StartCoroutine(apiManager.UpdateTaskForUser(this.task, this.task.userId));
            } else {
                StartCoroutine(apiManager.UpdateTask(this.task));
            }
        }

    }

    /// <summary>
    /// Ulear the fields of a task then calls the APIManager to update the server
    /// </summary>
    public void ClearTaskContent() {        
        this.title.SetText("");
        this.description.SetText("");
        this.user.SetText("");
        this.status = "";
        this.task.description = "";
        this.task.title = "";
        this.task.userId = 0;
        this.task.status ="todo";
        StartCoroutine(apiManager.ClearTask(this.task));
    }

    /// <summary>
    /// Ulear the fields of a task then calls the APIManager to update the server
    /// </summary>
    public void CopyTaskContent(Task task) {   

        if(this.task.empty) {
            this.task.description = task.description;
            this.task.title = task.title;
            this.task.userId = task.userId;
            this.task.projectId = task.projectId;
            this.task.status = task.status;
            this.task.empty = false;
            if(this.task.userId != 0) {
            StartCoroutine(apiManager.UpdateTaskForUser(this.task, this.task.userId));
            } else {
                StartCoroutine(apiManager.UpdateTask(this.task));
            }
            Debug.Log("Copied!");
        } else {
            Debug.Log("Task not empty!");
        }
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
