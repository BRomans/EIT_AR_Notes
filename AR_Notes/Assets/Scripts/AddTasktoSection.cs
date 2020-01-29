using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// This class defines the methods to update the status of a Task
/// </summary>
public class AddTasktoSection : MonoBehaviour
{
    public TextMeshPro currentStatus;
    public StatusBand statusBand;
    public TaskFieldsManager fieldsManager;
    public Material todoMaterial;
    public Material inProgressMaterial;
    public Material completedMaterial;
    private string statusName;

    /// <summary>
    /// Init the status band when the component is created
    /// </summary>
    void Start()
    {
        if (fieldsManager.task.status == "todo")
        {
            statusBand.GetComponent<Renderer>().material = todoMaterial;
            ChangeStatusText("TO DO");
        }
        else if (fieldsManager.task.status == "in_progress")
        {
            statusBand.GetComponent<Renderer>().material = inProgressMaterial;
            ChangeStatusText("IN PROGRESS");

        }
        else if (fieldsManager.task.status == "completed")
        {
            statusBand.GetComponent<Renderer>().material = completedMaterial;
            ChangeStatusText("COMPLETED");

        }
    }

    /// <summary>
    /// Updates the status band every frame to make it consistent with changes
    /// </summary>
    void Update()
    {
         if (fieldsManager.task.status == "todo")
        {
            statusBand.GetComponent<Renderer>().material = todoMaterial;
            ChangeStatusText("TO DO");
        }
        else if (fieldsManager.task.status == "in_progress")
        {
            statusBand.GetComponent<Renderer>().material = inProgressMaterial;
            ChangeStatusText("IN PROGRESS");

        }
        else if (fieldsManager.task.status == "completed")
        {
            statusBand.GetComponent<Renderer>().material = completedMaterial;
            ChangeStatusText("COMPLETED");

        }
    }

    /// <summary>
    /// Changes the status of a Task based on the collider of the section it enters
    /// </summary>
    /// <param name="other">Collider of the section</param>
    void OnTriggerEnter(Collider other)
    {  
        if (other.gameObject.name == "ImageTarget_ToDo")
        {
            statusBand.GetComponent<Renderer>().material = todoMaterial;
            ChangeStatusText("TO DO");
            fieldsManager.UpdateTaskStatus("todo");
        }
        else if (other.gameObject.name == "ImageTarget_InProgress")
        {
            statusBand.GetComponent<Renderer>().material = inProgressMaterial;
            ChangeStatusText("IN PROGRESS");
            fieldsManager.UpdateTaskStatus("in_progress");


        }
        else if (other.gameObject.name == "ImageTarget_Completed")
        {
            statusBand.GetComponent<Renderer>().material = completedMaterial;
            ChangeStatusText("COMPLETED");
            fieldsManager.UpdateTaskStatus("completed");

        }
    }

    /// <summary>
    /// Updates the label of the Status band
    /// </summary>
    /// <param name="status">Label of the new status</param>
    public void ChangeStatusText(string status) {
        this.statusName = status;
        this.currentStatus.SetText(status);
    }

    void OnTriggerExit(Collider other)
    {  
        //status.SetText("OUT ");
    }

}
