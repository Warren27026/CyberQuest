using UnityEngine;
using TMPro;
using System.Collections;

public class IntroFlow : MonoBehaviour
{
    [Header("Intro UI")]
    public GameObject introPanel;
    public TextMeshProUGUI introText;
    public GameObject btnNext;
    public GameObject btnCompris;
    public GameObject btnOk;
    public GameObject btnComprisCPU;
    public GameObject btnComprisDriver;
    public GameObject btnCommencerPatch;

    [Header("Mascotte Points")]
    public Transform cpuMascottePoint;
    public Transform driverMascottePoint;
    public Transform ramMascottePoint;

    [Header("Story Decision UI")]
    public GameObject storyDecisionCanvas;

    [Header("Refs")]
    public MascotteSimple mascotte;
    public TeleportSimple teleport;

    private int step = 0;

    void Start()
    {
        if (storyDecisionCanvas) storyDecisionCanvas.SetActive(false);
        if (btnCommencerPatch) btnCommencerPatch.SetActive(false);

        if (teleport != null) teleport.TeleportToEntry();
        ShowStep(0);
    }

    void ShowStep(int s)
    {
        step = s;

        if (btnNext) btnNext.SetActive(step == 0);
        if (btnCompris) btnCompris.SetActive(step == 1);
        if (btnOk) btnOk.SetActive(step == 2);

        if (introText == null) return;

        if (step == 0)
            introText.text = "Bienvenue à CyberQuest!\nLe jeu qui te met au sein d'une ville simulant les composants d'ordinateurs.";
        else if (step == 1)
            introText.text = "Pour montrer l’interface de jeu :\nfais un geste abracadabra et laisse le bouton maintenu.";
        else if (step == 2)
            introText.text = "La mascotte du jeu va s'afficher pour t'accompagner.";
    }

    public void BtnNext() => ShowStep(1);
    public void BtnCompris() => ShowStep(2);

    public void BtnOk()
    {
        if (introPanel) introPanel.SetActive(false);
        StartCoroutine(StartMascotteStory());
    }

    IEnumerator StartMascotteStory()
    {
        if (mascotte != null)
        {
            mascotte.ShowAtStoryPoint();
            yield return StartCoroutine(
                mascotte.StorySequence(() =>
                {
                    if (storyDecisionCanvas)
                        storyDecisionCanvas.SetActive(true);
                })
            );
        }
    }

    public void BtnYesStart()
    {
        if (storyDecisionCanvas) storyDecisionCanvas.SetActive(false);
        StartCPUStory();
    }

    public void StartCPUStory()
    {
        if (teleport != null) teleport.TeleportToCPU();
        if (btnComprisCPU) btnComprisCPU.SetActive(true);

        mascotte.ShowAt(cpuMascottePoint);
        mascotte.SetMood(MascotteMood.Happy);
        mascotte.Say("Voici le CPU\nIl est le cerveau de l’ordinateur.");
    }

    public void BtnComprisCPU()
    {
        StartDriverStory();
    }

    public void StartDriverStory()
    {
        if (btnComprisCPU) btnComprisCPU.SetActive(false);
        if (btnComprisDriver) btnComprisDriver.SetActive(true);

        if (teleport != null) teleport.TeleportToDriver();

        mascotte.ShowAt(driverMascottePoint);
        mascotte.SetMood(MascotteMood.Happy);
        mascotte.Say("Le disque dur est le lieu de \nstockage permanent des données.");
    }

    public void BtnComprisDriver()
    {
        if (teleport != null) teleport.TeleportToRAM();
        StartRamIntro();
    }

    public void StartRamIntro()
    {
        if (btnComprisDriver) btnComprisDriver.SetActive(false);
        StartCoroutine(RamSequence());
    }

    IEnumerator RamSequence()
    {
        mascotte.ShowAt(ramMascottePoint);
        mascotte.SetMood(MascotteMood.Happy);
        mascotte.Say("Voici la mémoire RAM\nElle stocke les données temporaires");
        yield return new WaitForSeconds(3f);

        mascotte.Say("Chaque donnée est stockée dans une cellule \n mémoire identifiée par une adresse.");
        yield return new WaitForSeconds(4f);

        mascotte.SetMood(MascotteMood.Inter);
        mascotte.Say("Un virus a causé une fuite mémoire.\nÀ toi de la réparer.");

        if (btnCommencerPatch) btnCommencerPatch.SetActive(true);
    }
}

