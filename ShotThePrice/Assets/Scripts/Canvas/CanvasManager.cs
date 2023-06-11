using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    Home home;
    LevelSelection levelSelection;
    EndScreen endScreen;
    CanvasManager canvasManager;
    Instruction instruction;

    private void Awake()
    {
        home = FindObjectOfType<Home>();
        levelSelection = FindObjectOfType<LevelSelection>();
        endScreen = FindObjectOfType<EndScreen>();
        canvasManager = FindObjectOfType<CanvasManager>();
        instruction = FindObjectOfType<Instruction>();
    }

    private void Start()
    {
        DeactivateAllCanvases();
        ActiveCanvas();
    }

    public void DeactivateAllCanvases()
    {
        if (home != null)
            home.gameObject.SetActive(false);
        if (levelSelection != null)
            levelSelection.gameObject.SetActive(false);
        if (endScreen != null)
            endScreen.gameObject.SetActive(false);
        if (canvasManager != null)
            canvasManager.gameObject.SetActive(false);
        if (instruction != null)
            instruction.gameObject.SetActive(false);
    }

    public void ActiveCanvas()
    {
        if (GameManager.activeCanvas == CanvasConstant.Home)
            home.gameObject.SetActive(true);
        else if (GameManager.activeCanvas == CanvasConstant.LevelSelection)
            levelSelection.gameObject.SetActive(true);
        else if (GameManager.activeCanvas == CanvasConstant.EndScreen)
            endScreen.gameObject.SetActive(true);
        else if (GameManager.activeCanvas == CanvasConstant.CanvasManager)
            canvasManager.gameObject.SetActive(true);
        else if (GameManager.activeCanvas == CanvasConstant.Instruction)
            instruction.gameObject.SetActive(true);
        else
            home.gameObject.SetActive(true);
    }

    public void SwitchCanvasScene()
    {
        if (home != null)
            home.gameObject.SetActive(false);
        if (levelSelection != null)
            levelSelection.gameObject.SetActive(false);
        if (endScreen != null)
            endScreen.gameObject.SetActive(false);
        if (canvasManager != null)
            canvasManager.gameObject.SetActive(false);
        if (canvasManager != null)
            instruction.gameObject.SetActive(false);

        canvasManager.gameObject.SetActive(true);
    }
}
