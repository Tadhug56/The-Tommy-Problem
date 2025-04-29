using UnityEngine;
using TMPro;
using System.Collections;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public int startTime = 30;

    public static float countDownSpeed = 1;
    public static float remainingTime;

    private bool running = false;

    void Awake()
    {
        ResetTimer();
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

    public void ResetTimer()
    {
        remainingTime = startTime;
        UpdateTimerUI();
        StartCoroutine(TickDown());
    }
}
