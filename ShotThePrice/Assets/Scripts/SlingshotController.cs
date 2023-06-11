using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;

public class SlingshotController : MonoBehaviour
{
    private AudioManager audioManager;
    private Level level;

    public LineRenderer[] lineRenderers;
    public Transform[] stripPositions;
    public Transform center;
    public Transform idlePosition;
    public Transform fruitContainer;

    public Vector3 currentPosition;
    public float maxLength;
    public float bottomBoundary;
    bool isMouseDown;

    public GameObject fruitPrefabs;

    Rigidbody2D fruitRigibody;
    Collider2D fruitCollider;
    ParticleSystem explosion;

    public float fruitPositionOffset;
    public float force;

    static int totalLives = 5;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        level = FindObjectOfType<Level>();

        lineRenderers[0].positionCount = 2;
        lineRenderers[1].positionCount = 2;
        lineRenderers[0].SetPosition(0, stripPositions[0].position);
        lineRenderers[1].SetPosition(0, stripPositions[1].position);

        CreateObject();
    }

    void Update()
    {
        if (isMouseDown)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10;

            currentPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            currentPosition = center.position + Vector3.ClampMagnitude(currentPosition - center.position, maxLength);

            currentPosition = ClampBoundry(currentPosition);

            SetStrips(currentPosition);

            if (fruitCollider)
            {
                fruitCollider.enabled = true;
            }
        }
        else
        {
            ResetStrips();
        }
    }

    void CreateObject()
    {
        fruitRigibody = Instantiate(fruitPrefabs).GetComponent<Rigidbody2D>();
        fruitCollider = fruitRigibody.GetComponent<Collider2D>();
        fruitCollider.enabled = false;

        fruitRigibody.isKinematic = true;

        fruitRigibody.tag = Tag.FruitObject;

        UIObjectCollision uiObjCollider = fruitRigibody.gameObject.AddComponent<UIObjectCollision>();
        uiObjCollider.SetImageIdentifierTag(Tag.Board);

        //explosion = Instantiate(applePrefab.GetComponentInChildren<ParticleSystem>(), apple.transform);
        //explosion.transform.localPosition = Vector3.zero;
        //explosion.Play();

        ResetStrips();
    }

    private void OnMouseDown()
    {
        audioManager.PlaySound(AudioConstant.Sound.Stretch);
        isMouseDown = true;
    }

    private void OnMouseUp()
    {
        audioManager.PlaySound(AudioConstant.Sound.Release);
        isMouseDown = false;
        Shoot();
    }

    void Shoot()
    {
        if (fruitRigibody)
        {
            SaveLivesCount();
            UpdateFruitLives();
            fruitRigibody.isKinematic = false;
            Vector3 appleForce = ((currentPosition - center.position) * force * -1);
            fruitRigibody.velocity = appleForce;
            fruitCollider.enabled = true;

            fruitRigibody = null;
            fruitCollider = null;

            Invoke("CreateObject", 2);
        }
    }

    void ResetStrips()
    {
        currentPosition = idlePosition.position;
        SetStrips(idlePosition.position);
    }

    void SetStrips(Vector3 position)
    {
        lineRenderers[0].SetPosition(1, position);
        lineRenderers[1].SetPosition(1, position);

        if (fruitRigibody)
        {
            Vector3 direction = position - center.position;
            fruitRigibody.transform.position = position + direction.normalized * fruitPositionOffset;
            fruitRigibody.transform.right = -direction.normalized;
        }
    }

    Vector3 ClampBoundry(Vector3 vector)
    {
        vector.y = Mathf.Clamp(vector.y, bottomBoundary, 1000);
        return vector;
    }

    public static void SaveLivesCount()
    {
        totalLives--;
    }

    public static int GetLivesCount()
    {
        return totalLives;
    }

    public static void ResetLives()
    {
        totalLives = 5;
    }

    public void UpdateFruitLives()
    {
        int totalfruit = fruitContainer.childCount;

        // Iterate through each star and set its active state based on the current number of points
        for (int i = 0; i < totalfruit; i++)
        {
            Transform fruit = fruitContainer.GetChild(i);
            fruit.gameObject.SetActive(i < (SlingshotController.GetLivesCount() - 1));
        }
    }

    [ContextMenu("CorrectAnswerByPass")]
    public void CorrectAnswerByPass()
    {
        Shoot();
        level.correctAnswerCount++;
    }

}

public static class RaycastUtilities
{
    public static bool PointerIsOverUI(Vector2 screenPos)
    {
        var hitObject = UIRaycast(ScreenPosToPointerData(screenPos));
        return hitObject != null && hitObject.layer == LayerMask.NameToLayer("UI");
    }

    static GameObject UIRaycast(PointerEventData pointerData)
    {
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        return results.Count < 1 ? null : results[0].gameObject;
    }

    static PointerEventData ScreenPosToPointerData(Vector2 screenPos)
       => new(EventSystem.current) { position = screenPos };

}