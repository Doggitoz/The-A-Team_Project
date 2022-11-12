using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(DialogueInstance))]
public class DialogueTrigger : MonoBehaviour
{
    [Range(0f, 5f)] public float range;
    public Canvas Indicator;
    [Tooltip("Can this dialogue be replayed")]
    public bool Replayable;
    
    
    private Dialogue[] dialogues;
    private GameObject IndicatorGO;
    private bool canPlay = true;


    SphereCollider sCol;

    private void Awake()
    {
        sCol = GetComponent<SphereCollider>();
        sCol.transform.position = transform.position; //Center the sphere colider at the origin
        sCol.isTrigger = true;
        sCol.radius = range;

        dialogues = GetComponent<DialogueInstance>().dialogues;

        IndicatorGO = Indicator.transform.parent.gameObject;
    }

    private void Update()
    {
        Camera localMain = Camera.main;
        IndicatorGO.transform.localEulerAngles = localMain.gameObject.transform.localEulerAngles;
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

    public void Finished()
    {
        if (!Replayable)
        {
            canPlay = false;
        }
        else
        {
            EnableIndicator();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && canPlay)
        {
            EnableIndicator();
            other.GetComponent<DialogueReader>().SetDialogue(dialogues);
            other.GetComponent<DialogueReader>().SetInRange(transform.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            DisableIndicator();
            other.GetComponent<DialogueReader>().SetOutRange();
        }
    }

}
