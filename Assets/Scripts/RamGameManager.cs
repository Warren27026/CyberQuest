using UnityEngine;
using System.Collections.Generic;

public class RamGameManager : MonoBehaviour
{
    [Header("Timer")]
    public float timeLimit = 90f;   
    private float timer;
    private bool gameRunning = false;

    [Header("Gameplay")]
    public List<SlotSocket> slots;

    [Header("Timer Reference")]
    public RamTimer ramTimer;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip successClip;
    public AudioClip failClip;

    [Header("Final Mascotte")]
    public GameObject finalMascotte;   // le GameObject "mascotte"
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
        timer = timeLimit;
        gameRunning = true;

        if (winPanel) winPanel.SetActive(false);
        if (losePanel) losePanel.SetActive(false);

        // Mascotte finale cachée au départ
        if (finalMascotte)
            finalMascotte.SetActive(false);
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

    // Mascotte finale

    void ShowFinalMascotte(bool happy)
    {
        if (!finalMascotte) return;

        finalMascotte.SetActive(true);

        if (finalFaceHappy)
            finalFaceHappy.SetActive(happy);

        if (finalFaceSad)
            finalFaceSad.SetActive(!happy);
    }

    // Sons finaux

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

    // Sons intermédiaires (si tu les utilises ailleurs)

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
