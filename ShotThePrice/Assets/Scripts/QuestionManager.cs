using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static QuestionConstant;

public class QuestionManager : MonoBehaviour
{
    public string questionDataFileName;
    public TextAsset questionDataJson;
    private JsonData jsonData;

    private void Start()
    {
        // LoadGameData();
        // GenerateRandomQuestions(5); // Generate 5 random questions
    }

    private void LoadQuestionDataFromResources()
    {
        // questionDataJson = Resources.Load<TextAsset>(questionDataFileName);
        if (GameManager.currentLevel == 1)
        {
            questionDataJson = Resources.Load<TextAsset>("Json/physics");
        }
        else if (GameManager.currentLevel == 2)
        {
            questionDataJson = Resources.Load<TextAsset>("Json/chemistry");
        }
        else if (GameManager.currentLevel == 3)
        {
            questionDataJson = Resources.Load<TextAsset>("Json/biology");
        }
        else if (GameManager.currentLevel == 4)
        {
            questionDataJson = Resources.Load<TextAsset>("Json/zoology");
        }
        else if (GameManager.currentLevel == 5)
        {
            questionDataJson = Resources.Load<TextAsset>("Json/astronomy");
        }
    }

    private void LoadGameData()
    {
        if (questionDataJson)
            jsonData = JsonConvert.DeserializeObject<JsonData>(questionDataJson.text);
    }

    [ContextMenu("Load Question")]
    public List<Question> GenerateRandomQuestions()
    {
        try
        {
            int count = 5;
            LoadQuestionDataFromResources();
            LoadGameData();

            var questions = new List<Question>();

            if (jsonData.ShootThePrice != null && jsonData.ShootThePrice.Module != null)
            {
                List<QuestionData> allQuestions = jsonData.ShootThePrice.Module;

                // Shuffle the list of questions
                allQuestions = ShuffleList(allQuestions);

                // Take the desired number of random questions
                List<QuestionData> randomQuestions = allQuestions.Take(count).ToList();

                foreach (QuestionData questionData in randomQuestions)
                {
                    List<string> answers = new List<string>
                {
                    questionData.CorrectAnswer,
                    questionData.IncorrectAnswer1
                };

                    answers = ShuffleList(answers);
                    int correctAnswerIndex = CheckAnswerIndex(answers, questionData.CorrectAnswer);

                    Question question = new Question
                    {
                        QuestionText = questionData.QuestionText,
                        Answers = answers,
                        CorrectAnswerIndex = correctAnswerIndex
                    };

                    questions.Add(question);
                }
            }
            return questions;
        }
        catch (Exception)
        {
            return new List<Question>();
        }
    }

    private List<T> ShuffleList<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = UnityEngine.Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
        return list;
    }

    private int CheckAnswerIndex<T>(List<T> list, string correctAnswer)
    {
        foreach (var item in list)
        {
            if (item.Equals(correctAnswer))
            {
                return list.IndexOf(item);
            }
        }

        return 0;
    }
}
