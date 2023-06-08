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
