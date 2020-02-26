using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Model of the Task data object that can be used to communicate with the server
/// </summary>
[Serializable]
public class Task {
    
    public long id;
    public long userId;
    public long projectId;
    public string title;
    public string description;
    public string status;
    public string startDate;
    public string endDate;
    public string createdAt;
    public string updatedAt;
    public string marker;
    public bool isEmpty;
}
