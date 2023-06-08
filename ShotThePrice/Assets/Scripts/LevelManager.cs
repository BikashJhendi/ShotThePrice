using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private int currentLevel = 1;
    private int unlockedLevel = 1;

    private const string UnlockedLevelKey = "UnlockedLevel";

    private void Start()
    {
        LoadUnlockedLevel();
    }

    public void LoadNextLevel()
    {
        if (currentLevel >= unlockedLevel)
        {
            unlockedLevel++;
            SaveUnlockedLevel();
        }
        currentLevel++;

        if (currentLevel > 5)
        {
            // Player completed all levels, show a game completion screen or perform any other desired action.
            Debug.Log("Congratulations! You completed all levels!");
        }
        else
        {
            SceneManager.LoadScene("LevelScene");
        }
    }

    public void LoadLevel(int level)
    {
        if (level <= unlockedLevel)
        {
            currentLevel = level;
            SceneManager.LoadScene("LevelScene");
        }
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainScene");
    }

    public int GetUnlockedLevel()
    {
        return 2;
    }

    private void SaveUnlockedLevel()
    {
        PlayerPrefs.SetInt(UnlockedLevelKey, unlockedLevel);
    }

    private void LoadUnlockedLevel()
    {
        if (PlayerPrefs.HasKey(UnlockedLevelKey))
        {
            unlockedLevel = PlayerPrefs.GetInt(UnlockedLevelKey);
        }
        else
        {
            PlayerPrefs.SetInt(UnlockedLevelKey, unlockedLevel);
        }
    }
}
