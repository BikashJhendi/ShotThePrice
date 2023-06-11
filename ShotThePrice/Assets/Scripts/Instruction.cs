using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instruction : MonoBehaviour
{
    AudioManager audioManager;
    CanvasManager canvasManager;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        canvasManager = FindObjectOfType<CanvasManager>();
    }

    void Start()
    {
        
    }

    public void OnBackButtonClick()
    {
        audioManager.PlaySound(AudioConstant.Sound.Click);
        GameManager.activeCanvas = CanvasConstant.Home;
        canvasManager.DeactivateAllCanvases();
        canvasManager.ActiveCanvas();
    }
}
