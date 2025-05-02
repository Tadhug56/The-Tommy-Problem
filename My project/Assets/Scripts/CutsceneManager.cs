using System.Collections;
using UnityEngine;
using TMPro;
using System;

public class CutsceneManager : MonoBehaviour
{
    // General globalish Variables
    public GameObject mainCamera; // Main camera
    public GameObject spawnManager; // Spawn manager

    // Main menu variables
    public GameObject menuUI; // Menu canvas

    // Cutscene variables
    public GameObject cutsceneCamera; // Cutscene camera
    public Transform cameraTransform; // Cutscene camera's Transform
    public GameObject cutsceneUI; // Cutscene canvas
    public GameObject gameplayUI; // Gameplay canvas
    public float cameraMoveSpeed = 1.0f;
    public float timeBetweenSubtitles = 10f;
    private int sacrifices = 0;

        // Opening Cutscene 
        public Transform openingCameraPosition;
        public Vector3 openingCameraEndPosition;
        private bool openingCutsceneStarted = false;
        public string[] subtitlesOpening;
        public TextMeshProUGUI subtitleText;
    
        // Death cutscene

        public GameObject deathCutscneUI;
        public Transform deathCameraPosition;
        public Vector3 deathCameraEndPosition;
        public bool deathCutsceneStarted;
        public string[] subtitlesDeath;
        public TextMeshProUGUI deathSubtitleText;

    // When we turn on the cutscene manager script (Means our loop continues automatically by switching the object with the script on and off)
    void Awake()
    {
        // If subtitles haven't been assigned (Don't think it's always neccesary to check but broke once without check)
        if (subtitlesOpening == null || subtitlesOpening.Length == 0)
        {
            // Create the array and assign the subtitles
            subtitlesOpening = new[]
            {
                "My name is Tommy",
                "And until recently I wasn't alive",
                "I think I preferred it that way…"
            };
        }

        // Do the same for the death cutscene
        if (subtitlesDeath == null || subtitlesDeath.Length == 0)
        {
            subtitlesDeath = new[]
            {
                "A moralist huh?",
                "Hard to argue with a utilitarian",
                "Just try not to think too hard about it",
                "Self preservation.. I guess..",
                "There's no rationalising this..."
            };
        }
    }

    // Every frame check if we need to be playing a cutscene
    void Update()
    {
        // If we should be playing the opening cutscene move the camera
        if (cameraTransform != null && openingCutsceneStarted)
        {
            openingCameraMovement(); // Move the camera
        }

        // If we should be playing the death cutscene move the camera
        if (cameraTransform != null && deathCutsceneStarted)
        {
            deathCameraMovement(); // Move the camera
        }
    }

    // OPENING CUTSCENE

    // Called by the menu and handles setting specific UI elements on and off
    public void StartOpeningCutscene()
    {
        openingCutsceneStarted = true; // Set the condition variable to true

        // Hide the menu
        menuUI.SetActive(false);
        mainCamera.SetActive(false);

        // Start the cutscene
        cutsceneCamera.SetActive(true);
        cutsceneUI.SetActive(true);


        StartCoroutine(PlayOpeningCutscene(openingCameraPosition)); // Start the cutscene proper
    }

    // Play the cutscene and handle the subtitles / camera movement
    private IEnumerator PlayOpeningCutscene(Transform targetPose)
    {
        // Set the camera position and rotation
        cutsceneCamera.transform.position = openingCameraPosition.position;
        cutsceneCamera.transform.rotation = openingCameraPosition.rotation;

        // Show each subtitle for the specified length in order
        for(int i = 0; i < subtitlesOpening.Length; i++)
        {
            subtitleText.text = subtitlesOpening[i];
            yield return new WaitForSeconds(timeBetweenSubtitles);
        }

        EndOpeningCutscene(); // End the cutscene
    }

    // Move the camera to the set endpoint
    private void openingCameraMovement()
    {
        cameraTransform.position = Vector3.MoveTowards(
                cameraTransform.position,
                openingCameraEndPosition,
                cameraMoveSpeed * Time.deltaTime
            );
    }

    // End the cutscene, handling turning on and off UI elements and script resets for game retries
    private void EndOpeningCutscene()
    {
        openingCutsceneStarted = false; // Set the condition variable to false

        // Turn main camera on (Handle first before turnng off other camera)
        mainCamera.SetActive(true);

        // Turn off cutscene camera and UI
        cutsceneCamera.SetActive(false);
        cutsceneUI.SetActive(false);

        // Turn on gameplay related UI
        gameplayUI.SetActive(true);

        // Reset UI kill counter and timer
        GameplayUI gameplayUIScript = FindObjectOfType<GameplayUI>();
        gameplayUIScript.ResetTimer();
        gameplayUIScript.ResetKillCount();

        // Turn on gameplay elements
        FindObjectOfType<Player>().playable = true; // Set playable to true (Game is on)
        spawnManager.SetActive(true); // Turn on the spawn manager
    }

    // DEATH CUTSCENE

    // Start
    public void StartDeathCutscene()
    {
        deathCutsceneStarted = true; // Set the condition variable to true

        // Handle turning on death cutscene UI
        deathCutscneUI.SetActive(true);

        // Handle Spawn manager
        spawnManager.GetComponent<SpawnManager>().ClearGameplay(); // Clear all spawn manager instantiated objects
        spawnManager.SetActive(false); // Turn off spawn manager

        // Handle gameplay UI
        sacrifices = gameplayUI.GetComponent<GameplayUI>().killCount; // Update sacrifices for death cutscene subtitle result
        gameplayUI.SetActive(false); // Turn off gameplay UI

        StartCoroutine(PlayDeathCutscene()); // Play the death cutscene proper
    }

    // Moves the camera to the specified position and display's the subtitle the user earned on their run
    private IEnumerator PlayDeathCutscene()
    {
        // Set the camera position and rotation
        cutsceneCamera.transform.position = deathCameraPosition.position;
        cutsceneCamera.transform.rotation = deathCameraPosition.rotation;
        cutsceneCamera.SetActive(true); // Turn on cutscene camera

        // If no kills
        if(sacrifices == 0)
        {
            deathSubtitleText.text = subtitlesDeath[0]; // Moralist
        }

        // If 1 kill
        else if(sacrifices == 1)
        {
            deathSubtitleText.text = subtitlesDeath[1]; // Utilitarian
        }

        // If between 1 - 10 kills
        else if(sacrifices > 1 && sacrifices < 10)
        {
            deathSubtitleText.text = subtitlesDeath[2]; // Clueless panic
        }

        // If between 10 - 50 kills
        else if(sacrifices > 10 && sacrifices < 50)
        {
            deathSubtitleText.text = subtitlesDeath[3]; // Self preservation
        }

        // Else more than 50
        else
        {
            deathSubtitleText.text = subtitlesDeath[4]; // Monster
        }
            
        yield return new WaitForSeconds(timeBetweenSubtitles); // Let the subtitle play

        EndDeathCutscene(); // Handle the end of the cutscene
    }

    // Moves the camera to the set position smoothly
    private void deathCameraMovement()
    {
        cameraTransform.position = Vector3.MoveTowards(
                cameraTransform.position,
                deathCameraEndPosition,
                cameraMoveSpeed * Time.deltaTime
            );
    }

    // Handle the end of the death cutscene
    private void EndDeathCutscene()
    {
        deathCutsceneStarted = false; // Set the condition variable to false

        // Handle UI elements
        deathCutscneUI.SetActive(false);
        cutsceneUI.SetActive(false);
        menuUI.SetActive(true);
        
        EnemyShooter enemyShooterScript = FindObjectOfType<EnemyShooter>(); // Get an enemy shooter script

        // Have to check if there is one as a player could die without one instantiated
        if(enemyShooterScript)
        {
            enemyShooterScript.ResetFirerate(); // Reset enemy shooter firerate
        }
    }
}
