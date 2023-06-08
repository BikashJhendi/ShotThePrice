using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioConstant.SoundAudioClip[] soundAudioClipArray;
    public bool isMuted = false;
    public Sprite mute;
    public Sprite unMute;
    private Button muteButton;

    private void Awake()
    {
        muteButton = FindObjectOfType<Button>();
    }

    public void PlaySound(AudioConstant.Sound sound)
    {
        if (!isMuted)
        {
            GameObject soundObject = new GameObject("Sound");
            AudioSource audioSource = soundObject.AddComponent<AudioSource>();
            audioSource.PlayOneShot(GetAudioClip(sound));
        }
    }

    private AudioClip GetAudioClip(AudioConstant.Sound sound)
    {
        foreach (AudioConstant.SoundAudioClip item in soundAudioClipArray)
        {
            if (item.audioSource == sound)
            {
                return item.audioClips;
            }
        }

        return null;
    }

    [ContextMenu("Mute")]
    public void ToggleMute()
    {
        if (isMuted)
        {
            isMuted = false;
            // muteButton.image.sprite = unMute;
        }
        else
        {
            PlaySound(AudioConstant.Sound.Click);
            isMuted = true;
            // muteButton.image.sprite = mute;
        }
    }

}
