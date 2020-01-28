using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AddTasktoSection : MonoBehaviour
{
    public TextMeshPro currentStatus;
    public StatusBand statusBand;
    public TaskFieldsManager fieldsManager;
    public Material todoMaterial;
    public Material inProgressMaterial;
    public Material completedMaterial;
    private string statusName;

    // Start is called before the first frame update
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

    // Update is called once per frame
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

    public void ChangeStatusText(string status) {
        this.statusName = status;
        this.currentStatus.SetText(status);
    }

    void OnTriggerExit(Collider other)
    {  
        //status.SetText("OUT ");
    }

}
