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
    public DateTime startDate;
    public DateTime endDate;
    public DateTime createdAt;
    public DateTime updatedAt;
}
