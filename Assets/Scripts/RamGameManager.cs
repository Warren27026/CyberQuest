using UnityEngine;
using System.Collections.Generic;

public class RamGameManager : MonoBehaviour
{
    [Header("Timer")]
    public float timeLimit = 500f;   
    private float timer;
    private bool gameRunning = false;

    [Header("Gameplay")]
    public List<SlotSocket> slots;


    [Header("Mascotte")]
    public MascotteSimple mascotte;

    [Header("Result UI")]
    public GameObject winPanel;
    public GameObject losePanel;

    void Start()
    {
        timer = timeLimit;
        gameRunning = true;   //Démarrage automatique

        if (winPanel) winPanel.SetActive(false);
        if (losePanel) losePanel.SetActive(false);
    }

    void Update()
    {
        if (!gameRunning) return;

        timer -= Time.deltaTime;

        if (CheckVictory())
        {
            EndGame(true);
        }
        else if (timer <= 0f)
        {
            EndGame(false);
        }
    }

    bool CheckVictory()
    {
        foreach (SlotSocket s in slots)
        {
            if (!s.isCorrect)
                return false;
        }
        return true;
    }



    void EndGame(bool success)
    {
        gameRunning = false;

        if (success)
        {
            if (winPanel) winPanel.SetActive(true);

        }
        else
        {
            if (losePanel) losePanel.SetActive(true);


        }
    }
}

