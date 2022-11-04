/*
 * TODO:
 * 
 * Disable all player inputs whilst in dialogue. Possibly utilizes timelines? I'm not entirely sure.
 * 
 * Have the indicator billboard follow the player. It also needs to stand out in front of all objects
 * 
 * Potentially incorporate moving the players camera to the player talking
 * 
 * General script polishing. Allow for potential refactor of dialogue array to txt file
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DialogueScript : MonoBehaviour
{
    public TMP_Text DialogueText;
    public TMP_Text NameText;
    public Canvas DialogueCanvas;
    [Range(1, 5)] public int textSpeed = 1;

    //private variables
    Dialogue[] dialogues;
    int dialoguesIndex = 0;
    float timeSinceStartedReading = 0f;
    bool completedCurrentDialogue = false;
    bool inRangeOfDialogue = false;
    bool isPlayingDialogue = false;
    string currentName = "";

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && inRangeOfDialogue)
        {
            StartDialogue();
        }

        if (!isPlayingDialogue) return; //If not playing a dialogue instance

        if (!completedCurrentDialogue) //If the current dialogue frame has not concluded
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

        if (Input.GetMouseButtonDown(0)) //Used to skip the current dialogue frame or move onto next frame
        {
            if (completedCurrentDialogue)
            {
                dialoguesIndex++;
                timeSinceStartedReading = 0;
                completedCurrentDialogue = false;
                if (dialoguesIndex >= dialogues.Length)
                {
                    dialoguesIndex = 0;
                    StopDialogue();
                }
            }
            else
            {
                timeSinceStartedReading = 1000; // Arbitrary large number. Mathf.Infinity does not work
            }
        }
    }

    public void StartDialogue()
    {
        isPlayingDialogue = true;
        DialogueCanvas.enabled = true;
    }

    public void StopDialogue()
    {
        isPlayingDialogue = false;
        DialogueCanvas.enabled = false;
    }

    public void SetInRange()
    {
        inRangeOfDialogue = true;
    }

    public void SetOutRange()
    {
        inRangeOfDialogue = false;
    }

    public void SetDialogue(Dialogue[] d)
    {
        dialogues = d;
    }
}
