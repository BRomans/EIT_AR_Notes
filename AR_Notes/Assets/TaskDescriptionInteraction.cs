using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;


{

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
            // This example show the convenience of having both TMP components derive from TMP_Text. 
            if (ObjectType == 0)
                m_text = GetComponent<TextMeshPro>() ?? gameObject.AddComponent<TextMeshPro>();
            else
                m_text = GetComponent<TextMeshProUGUI>() ?? gameObject.AddComponent<TextMeshProUGUI>();

        }


        void Update()
        {
            
        }

    }
}
