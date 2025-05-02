using UnityEngine;
using UnityEngine.SceneManagement;

// MainMenu class that handles the menu logic
public class MainMenu : MonoBehaviour
{
    // Start the game when the start button is pressed
    public void StartGame()
    {   
        FindObjectOfType<Player>().ResetPlayer(); // Reset the player
        GroundTile.speed = GroundTile.startingSpeed; // Reset the speed
        FindObjectOfType<CutsceneManager>().StartOpeningCutscene();// Start the opening cutscene
    }

    // Exit the game
    public void ExitGame()
    {
        Application.Quit(); // Quit the application
    }
}
