using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIObjectCollision : MonoBehaviour
{
    public string imageIdentifierTag;
    Level level;
    AudioManager audioManager;
    GameManager gameManager;
    EndScreen endScreen;
    SlingshotController slingshotController;

    private void Awake()
    {
        level = FindObjectOfType<Level>();
        audioManager = FindObjectOfType<AudioManager>();
        endScreen = FindObjectOfType<EndScreen>();
        slingshotController = FindObjectOfType<SlingshotController>();
    }

    internal void SetImageIdentifierTag(string tag)
    {   
        imageIdentifierTag = tag;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object has a UI Image component
        Image uiImage = collision.gameObject.GetComponent<Image>();

        if (uiImage != null)
        {
            // Destroy the UI Image component's game object
            if (uiImage.tag == imageIdentifierTag)
            {
                TextMeshProUGUI imageText = uiImage.GetComponentInChildren<TextMeshProUGUI>();
                if (imageText != null)
                {
                    level.CheckAnswer(imageText.text);
                }

                //slingshotController.Explosion();
                Destroy(uiImage.gameObject);
                audioManager.PlaySound(AudioConstant.Sound.EggDropped);

                // Destroy the collided 2D clone object
                Destroy(gameObject);
                CheckLives();
            }
            else
            {
                // Destroy the collided 2D clone object
                if (gameObject.tag == Tag.FruitObject)
                {
                    audioManager.PlaySound(AudioConstant.Sound.EggDropped);
                    Destroy(gameObject, 1f);
                    CheckLives();
                }
            }
        }
    }

    void CheckLives()
    {
        if (SlingshotController.GetLivesCount() <= 0)
        {
            GameManager.correctAnswerCount = level.correctAnswerCount;
            GameManager.activeCanvas = CanvasConstant.EndScreen;
            SceneManager.LoadScene(ScenesConstant.Main);
        }
    }

    //void Contacts(Collision2D collision)
    //{
    //    foreach (ContactPoint2D contact in collision.contacts)
    //    {
    //        GameObject otherObject = contact.collider.gameObject;

    //        // Check if the other object is the ground object
    //        if (otherObject.CompareTag("ground"))
    //        {
    //            // The current object is touching the ground object
    //            // Perform your desired actions here

    //            // Access the unique ID of the ground object
    //            GameObject groundObject = otherObject.GetComponent<GameObject>();
    //            if (groundObject != null)
    //            {
    //                Debug.Log(groundObject.gameObject.tag);
    //            }
    //        }
    //    }
    //}
}
