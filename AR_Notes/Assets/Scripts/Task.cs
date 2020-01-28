using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
