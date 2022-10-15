using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueScript : MonoBehaviour
{
    public TMP_Text DialogueText;
    public TMP_Text NameText;
    [Range(1, 5)] public int textSpeed = 1;
    public Dialogue[] dialogues;

    //private variables
    int dialoguesIndex = 0;
    float timeSinceStartedReading = 0f;
    bool completedCurrentDialogue = false;
    string currentName = "";
    GameObject currentPlayer;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (!completedCurrentDialogue)
        {
            if (dialogues[dialoguesIndex].Name != currentName && dialogues[dialoguesIndex].Name != "")
            {
                currentName = dialogues[dialoguesIndex].Name;
                NameText.text = currentName;
            }
            timeSinceStartedReading += Time.deltaTime * textSpeed * 10;
            int totalCharsToShow = Mathf.FloorToInt(timeSinceStartedReading);
            totalCharsToShow = Mathf.Clamp(totalCharsToShow, 0, dialogues[dialoguesIndex].Text.Length);
            DialogueText.text = dialogues[dialoguesIndex].Text.Substring(0, totalCharsToShow);
            completedCurrentDialogue = (totalCharsToShow == dialogues[dialoguesIndex].Text.Length) ? true : false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (completedCurrentDialogue)
            {
                dialoguesIndex++;
                dialoguesIndex = Mathf.Clamp(dialoguesIndex, 0, dialogues.Length);
                timeSinceStartedReading = 0;
                completedCurrentDialogue = false;
            }
            else
            {
                timeSinceStartedReading = 1000f;
            }
        }
    }
}
