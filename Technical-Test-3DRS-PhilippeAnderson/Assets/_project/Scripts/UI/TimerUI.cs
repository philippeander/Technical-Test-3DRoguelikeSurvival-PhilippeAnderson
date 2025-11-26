using UnityEngine;
using TMPro;

public class TimerUI : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public bool countUp = true;
    public float startTime = 0f;

    private float currentTime;
    private bool isPaused = false;

    private void Start()
    {
        currentTime = startTime;
        UpdateUI();
    }

    private void Update()
    {
        if (isPaused)
            return;

        if (countUp)
            currentTime += Time.deltaTime;
        else
            currentTime = Mathf.Max(0, currentTime - Time.deltaTime);

        UpdateUI();
    }

    private void UpdateUI()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);

        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    public void PauseTime()
    {
        isPaused = true;
    }

    public void ResumeTime()
    {
        isPaused = false;
    }

    public void ResetTimer(float newTime = 0f)
    {
        currentTime = newTime;
        UpdateUI();
    }

    public float GetTime() => currentTime;
}