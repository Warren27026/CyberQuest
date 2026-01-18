using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.XR.Interaction.Toolkit;

public class IntroFlow : MonoBehaviour
{
    [Header("UI")]
    public GameObject introCanvas;
    public TextMeshProUGUI introText;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip beepClip;

    [Header("Game")]
    public RamGameManager gameManager;

    [Header("XR Controls")]
    public XRRayInteractor leftRay;
    public XRRayInteractor rightRay;
    public XRDirectInteractor leftGrab;
    public XRDirectInteractor rightGrab;

    [Header("Timing")]
    public float explorationTime = 40f;

    private bool introFinished = false;

    [Header("RAM Cubes")]
    public XRGrabInteractable[] ramCubes;


    void Start()
    {
        // Phase 0 — Bloquer totalement le joueur
        DisableXR();

        // Sécurité UI
        if (introCanvas) introCanvas.SetActive(true);

        // Lancement automatique de la séquence
        StartCoroutine(IntroSequence());
        DisableXR();
        DisableCubes();

    }

    IEnumerator IntroSequence()
    {
        //PHASE 1 — INTRO UI
        SetText("Bienvenue dans CyberQuest");
        yield return new WaitForSeconds(2f);

        SetText("La ville qui simule les composants internes de l’ordinateur sous forme de ville");
        yield return new WaitForSeconds(3f);

        SetText("Notre RAM a été attaquée par un virus qui a causé une fuite mémoire");
        yield return new WaitForSeconds(5f);

        SetText("Veuillez remettre les instructions à leurs places, utilise le trigger pour te teleporter et attraper l'instruction, et le joystick pour le déplacer");
        yield return new WaitForSeconds(4f);

        SetText("Mais d'abord, vous avez 40 secondes pour découvrir les composants avant de commencer");
        yield return new WaitForSeconds(4f);

        // Masquer l’UI
        if (introCanvas) introCanvas.SetActive(false);

        //PHASE 2 — EXPLORATION LIBRE
        EnableXR();   // Le joueur peut regarder / se déplacer

        yield return new WaitForSeconds(explorationTime);

        // Bip
        if (audioSource && beepClip)
            audioSource.PlayOneShot(beepClip);
            EnableCubes();


        //PHASE 3 — JEU ACTIF 
        if (gameManager)
            gameManager.StartGame();

        introFinished = true;
    }

    void SetText(string message)
    {
        if (introText)
            introText.text = message;
    }

    //XR CONTROL

    void DisableXR()
    {
        if (leftRay) leftRay.enabled = false;
        if (rightRay) rightRay.enabled = false;

        if (leftGrab) leftGrab.enabled = false;
        if (rightGrab) rightGrab.enabled = false;
    }

    void EnableXR()
    {
        if (leftRay) leftRay.enabled = true;
        if (rightRay) rightRay.enabled = true;

        if (leftGrab) leftGrab.enabled = true;
        if (rightGrab) rightGrab.enabled = true;
    }

    void DisableCubes()
    {
        foreach (var cube in ramCubes)
        {
           if (cube)
                cube.enabled = false;
        }
    }

    void EnableCubes()
    {
        foreach (var cube in ramCubes)
        {
            if (cube)
                cube.enabled = true;
        }
    }


}


