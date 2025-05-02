using UnityEngine;
using TMPro;
using System.Collections;

// GameplayUI class that handles the UI for the gameplay
public class GameplayUI : MonoBehaviour
{
    // Timer Variables
    public TextMeshProUGUI timerText; // Self destruct timer text
    public int startTime = 30; // Start time for the timer
    public static float countDownSpeed = 1; // How fast to countdown (Not actually implemented yet)
    public float remainingTime; // Actual timer value

    // Kill Count Variables
    public TextMeshProUGUI killCountText; // Text for kill count
    public int killCount = 0; // Kill count value
    
    // When turned on reset the timer when turned on and the killCount (UI method called in CutsceneManager)
    void Awake()
    {
        ResetTimer(); // Reset the timer
        killCount = 0; // Set the kill count to 0
    }

    // Tick down the timer every second
    private IEnumerator TickDown()
    {
        // While the player still has time
        while(remainingTime > 0.0f)
        {
            yield return new WaitForSeconds(1.0f / countDownSpeed); // Count down a second divided by the countDownSpeed
            remainingTime -= 1; // Minus the second
            UpdateTimerUI(); // Update the UI
        }

        FindObjectOfType<Player>().PlayerLose(); // Call PlayerLose() if out of time
    }

    // Update the timer UI
    public void UpdateTimerUI()
    {
        int display = Mathf.CeilToInt(remainingTime); // Set an integer readable time (Can cause a bug where it ticks down in 2's) Chatgpt recommended CeilToInt()
        timerText.text = "Self destruct in : " + display.ToString(); // Update the timer text
    }

    // Update the kill count UI
    public void UpdateKillCountUI()
    {
        killCount += 1; // Increase the count by 1
        killCountText.text = "Sacrifices : " + killCount.ToString(); // Update the UI
    }

    // Reset the timer
    public void ResetTimer()
    {
        remainingTime = startTime; // Set time to start value
        UpdateTimerUI(); // Update the UI
        StartCoroutine(TickDown()); // Start the ticking down again
    }

    // Reset the kill count
    public void ResetKillCount()
    {
        killCount = -1; // Set to -1 so that the subsequent UpdateKillCountUI() resets it to 0
        UpdateKillCountUI(); // Update the UI
    }
}
