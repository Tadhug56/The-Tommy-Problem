using UnityEngine;
using TMPro;
using System.Collections;

public class GameplayUI : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public int startTime = 30;

    public static float countDownSpeed = 1;
    public float remainingTime;

    public TextMeshProUGUI killCountText;

    public int killCount = 0;

    private bool running = false;

    void Awake()
    {
        ResetTimer();
        killCount = 0;
    }

    private IEnumerator TickDown()
    {
        running = true;

        while(remainingTime > 0.0f)
        {
            yield return new WaitForSeconds(1.0f / countDownSpeed);
            remainingTime -= 1;
            UpdateTimerUI();
        }

        running = false;
        FindObjectOfType<Player>().PlayerLose();
    }

    void UpdateTimerUI()
    {
        int display = Mathf.CeilToInt(remainingTime);
        timerText.text = "Self destruct in : " + display.ToString();
    }

    public void UpdateKillCountUI()
    {
        killCount += 1;
        killCountText.text = "Sacrifices : " + killCount.ToString();
    }

    public void ResetTimer()
    {
        remainingTime = startTime;
        UpdateTimerUI();
        StartCoroutine(TickDown());
    }

    public void ResetKillCount()
    {
        killCount = -1; // Set to -1 so that the subsequent UpdateKillCountUI() resets it to 0
        UpdateKillCountUI();
    }
}
