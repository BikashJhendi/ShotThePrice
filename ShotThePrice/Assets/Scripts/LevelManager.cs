using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private int currentLevel = 1;
    private static int unlockedLevel = 1;
    private CanvasManager canvasManager;

    private void Awake()
    {
        canvasManager = FindObjectOfType<CanvasManager>();
    }

    private void Start()
    {
        LoadUnlockedLevel();
    }

    public void LoadLevel(int level)
    {
        if (level <= unlockedLevel)
        {
            GameManager.currentLevel = level;
            canvasManager.SwitchCanvasScene();
            if (level == 1)
            {
                SceneManager.LoadScene(ScenesConstant.Level1);
            }
            else if (level == 2)
            {
                SceneManager.LoadScene(ScenesConstant.Level2);
            }
            else if (level == 3)
            {
                SceneManager.LoadScene(ScenesConstant.Level3);
            }
            else if (level == 4)
            {
                SceneManager.LoadScene(ScenesConstant.Level4);
            }
            else if (level == 5)
            {
                SceneManager.LoadScene(ScenesConstant.Level5);
            }
        }
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(ScenesConstant.Main);
    }

    public int GetUnlockedLevel()
    {
        return unlockedLevel;
    }

    public static void SaveUnlockedLevel()
    {
        if (GameManager.currentLevel == unlockedLevel)
        {
            if (unlockedLevel < 5)
                unlockedLevel++;
            PlayerPrefs.SetInt(PlayerPrefConstant.UnlockedLevel, unlockedLevel);
        }
    }

    private void LoadUnlockedLevel()
    {
        if (PlayerPrefs.HasKey(PlayerPrefConstant.UnlockedLevel))
        {
            unlockedLevel = PlayerPrefs.GetInt(PlayerPrefConstant.UnlockedLevel);
        }
        else
        {
            PlayerPrefs.SetInt(PlayerPrefConstant.UnlockedLevel, unlockedLevel);
        }
    }

    public static void SaveHighestScore(int highestScore)
    {
        PlayerPrefs.SetInt(PlayerPrefConstant.HighestScore, highestScore);
    }

    public static int LoadHighestScore()
    {
        if (PlayerPrefs.HasKey(PlayerPrefConstant.HighestScore))
        {
            return PlayerPrefs.GetInt(PlayerPrefConstant.HighestScore);
        }

        return 0;
    }

}
