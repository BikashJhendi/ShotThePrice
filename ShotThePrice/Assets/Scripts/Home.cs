using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Home : MonoBehaviour
{
    LevelSelection levelSelection;
    AudioManager audioManager;
    public Button soundButton;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        ChangedSoundSprite();
    }

    void Update()
    {
    }

    void ChangedSoundSprite()
    {
        if (GameManager.isMuted)
        {
            soundButton.image.sprite = audioManager.mute;
        }
        else
        {
            soundButton.image.sprite = audioManager.unMute;
        }
    }

    public void OnSoundButtonClicked()
    {
        ChangedSoundSprite();
    }
}
