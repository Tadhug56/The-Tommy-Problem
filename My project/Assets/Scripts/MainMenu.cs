using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject menuUI;
    public GameObject mainCamera;
    public GameObject cutsceneCamera;
    public GameObject cutsceneUI;

    public void StartGame()
    {
        // Hide the menu
        menuUI.SetActive(false);
        mainCamera.SetActive(false);

        // Start the cutscene
        cutsceneCamera.SetActive(true);
        cutsceneUI.SetActive(true);

        // Optional: Start the cutscene script
        FindObjectOfType<CutsceneManager>().StartCutscene();
    }

    public void ExitGame()
    {
        Debug.Log("Exiting Game...");
        Application.Quit();
    }
}
