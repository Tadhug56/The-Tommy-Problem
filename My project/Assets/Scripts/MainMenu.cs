using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {   
        FindObjectOfType<Player>().ResetPlayer();
        GroundTile.speed = GroundTile.startingSpeed;
        FindObjectOfType<CutsceneManager>().StartOpeningCutscene();// Start the opening cutscene
    }

    public void ExitGame()
    {
        Debug.Log("Exiting Game...");
        Application.Quit();
    }
}
