using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class DebugUI : MonoBehaviour
{
    public AudioClip MusicOne;
    public AudioClip MusicTwo;
    public AudioManager AudioManager;
    public Canvas DebugCanvas;
    public PlayerInput PlayerInputComponent;


    bool isOpen = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }

    public void StartMusicOne()
    {
        AudioManager.PlayMusic(MusicOne);
    }

    public void StartMusicTwo()
    {
        AudioManager.PlayMusic(MusicTwo);
    }

    public void ToggleMenu()
    {
        if (isOpen)
        {
            isOpen = false;
            Cursor.lockState = CursorLockMode.None;
            DebugCanvas.enabled = false;
            //PlayerInputComponent.enabled = true;
        }
        else
        {
            isOpen = true;
            Cursor.lockState = CursorLockMode.Locked;
            DebugCanvas.enabled = true;
            //PlayerInputComponent.enabled = false;
        }
    }

    public void MasterVolSlider(float f)
    {
        Debug.Log("Float: " + f);
        AudioManager.SetMasterVolume(f);
    }

    public void MusicVolSlider(float f)
    {
        AudioManager.SetMusicVolume(f);
    }

}
