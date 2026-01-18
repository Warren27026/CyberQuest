//script de gestion du jeu RAM
using UnityEngine;
using System.Collections.Generic;

public class RamGameManager : MonoBehaviour
{
    [Header("Timer")]
    public float timeLimit = 90f;   
    private float timer;
    private bool gameRunning = false;
    public bool IsGameRunning => gameRunning;

    

    [Header("Gameplay")]
    public List<SlotSocket> slots;

    [Header("Timer Reference")]
    public RamTimer ramTimer;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip successClip;
    public AudioClip failClip;

    [Header("Final Mascotte")]
    public GameObject finalMascotte;
    public GameObject finalFaceHappy;
    public GameObject finalFaceSad;

    [Header("Final Audio")]
    public AudioClip finalWinClip;
    public AudioClip finalLoseClip;

    [Header("Result UI")]
    public GameObject winPanel;
    public GameObject losePanel;

    


    void Start()
    {
        //le jeu ne demarre pas automatiquement (on attend la fin de l'exploration libre)
        gameRunning = false;
        timer = timeLimit;

        if (winPanel) winPanel.SetActive(false);
        if (losePanel) losePanel.SetActive(false);

        //mascotte cachee au depart
        if (finalMascotte)
            finalMascotte.SetActive(false);

        
    }

    //mise a jour du timer chaque frame pour voir si on arrete le jeu en cas de victoire ou defaite 
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

    //demarrer le jeu RAM
    public void StartGame()
    {
        //initialiser le timer
        timer = timeLimit;
        gameRunning = true;

        if (ramTimer != null)
            ramTimer.StartTimer();
    }

    //verifier si toutes les cases sont correctes:
    bool CheckVictory()
    {
        foreach (SlotSocket s in slots)
        {
            if (!s.isCorrect)
                return false;
        }
        return true;
    }
    
    //terminer le jeu RAM
    void EndGame(bool success)
    {
        gameRunning = false;

        if (ramTimer != null)
        {
            ramTimer.StopTimer();
            ramTimer.ResetTimer();
        }

        if (success)
        {
            if (winPanel) winPanel.SetActive(true);
            PlayFinalWin();
            ShowFinalMascotte(true);
        }
        else
        {
            if (losePanel) losePanel.SetActive(true);
            PlayFinalLose();
            ShowFinalMascotte(false);
        }
    }

    //afficher la mascotte finale avec l'expression appropriee
    void ShowFinalMascotte(bool happy)
    {
        if (!finalMascotte) return;

        finalMascotte.SetActive(true);

        if (finalFaceHappy)
            finalFaceHappy.SetActive(happy);

        if (finalFaceSad)
            finalFaceSad.SetActive(!happy);
    }

    
    //sons du feedback
    public void PlayFinalWin()
    {
        if (audioSource && finalWinClip)
            audioSource.PlayOneShot(finalWinClip);
    }

    public void PlayFinalLose()
    {
        if (audioSource && finalLoseClip)
            audioSource.PlayOneShot(finalLoseClip);
    }

    public void PlaySuccess()
    {
        if (audioSource && successClip)
            audioSource.PlayOneShot(successClip);
    }

    public void PlayFail()
    {
        if (audioSource && failClip)
            audioSource.PlayOneShot(failClip);
    }
}
