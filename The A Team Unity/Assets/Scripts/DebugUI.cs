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
    public AudioManager AM;
    public Canvas DebugCanvas;
    public PlayerInput PlayerInputComponent;


    bool isOpen = false;

    private void Awake()
    {
        AM = AudioManager.AM;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
        Debug.Log(AM.GetGlobalMusicVolume());
    }

    public void StartMusicOne()
    {
        AM.PlayMusic(MusicOne);
    }

    public void StartMusicTwo()
    {
        AM.PlayMusic(MusicTwo);
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
        AM.SetMasterVolume(f);
    }

    public void MusicVolSlider(float f)
    {
        AM.SetMusicVolume(f);
    }

}
