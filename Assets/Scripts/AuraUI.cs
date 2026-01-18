using UnityEngine;
using UnityEngine.UI;

public class AuraUI : MonoBehaviour
{
    [Header("References")]
    public GameObject auraCanvas;   // Le Canvas World Space
    public Image auraImage;         // L'image de l'aura

    [Header("Visual Settings")]
    [Range(0f, 1f)]
    public float alpha = 0.2f;      // Transparence

    public Color correctColor = new Color(0.2f, 1f, 0.2f, 1f);   // Vert doux
    public Color wrongColor   = new Color(1f, 0.2f, 0.2f, 1f);   // Rouge doux

    void Start()
    {
        // On cache l’aura au démarrage
        if (auraCanvas != null)
            auraCanvas.SetActive(false);
    }

    // Affiche l’aura (vert si correct, rouge sinon)
    public void Show(bool isCorrect)
    {
        if (auraCanvas == null || auraImage == null)
        {
            Debug.LogWarning("AuraUI: références manquantes !");
            return;
        }

        auraCanvas.SetActive(true);

        Color c = isCorrect ? correctColor : wrongColor;
        c.a = alpha;                 // Appliquer la transparence
        auraImage.color = c;
    }

    // Cache l’aura
    public void Hide()
    {
        if (auraCanvas != null)
            auraCanvas.SetActive(false);
    }
}

