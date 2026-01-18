//gestion du timer qui est au dessus de la RAM
using UnityEngine;
using TMPro;

public class RamTimer : MonoBehaviour
{
    [Header("Timer Settings")]
    public float timeLimit = 90f;   //duree de la tache 1 min 30s
    private float timer;
    private bool isRunning = false;

    [Header("UI")]
    public TextMeshProUGUI timerText;

    //les couleurs du timer selon le temps restant:
    [Header("Colors")]
    public Color goodColor = new Color(0.2f, 1f, 0.2f);   // vert
    public Color warningColor = new Color(1f, 0.6f, 0f); // orange
    public Color dangerColor = new Color(1f, 0.2f, 0.2f); // rouge

    void Start()
    {
        //on initialise le timer mais on ne le lance pas des le debut du jeu
        ResetTimer();
        StopTimer();
    }

    //cette fonction est appelee a chaque frame pour mettre a jour le timer
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
    //met a jour l'affichage du timer
    void UpdateDisplay()
    {
        if (!timerText) return;

        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);

        timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");

        //changement de couleur dynamique
        if (timer <= 10f)
            timerText.color = dangerColor;
        else if (timer <= 30f)
            timerText.color = warningColor;
        else
            timerText.color = goodColor;
    }

    //api pour demarrer/arreter/reset le timer

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




