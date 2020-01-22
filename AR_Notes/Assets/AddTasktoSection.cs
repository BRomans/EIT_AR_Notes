using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AddTasktoSection : MonoBehaviour
{
    TextMeshPro status;

    // Start is called before the first frame update
    void Start()
    {
        status = GetComponentInChildren<TextMeshPro>();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {  
        if (other.gameObject.name == "ImageTarget_ToDo")
        {
            status.SetText("IN TO DO");
        }
        else if (other.gameObject.name == "ImageTarget_InProgress")
        {
            status.SetText("IN PROGRESS");
        }
        else if (other.gameObject.name == "ImageTarget_Completed")
        {
            status.SetText("IN COMPLETED");
        }
        else status.SetText("IN Somewhere?");
    }

    void OnTriggerExit(Collider other)
    {  
        status.SetText("OUT ");
    }

}
