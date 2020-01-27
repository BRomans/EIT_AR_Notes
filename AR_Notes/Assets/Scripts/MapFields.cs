using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MapFields : MonoBehaviour
{
    public TextMeshPro title;
    public TextMeshPro description;
    public TextMeshPro user;

    // Start is called before the first frame update
    void Start()
    {
               

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetFields(String title, String description, String username) {
        this.title.SetText(title);
        this.description.SetText(description);
        this.user.SetText(username);
    }
}
