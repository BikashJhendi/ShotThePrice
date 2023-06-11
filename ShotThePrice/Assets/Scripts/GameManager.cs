using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static Home home;
    static LevelSelection levelSelection;
    static EndScreen endScreen;
    static CanvasManager canvasManager;
    AudioManager audioManager;
    public static bool isMuted = false;
    public static string activeCanvas = "Home";
    public static int correctAnswerCount = 0;
    public static int currentLevel = 1;

    public static GameObject homeCanvas;
    public static GameObject levelSelectionCanvas;
    public static GameObject endCanvas;

    private void Awake()
    {
        home = FindObjectOfType<Home>();
        levelSelection = FindObjectOfType<LevelSelection>();
        endScreen = FindObjectOfType<EndScreen>();
        canvasManager = FindObjectOfType<CanvasManager>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    void Start()
    {
        //canvasManager.DeactivateAllCanvases();
        //canvasManager.ActiveCanvas();
    }

    void Update()
    {

    }

    public void OnPlayButtonClicked()
    {
        audioManager.PlaySound(AudioConstant.Sound.Click);
        activeCanvas = CanvasConstant.LevelSelection;
        canvasManager.DeactivateAllCanvases();
        canvasManager.ActiveCanvas();
    }

    public void OnInstructionButtonClicked()
    {
        audioManager.PlaySound(AudioConstant.Sound.Click);
        activeCanvas = CanvasConstant.Instruction;
        canvasManager.DeactivateAllCanvases();
        canvasManager.ActiveCanvas();
    }

    public void OnLevelButttonClicked()
    {
        ClickSound();
    }

    public void ClickSound()
    {
        audioManager.PlaySound(AudioConstant.Sound.Click);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
