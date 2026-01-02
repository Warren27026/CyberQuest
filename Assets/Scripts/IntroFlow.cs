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

    [Header("Story Decision UI")]
    public GameObject storyDecisionCanvas; // Canvas_StoryDecision

    [Header("Refs")]
    public MascotteSimple mascotte;
    public TeleportSimple teleport;

    private int step = 0;

    void Start()
    {
        if (storyDecisionCanvas) storyDecisionCanvas.SetActive(false);

        // Optionnel: remettre le joueur à l'entrée au lancement
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
            introText.text = "Bienvenue à CyberQuest!\n le jeu qui te met au sein d'une ville simulant les composants d'ordinateurs";
        else if (step == 1)
            introText.text = "Pour montrer l’interface de jeu :\n fais un geste abracadabra et laisse le bouton maintenu";
        else if (step == 2)
            introText.text = "La mascotte du jeu va s'afficher pour t'accompagner";
    }

    // Liés aux boutons
    public void BtnNext() => ShowStep(1);
    public void BtnCompris() => ShowStep(2);

    public void BtnOk()
    {
        // On cache l’intro
        if (introPanel) introPanel.SetActive(false);

        // On lance la story mascotte
        StartCoroutine(StartMascotteStory());
    }

    IEnumerator StartMascotteStory()
    {
        if (mascotte != null)
        {
            mascotte.ShowAtStoryPoint();
            yield return StartCoroutine(mascotte.StorySequence(() =>
            {
                // afficher bouton Oui c’est parti
                if (storyDecisionCanvas) storyDecisionCanvas.SetActive(true);
            }));
        }
    }

    // Bouton "Oui c'est parti"
    public void BtnYesStart()
    {
        if (storyDecisionCanvas) storyDecisionCanvas.SetActive(false);

        // Téléportation automatique vers RAM
        if (teleport != null) teleport.TeleportToRAM();
    }
}
