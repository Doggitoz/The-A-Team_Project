/*
 * TODO:
 * 
 * Multiple dialogue instances on one object --DOESN'T SEEM TO WORK. WILL NEED TO PROVIDE MULTIPLE DIALOGUE TRIGGERS IT SEEMS
 * 
 * Potentially incorporate moving the players camera to an object of choice (NEED TO LEARN HOW CINEMACHINE WORKS FIRST)
 * 
 * Maybe improve the interaction between DialogueTrigger and DialogueReader
 * 
 * General script polishing. Allow for potential refactor of dialogue array to txt file
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using static UnityEngine.Debug;
using StarterAssets;


public class DialogueReader : MonoBehaviour
{
    //Inspector variables
    public TMP_Text DialogueText;
    public TMP_Text NameText;
    public Canvas DialogueCanvas;
    public PlayerInput PlayerInputComponent;
    [Tooltip("Stops all time based events from happening whilst in dialogue box")]
    public bool FreezesGame;
    [Range(1, 5)] public int textSpeed = 1;

    //private variables
    Dialogue[] dialogues;
    int dialoguesIndex = 0;
    float timeSinceStartedReading = 0f;
    bool completedCurrentDialogue = false;
    GameObject inRangeOfDialogue;
    bool isPlayingDialogue = false;
    string currentName = "";
    private StarterAssetsInputs _input;

    private void Awake()
    {
        _input = GetComponent<StarterAssetsInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_input.interact && inRangeOfDialogue && dialogues != null)
        {
            StartDialogue();
            inRangeOfDialogue.GetComponent<DialogueTrigger>().DisableIndicator();
        }

        if (!isPlayingDialogue) return; //If not playing a dialogue instance

        if (!completedCurrentDialogue) //If the current dialogue frame has not concluded
        {
            UpdateScrollingText();
        }

        if (Input.GetMouseButtonDown(0)) //Used to skip the current dialogue frame or move onto next frame
        {
            if (completedCurrentDialogue)
            {
                NextFrame();
            }
            else
            {
                timeSinceStartedReading = 1000; // Arbitrary large number. Mathf.Infinity does not work. This fills in the rest of the text
            }
        }
    }

    private void UpdateScrollingText()
    {
        if (dialogues[dialoguesIndex].Name != currentName && dialogues[dialoguesIndex].Name != "")
        {
            currentName = dialogues[dialoguesIndex].Name;
            NameText.text = currentName;
        }
        timeSinceStartedReading += Time.unscaledDeltaTime * textSpeed * 10; //Unscaled delta time for the sake of pausing the game.
        int totalCharsToShow = Mathf.FloorToInt(timeSinceStartedReading);
        totalCharsToShow = Mathf.Clamp(totalCharsToShow, 0, dialogues[dialoguesIndex].Text.Length);
        DialogueText.text = dialogues[dialoguesIndex].Text.Substring(0, totalCharsToShow);
        completedCurrentDialogue = (totalCharsToShow == dialogues[dialoguesIndex].Text.Length) ? true : false;
    }

    private void NextFrame()
    {
        dialoguesIndex++;
        timeSinceStartedReading = 0;
        completedCurrentDialogue = false;
        if (dialoguesIndex >= dialogues.Length)
        {
            dialoguesIndex = 0;
            StopDialogue();
            DialogueTrigger DT = inRangeOfDialogue.GetComponent<DialogueTrigger>();
            DT.Finished();
            if (DT.Replayable == false)
            {
                dialogues = null;
            }
        }
    }

    public void StartDialogue()
    {
        isPlayingDialogue = true;
        DialogueCanvas.enabled = true;
        PlayerInputComponent.enabled = false;
        if (FreezesGame)
            Time.timeScale = 0;
    }

    public void StopDialogue()
    {
        isPlayingDialogue = false;
        DialogueCanvas.enabled = false;
        PlayerInputComponent.enabled = true;
        if (FreezesGame)
            Time.timeScale = 1;
    }

    public void SetInRange(GameObject GO)
    {
        inRangeOfDialogue = GO;
    }

    public void SetOutRange()
    {
        inRangeOfDialogue = null;
    }

    public void SetDialogue(Dialogue[] dia)
    {
        dialogues = dia;
    }
}
