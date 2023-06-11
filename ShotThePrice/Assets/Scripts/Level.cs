using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static QuestionConstant;

public class Level : MonoBehaviour
{
    AudioManager audioManager;
    QuestionManager questionManager;
    GameManager gameManager;
    public Button soundButton;
    
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] TextMeshProUGUI[] answerText;
    [SerializeField] TextMeshProUGUI currentQuestionCountText;
    [SerializeField] TextMeshProUGUI correctAnswerCountText;
    [SerializeField] TextMeshProUGUI highestScoreText;
    List<Question> questions = new List<Question>();
    int currentQuestionIndex = 0;
    public int correctAnswerCount = 0;

    public GameObject tickPrefab;
    public GameObject crossPrefab;
    public GameObject centerSpawn;

    public GameObject[] boardGameObject;
    public GameObject[] boardPrefab;
    RectTransform canvasRectTransform;


    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        questionManager = FindObjectOfType<QuestionManager>();
        gameManager = FindObjectOfType<GameManager>();
        questions = questionManager.GenerateRandomQuestions();

        DisplayQuestion(currentQuestionIndex);
        UpdateUIText();
        ChangedSoundSprite();
        SlingshotController.ResetLives();

        canvasRectTransform = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
    }

    void Update()
    {

    }

    public void CheckAnswer(string answer)
    {
        if (!IsAnswerCorrect(answer))
        {
            DisplayIcon("cross");
            return;
        }

        DisplayIcon("tick");
        correctAnswerCount++;
        LevelManager.SaveHighestScore(LevelManager.LoadHighestScore() + 10);

        DestoryBoardUI();
        RegenerateBoardObject();
        NextQuestion();
        UpdateUIText();
    }

    private void DisplayQuestion(int questionIndex)
    {
        Question currentQuestion = questions[questionIndex];
        questionText.text = currentQuestion.QuestionText;

        for (int i = 0; i < answerText.Length; i++)
        {
            TextMeshProUGUI buttonText = answerText[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.Answers[i];
        }
    }

    public void NextQuestion()
    {
        currentQuestionIndex++;

        if (currentQuestionIndex < questions.Count)
        {
            DisplayQuestion(currentQuestionIndex);
        }
        else
        {
            Debug.Log("No more questions");
        }
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

    public void OnBackButtonClicked()
    {
        audioManager.PlaySound(AudioConstant.Sound.EggDropped);
        GameManager.activeCanvas = CanvasConstant.LevelSelection;
        SceneManager.LoadScene(ScenesConstant.Main);
    }

    void UpdateUIText()
    {
        currentQuestionCountText.text = (currentQuestionIndex + 1).ToString();
        correctAnswerCountText.text = correctAnswerCount.ToString();
        highestScoreText.text = LevelManager.LoadHighestScore().ToString();
    }

    bool IsAnswerCorrect(string answer)
    {
        int answerIndex = -1;
        Question currentQuestion = questions[currentQuestionIndex];

        foreach (var item in currentQuestion.Answers)
        {
            if (item.Equals(answer))
            {
                answerIndex = currentQuestion.Answers.IndexOf(item);
                break;
            }
        }

        if (currentQuestion.CorrectAnswerIndex == answerIndex)
            return true;

        return false;
    }

    [ContextMenu("DestoryBoardUI")]
    public void DestoryBoardUI()
    {
        foreach (var item in boardGameObject)
        {
            Destroy(item);
        }
    }

    [ContextMenu("RegenerateBoardObject")]
    public void RegenerateBoardObject()
    {
        for (int i = 0; i < boardGameObject.Length; i++)
        {
            boardGameObject[i] = Instantiate(boardPrefab[i], boardPrefab[i].transform.position, Quaternion.identity);
            boardGameObject[i].transform.SetParent(canvasRectTransform, false);
            answerText[i] = boardGameObject[i].GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    public void DisplayIcon(string iconName)
    {
        if (iconName == "cross")
        {
            centerSpawn = Instantiate(crossPrefab, crossPrefab.transform.position, Quaternion.identity);
            centerSpawn.transform.SetParent(canvasRectTransform, false);
            Destroy(centerSpawn, 0.5f);
        }
        else if (iconName == "tick")
        {
            centerSpawn = Instantiate(tickPrefab, tickPrefab.transform.position, Quaternion.identity);
            centerSpawn.transform.SetParent(canvasRectTransform, false);
            Destroy(centerSpawn, 0.5f);
        }
    }

    

}
