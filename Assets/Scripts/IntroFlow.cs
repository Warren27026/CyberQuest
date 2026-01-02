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



    [Header("Story Decision UI")]
    public GameObject storyDecisionCanvas; // Canvas_StoryDecision

    [Header("Refs")]
    public MascotteSimple mascotte;
    public TeleportSimple teleport;

    private int step = 0;

    void Start()
    {
        if (storyDecisionCanvas) storyDecisionCanvas.SetActive(false);
        if (btnCommencerPatch) btnCommencerPatch.SetActive(false);


        // Replacer le joueur à l’entrée au lancement
        if (teleport != null) teleport.TeleportToEntry();

        ShowStep(0);
    }

    void ShowStep(int s)
    {
        step = s;

        if (introText == null) return;

        // Boutons visibles selon l’étape
        if (btnNext) btnNext.SetActive(step == 0);
        if (btnCompris) btnCompris.SetActive(step == 1);
        if (btnOk) btnOk.SetActive(step == 2);

        if (step == 0)
            introText.text =
                "Bienvenue à CyberQuest!\n" +
                "Le jeu qui te met au sein d'une ville simulant les composants d'ordinateurs.";
        else if (step == 1)
            introText.text =
                "Pour montrer l’interface de jeu :\n" +
                "fais un geste abracadabra et laisse le bouton maintenu.";
        else if (step == 2)
            introText.text =
                "La mascotte du jeu va s'afficher pour t'accompagner.";
    }

    // =======================
    // INTRO UI BUTTONS
    // =======================
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

    // =======================
    // YES START → CPU (CHANGEMENT CLÉ)
    // =======================
    public void BtnYesStart()
    {
        if (storyDecisionCanvas) storyDecisionCanvas.SetActive(false);
        StartCPUStory(); // ✅ CPU en premier
    }

    // =======================
    // CPU STORY
    // =======================
    public void StartCPUStory()
    {
        if (teleport != null) teleport.TeleportToCPU();
        if (btnComprisCPU) btnComprisCPU.SetActive(true);

        if (mascotte != null)
        {
            mascotte.ShowAtStoryPoint();
            mascotte.SetMood(MascotteMood.Happy);
            mascotte.Say("Voici le CPU. Il est le cerveau de l’ordinateur.");
        }
    }

    public void BtnComprisCPU()
    {

        if (teleport != null) teleport.TeleportToDriver();
        StartDriverStory();
    }

    // =======================
    // DRIVER STORY
    // =======================
    public void StartDriverStory()
    {
        if (btnComprisCPU) btnComprisCPU.SetActive(false);

        if (teleport != null) teleport.TeleportToDriver();
        if (btnComprisDriver) btnComprisDriver.SetActive(true);

        if (mascotte != null)
        {
            mascotte.ShowAtStoryPoint();
            mascotte.SetMood(MascotteMood.Happy);
            mascotte.Say("Le driver permet aux composants de communiquer avec le système.");
        }
    }

    public void BtnComprisDriver()
    {
        if (teleport != null) teleport.TeleportToRAM();
        StartRamIntro();
    }

    
    public void StartRamIntro()
    {
        if (btnComprisCPU) btnComprisCPU.SetActive(false);
        if (btnComprisDriver) btnComprisDriver.SetActive(false);
        StartCoroutine(RamSequence());
    }

    IEnumerator RamSequence()
    {
        if (mascotte == null) yield break;

        mascotte.ShowAtStoryPoint();
        mascotte.SetMood(MascotteMood.Happy);
        mascotte.Say("Voici la mémoire RAM. Elle stocke les données temporaires qu'utilisent les programmes");
        yield return new WaitForSeconds(5f);
        mascotte.Say("Chaque donnée est stockée dans une cellule mémoire identifiée par une adresse");
        yield return new WaitForSeconds(5f);



        mascotte.SetMood(MascotteMood.Inter);
        mascotte.Say("Un virus a causé une fuite mémoire. \n les données ne sont plus à leur place. \n À toi de la réparer.");

        // Bouton "Je commence le patch"
        if (btnCommencerPatch) btnCommencerPatch.SetActive(true);

    }
}

