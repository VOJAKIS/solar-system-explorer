using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Descriptor : MonoBehaviour
{
    public TextMeshProUGUI titleTMP;
    public TextMeshProUGUI descriptionTMP;
    public string title;
    public string description;

    void Start()
    {
        titleTMP.text = "<size=100%>" + title;
        descriptionTMP.text = "<size=50%>" + description; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
