using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignUser : MonoBehaviour
{
    public Task task;
    private APIManager apiManager; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {  
        other.GetComponent<TaskFieldsManager>().ClaimTask(1);
    }
}
