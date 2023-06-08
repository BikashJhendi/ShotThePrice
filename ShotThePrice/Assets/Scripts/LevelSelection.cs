using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    public Button[] levelButtons;
    public Sprite[] unlockedButtonSprite;
    public Sprite lockedButtonSprite;
    private LevelManager levelManager;
    private AudioManager audioManager;

    void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Start()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            int levelIndex = i + 1; // Assuming levels are 1-based index

            if (levelIndex <= levelManager.GetUnlockedLevel())
            {
                levelButtons[i].image.sprite = unlockedButtonSprite[i];
                levelButtons[i].onClick.AddListener(() => audioManager.PlaySound(AudioConstant.Sound.Click));
                // levelButtons[i].GetComponentInChildren<Text>().text = "Level " + levelIndex;
                // levelButtons[i].onClick.AddListener(() => SelectLevel(levelIndex));
            }
            else
            {
                levelButtons[i].image.sprite = lockedButtonSprite;
                // levelButtons[i].GetComponentInChildren<Text>().text = "Locked";
                // levelButtons[i].interactable = true;
                levelButtons[i].onClick.AddListener(() => audioManager.PlaySound(AudioConstant.Sound.EggDropped));
            }
        }
    }

    private void SelectLevel(int level)
    {
        levelManager.LoadLevel(level);
    }
}