using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyTaskContent : MonoBehaviour
{


    /// <summary>
    /// Copy the content of a Task on an empty colliding task
    /// </summary>
    /// <param name="other">Collider of the section</param>
    void OnTriggerEnter(Collider other)
    {  
        
        if (other.gameObject.tag == "Task")
        {
            Debug.Log("It's a task");
        }
    }
}
