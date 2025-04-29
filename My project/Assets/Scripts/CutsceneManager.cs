using System.Collections;
using UnityEngine;
using TMPro;
using System;

public class CutsceneManager : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject cutsceneCamera;

    public GameObject spawnManager;

    public GameObject cutsceneUI;
    public Transform cameraTransform;
    public Vector3 cameraEndPosition;
    public float cameraMoveSpeed = 1.0f;

    public TextMeshProUGUI subtitleText;
    public string[] subtitles;
    public float timeBetweenSubtitles = 10f;

    private bool cutsceneStarted = false;

    void Awake()
    {
        // if you prefer to set in code instead of Inspector:
        if (subtitles == null || subtitles.Length == 0)
        {
            subtitles = new[]
            {
                "My name is Tommy",
                "And until recently I wasn't alive",
                "I think I preferred it that way…"
            };
        }
    }

    public void StartCutscene()
    {
        cutsceneStarted = true;
        StartCoroutine(PlayCutscene());
    }

    private IEnumerator PlayCutscene()
    {
        // show each subtitle
        for(int i = 0; i < subtitles.Length; i++)
        {
            subtitleText.text = subtitles[i];
            yield return new WaitForSeconds(timeBetweenSubtitles);
            //Debug.Log("Line should have plauyed : " + subtitleText.text);
        }

        // end of cutscene: you could now load your game scene or re-enable gameplay
        EndCutscene();
    }

    void Update()
    {
        // Move camera during cutscene
        if (cameraTransform != null && cutsceneStarted)
        {
            cameraTransform.position = Vector3.MoveTowards(
                cameraTransform.position,
                cameraEndPosition,
                cameraMoveSpeed * Time.deltaTime
            );
        }
    }

    private void EndCutscene()
    {
        mainCamera.SetActive(true);
        cutsceneCamera.SetActive(false);
        cutsceneUI.SetActive(false);
        spawnManager.SetActive(true);
        // e.g. load gameplay, or re-enable main camera:
        // FindObjectOfType<MainMenu>().… or SceneManager.LoadScene(...)
        //Debug.Log("Cutscene complete");
    }
}
