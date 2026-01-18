//ce script est pour gerer l'état de trois boutons de CPU qui s'allument et clignotent.

using UnityEngine;
using System.Collections;

public class ButtonController : MonoBehaviour
{
    public Renderer bouton1; //le bouton du haut
    public Renderer bouton2; //le bouton du milieu
    public Renderer bouton3; //le bouton du bas

    public Color greenColor = Color.green; //colorer en vert le bouton de l'instruction en cours d execution
    public Color orangeColor = new Color(1f, 0.5f, 0f); //colorer en orange le bouton de l'instruction en attente d'execution
    public float blinkSpeed = 0.5f; //vitesse de clignotement des boutons en attente

    void Start()
    {
        //le bouton en haut est toujours vert
        bouton1.material.color = greenColor;

        //pour demarrer le clignotement des boutons 2 et 3
        StartCoroutine(BlinkButtons());
    }
    
    //gestion cligotement des boutons 2 et 3:
    IEnumerator BlinkButtons()
    {
        while (true)
        {
            //si on alors orange
            bouton2.material.color = orangeColor;
            bouton3.material.color = orangeColor;
            yield return new WaitForSeconds(blinkSpeed);

            //si off alors noir/éteint
            bouton2.material.color = Color.black;
            bouton3.material.color = Color.black;
            yield return new WaitForSeconds(blinkSpeed);
        }
    }
}

