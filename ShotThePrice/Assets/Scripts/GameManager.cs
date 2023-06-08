using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    Home home;
    LevelSelection levelSelection;
    AudioManager audioManager;

    private void Awake()
    {
        home = FindObjectOfType<Home>();
        levelSelection = FindObjectOfType<LevelSelection>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    void Start()
    {
        home.gameObject.SetActive(true);
        levelSelection.gameObject.SetActive(false);
    }

    void Update()
    {

    }

    public void OnPlayButtonClicked()
    {
        audioManager.PlaySound(AudioConstant.Sound.Click);
        home.gameObject.SetActive(false);
        levelSelection.gameObject.SetActive(true);
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnLevelButttonClicked()
    {
        ClickSound();
    }

    public void ClickSound()
    {
        audioManager.PlaySound(AudioConstant.Sound.Click);
    }
}
