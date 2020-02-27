using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserFieldsManager : MonoBehaviour
{
    private APIManager apiManager; 
    public User user;
    public long id;

    // Start is called before the first frame update
    void Start()
    {
        apiManager= GameObject.FindObjectOfType<APIManager>();        
    }

    /// Setup the main fields of the game object
    /// </summary>
    /// <param name="user">User data object</param>
    /// <param name="id">User ID</param>

    /// Assign user ID to target
    public void SetFields(User user, long id) {
        this.user = user;
        this.id = id;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
