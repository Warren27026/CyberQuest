using UnityEngine;
using System.Collections.Generic;

public class RamGameManager : MonoBehaviour
{
    [Header("Timer")]
    public float timeLimit = 300f;   // 5 minutes
    private float timer;
    private bool gameRunning = false;

    [Header("Gameplay")]
    public List<Slot> slots = new List<Slot>();

    [Header("Mascotte")]
    public MascotteSimple mascotte;

    [Header("Result UI")]
    public GameObject winPanel;
    public GameObject losePanel;

    void Start()
    {
        timer = timeLimit;

        // Sécurité : cacher les panels au démarrage
        if (winPanel) winPanel.SetActive(false);
        if (losePanel) losePanel.SetActive(false);
    }

    // Appelé quand le joueur clique "Je commence le patch"
    public void StartGame()
    {
        gameRunning = true;
        timer = timeLimit;
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
        foreach (Slot s in slots)
        {
            if (!s.isOccupied)
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

            if (mascotte)
            {
                mascotte.ShowAtStoryPoint();
                mascotte.SetMood(MascotteMood.Happy);
                mascotte.Say("Bravo ! Tu as réparé la mémoire !");
            }
        }
        else
        {
            if (losePanel) losePanel.SetActive(true);

            if (mascotte)
            {
                mascotte.ShowAtStoryPoint();
                mascotte.SetMood(MascotteMood.Anx);
                mascotte.Say("Oh non... Le temps est écoulé...");
            }
        }
    }
}


