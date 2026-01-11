using UnityEngine;
using System.Collections;

public class ButtonController : MonoBehaviour
{
    public Renderer bouton1;
    public Renderer bouton2;
    public Renderer bouton3;

    public Color greenColor = Color.green;
    public Color orangeColor = new Color(1f, 0.5f, 0f);
    public float blinkSpeed = 0.5f;

    void Start()
    {
        // Bouton 1 toujours vert
        bouton1.material.color = greenColor;

        // Démarrer le clignotement des boutons 2 et 3
        StartCoroutine(BlinkButtons());
    }

    IEnumerator BlinkButtons()
    {
        while (true)
        {
            // ON (orange)
            bouton2.material.color = orangeColor;
            bouton3.material.color = orangeColor;
            yield return new WaitForSeconds(blinkSpeed);

            // OFF (noir / éteint)
            bouton2.material.color = Color.black;
            bouton3.material.color = Color.black;
            yield return new WaitForSeconds(blinkSpeed);
        }
    }
}

