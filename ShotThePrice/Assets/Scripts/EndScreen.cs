using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    public Transform starsContainer;
    public TextMeshProUGUI highestScoreText;
    public CanvasManager canvasManager;
    public AudioManager audioManager;

    private void Awake()
    {
        canvasManager = FindObjectOfType<CanvasManager>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    void Start()
    {
        highestScoreText.text = LevelManager.LoadHighestScore().ToString();
        UpdateStarRating(GameManager.correctAnswerCount);
    }

    void Update()
    {
    }

    public void UpdateStarRating(int correctAnswerCount)
    {
        int noOfStars = ConvertPointsToStars(correctAnswerCount);
        int totalStars = starsContainer.childCount;

        // Iterate through each star and set its active state based on the current number of points
        for (int i = 0; i < totalStars; i++)
        {
            Transform star = starsContainer.GetChild(i);
            star.gameObject.SetActive(i < noOfStars);
        }

        highestScoreText.text = LevelManager.LoadHighestScore().ToString();

        if(noOfStars == 3)
        {
            LevelManager.SaveUnlockedLevel();
        }
    }

    public int ConvertPointsToStars(int points)
    {
        switch (points)
        {
            case >= 5:
                return 3;
            case >= 3:
                return 2;
            case > 0:
                return 1;
            default:
                return 0;
        }
    }

    public void OnNextButtonClick()
    {
        audioManager.PlaySound(AudioConstant.Sound.Click);
        GameManager.activeCanvas = CanvasConstant.LevelSelection;
        canvasManager.DeactivateAllCanvases();
        canvasManager.ActiveCanvas();
    }

    public void OnReplayButtonCLick()
    {
        audioManager.PlaySound(AudioConstant.Sound.Click);
        canvasManager.DeactivateAllCanvases();
        canvasManager.SwitchCanvasScene();
        SceneManager.LoadScene("level" + GameManager.currentLevel.ToString());
    }

}