using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignUser : MonoBehaviour
{
    public Task task;
    public User user;
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
        var userid = this.GetComponent<UserFieldsManager>().id;
        other.GetComponent<TaskFieldsManager>().ClaimTask(userid);
    }
}
