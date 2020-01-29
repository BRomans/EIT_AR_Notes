using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

/// <summary>
/// Deprecated class for managing the interaction with the TaskDescription
/// </summary>
public class TaskDescriptionInteraction : MonoBehaviour
{
    public enum objectType { TextMeshPro = 0, TextMeshProUGUI = 1 };

    public objectType ObjectType;
    public bool isStatic;

    private TMP_Text m_text;

    private TMP_InputField m_inputfield;



    void Awake()
    {
        // Get a reference to the TMP text component if one already exists otherwise add one.
        if (ObjectType == 0)
            m_text = GetComponent<TextMeshPro>() ?? gameObject.AddComponent<TextMeshPro>();
        else
            m_text = GetComponent<TextMeshProUGUI>() ?? gameObject.AddComponent<TextMeshProUGUI>();

    }


    void Update()
    {
        
    }

}

