using UnityEngine;
using TMPro;

public class RamTimer : MonoBehaviour
{
    [Header("Timer Settings")]
    public float timeLimit = 90f;   // 1 min 30s
    private float timer;
    private bool isRunning = false;

    [Header("UI")]
    public TextMeshProUGUI timerText;

    [Header("Colors")]
    public Color goodColor = new Color(0.2f, 1f, 0.2f);   // vert
    public Color warningColor = new Color(1f, 0.6f, 0f); // orange
    public Color dangerColor = new Color(1f, 0.2f, 0.2f); // rouge

    void Start()
    {
        ResetTimer();
        StartTimer();
    }

    void Update()
    {
        if (!isRunning) return;

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            timer = 0f;
            StopTimer();
        }

        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);

        timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");

        // Couleur dynamique
        if (timer <= 10f)
            timerText.color = dangerColor;
        else if (timer <= 30f)
            timerText.color = warningColor;
        else
            timerText.color = goodColor;
    }

    //API PUBLIQUE

    public void StartTimer()
    {
        isRunning = true;
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public void ResetTimer()
    {
        timer = timeLimit;
        UpdateDisplay();
    }

    public float GetRemainingTime()
    {
        return timer;
    }
}




