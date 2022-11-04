using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class DialogueInstance : MonoBehaviour
{
    [Range(0f, 5f)] public float range;
    public Canvas Indicator;
    public Dialogue[] dialogues;


    SphereCollider sCol;

    private void Awake()
    {
        sCol = GetComponent<SphereCollider>();
        sCol.transform.position = transform.position; //Center the sphere colider at the origin
        sCol.isTrigger = true;
        sCol.radius = range;
    }

    //Enables the dialogue indicator popup
    public void EnableIndicator()
    {
        Indicator.enabled = true;
    }

    //Disables the dialogue indicator popup
    public void DisableIndicator()
    {
        Indicator.enabled = false;
    }

    //Returns if the dialogue instance is in range
    public bool isEnabled()
    {
        return Indicator.enabled;
    }

    //Sends the provided dialogue to the main dialogue canvas
    public Dialogue[] returnDialogues()
    {
        return dialogues;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            EnableIndicator();
            other.GetComponent<DialogueScript>().SetDialogue(dialogues);
            other.GetComponent<DialogueScript>().SetInRange();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            DisableIndicator();
            other.GetComponent<DialogueScript>().SetOutRange();
        }
    }

}
