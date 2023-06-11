using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Common
{

}

public static class AudioConstant
{
    public enum Sound
    {
        Click,
        EggDropped,
        Release,
        Stretch
    };

    [System.Serializable]
    public class SoundAudioClip
    {
        public Sound audioSource;
        public AudioClip audioClips;
    }
}

public static class ScenesConstant
{
    public static string Main = "Game";
    public static string Level1 = "Level1";
    public static string Level2 = "Level2";
    public static string Level3 = "Level3";
    public static string Level4 = "Level4";
    public static string Level5 = "Level5";
}

public static class Tag
{
    public static string FruitObject = "fruitObject";
    public static string Board = "board";
    public static string Ground = "ground";
}

public static class CanvasConstant
{
    public static string Home = "Home";
    public static string LevelSelection = "LevelSelection";
    public static string EndScreen = "EndScreen";
    public static string CanvasManager = "CanvasManager";
    public static string Instruction = "Instruction";
}

public static class PlayerPrefConstant
{
    public static string UnlockedLevel = "UnlockedLevel";
    public static string HighestScore = "HighestScore";
}


public class QuestionConstant : MonoBehaviour

{
    [System.Serializable]
    public class JsonData
    {
        [JsonProperty("Shoot the Price")]
        public Modules ShootThePrice { get; set; }
    }

    [System.Serializable]
    public class Modules
    {
        [JsonProperty(PropertyName = "Physics")]
        public List<QuestionData> Module { get; set; }

        [JsonProperty(PropertyName = "Chemistry")]
        public List<QuestionData> Module1 { set { Module = value; } }

        [JsonProperty(PropertyName = "Biology")]
        public List<QuestionData> Module2 { set { Module = value; } }

        [JsonProperty(PropertyName = "Astronomy")]
        public List<QuestionData> Module3 { set { Module = value; } }

        [JsonProperty(PropertyName = "Zoology")]
        public List<QuestionData> Module4 { set { Module = value; } }
    }

    [System.Serializable]
    public class QuestionData
    {
        [JsonProperty("Question")]
        public string QuestionText { get; set; }

        [JsonProperty("Correct")]
        public string CorrectAnswer { get; set; }

        [JsonProperty("Incorrect1")]
        public string IncorrectAnswer1 { get; set; }

        [JsonProperty("Incorrect2")]
        public string IncorrectAnswer2 { get; set; }

        [JsonProperty("Incorrect3")]
        public string IncorrectAnswer3 { get; set; }

        [JsonProperty("Image")]
        public string Image { get; set; }

        [JsonProperty("Suggestion")]
        public string Suggestion { get; set; }
    }

    public class Question
    {
        public string QuestionText { get; set; }
        public List<string> Answers { get; set; }
        public int CorrectAnswerIndex { get; set; }
    }
}
