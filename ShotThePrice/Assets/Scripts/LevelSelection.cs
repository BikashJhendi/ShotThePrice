using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    public Button[] levelButtons;
    public Sprite[] unlockedButtonSprite;
    public Sprite lockedButtonSprite;
    private LevelManager levelManager;
    private AudioManager audioManager;
    private CanvasManager canvasManager;

    void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
        audioManager = FindObjectOfType<AudioManager>();
        canvasManager = FindObjectOfType<CanvasManager>();
    }

    private void Start()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            int levelIndex = i + 1; 

            if (levelIndex <= levelManager.GetUnlockedLevel())
            {
                levelButtons[i].image.sprite = unlockedButtonSprite[i];
                levelButtons[i].onClick.AddListener(() => SelectLevel(levelIndex));
            }
            else
            {
                levelButtons[i].image.sprite = lockedButtonSprite;
                levelButtons[i].onClick.AddListener(() => audioManager.PlaySound(AudioConstant.Sound.EggDropped));
            }
        }
    }

    private void SelectLevel(int level)
    {
        audioManager.PlaySound(AudioConstant.Sound.Click);
        levelManager.LoadLevel(level);
    }

    public void OnBackButtonClick()
    {
        audioManager.PlaySound(AudioConstant.Sound.Click);
        GameManager.activeCanvas = CanvasConstant.Home;
        canvasManager.DeactivateAllCanvases();
        canvasManager.ActiveCanvas();
    }

}