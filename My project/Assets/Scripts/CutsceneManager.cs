using System.Collections;
using UnityEngine;
using TMPro;
using System;

public class CutsceneManager : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject menuUI;
    public GameObject cutsceneCamera;

    public GameObject spawnManager;

    public GameObject cutsceneUI;
    public GameObject gameplayUI;
    public Transform cameraTransform;

    public Transform openingCameraPosition;
    public Vector3 openingCameraEndPosition;
    private bool openingCutsceneStarted = false;
    public string[] subtitlesOpening;
    
    // Death cutscene
    public Transform deathCameraPosition;
    public Vector3 deathCameraEndPosition;
    public bool deathCutsceneStarted;
    public string[] subtitlesDeath;
    
    public float cameraMoveSpeed = 1.0f;

    public TextMeshProUGUI subtitleText;
    
    public float timeBetweenSubtitles = 10f;

    void Awake()
    {
        if (subtitlesOpening == null || subtitlesOpening.Length == 0)
        {
            subtitlesOpening = new[]
            {
                "My name is Tommy",
                "And until recently I wasn't alive",
                "I think I preferred it that way…"
            };
        }
    }

    void Update()
    {
        // Move camera during cutscene
        if (cameraTransform != null && openingCutsceneStarted)
        {
            openingCameraMovement();
        }

        // Move camera during cutscene
        if (cameraTransform != null && deathCutsceneStarted)
        {
            deathCameraMovement();
        }
    }

    public void StartOpeningCutscene()
    {
        openingCutsceneStarted = true;

        // Hide the menu
        menuUI.SetActive(false);
        mainCamera.SetActive(false);

        // Start the cutscene
        cutsceneCamera.SetActive(true);
        cutsceneUI.SetActive(true);


        StartCoroutine(PlayOpeningCutscene(openingCameraPosition));
    }

    private IEnumerator PlayOpeningCutscene(Transform targetPose)
    {
        cutsceneCamera.transform.position = openingCameraPosition.position;
        cutsceneCamera.transform.rotation = openingCameraPosition.rotation;
        // show each subtitle
        for(int i = 0; i < subtitlesOpening.Length; i++)
        {
            subtitleText.text = subtitlesOpening[i];
            yield return new WaitForSeconds(timeBetweenSubtitles);
            //Debug.Log("Line should have plauyed : " + subtitleText.text);
        }

        EndOpeningCutscene();
    }

    private void openingCameraMovement()
    {
        cameraTransform.position = Vector3.MoveTowards(
                cameraTransform.position,
                openingCameraEndPosition,
                cameraMoveSpeed * Time.deltaTime
            );
    }

    private void EndOpeningCutscene()
    {
        openingCutsceneStarted = false;

        mainCamera.SetActive(true);

        cutsceneCamera.SetActive(false);
        cutsceneUI.SetActive(false);

        gameplayUI.SetActive(true);
        FindObjectOfType<Timer>().ResetTimer();

        spawnManager.SetActive(true);
    }

    // DEATH CUTSCENE

    public void StartDeathCutscene()
    {
        deathCutsceneStarted = true;
        spawnManager.GetComponent<SpawnManager>().ClearGameplay();
        spawnManager.SetActive(false);
        gameplayUI.SetActive(false);
       
        subtitlesDeath = new[]
        {
            "It's...",
            "It's .. over.."
        };

        StartCoroutine(PlayDeathCutscene());
    }

    private IEnumerator PlayDeathCutscene()
    {
        cutsceneCamera.transform.position = deathCameraPosition.position;
        cutsceneCamera.transform.rotation = deathCameraPosition.rotation;
        cutsceneCamera.SetActive(true);

        // Show each subtitle
        for(int i = 0; i < subtitlesDeath.Length; i++)
        {
            subtitleText.text = subtitlesDeath[i];
            yield return new WaitForSeconds(timeBetweenSubtitles);
            //Debug.Log("Line should have plauyed : " + subtitleText.text);
        }

        EndDeathCutscene();
    }

    private void deathCameraMovement()
    {
        cameraTransform.position = Vector3.MoveTowards(
                cameraTransform.position,
                deathCameraEndPosition,
                cameraMoveSpeed * Time.deltaTime
            );
    }

    private void EndDeathCutscene()
    {
        deathCutsceneStarted = false;
        cutsceneUI.SetActive(false);
        menuUI.SetActive(true);
    }
}
