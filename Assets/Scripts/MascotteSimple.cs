using UnityEngine;
using TMPro;
using System.Collections;

public enum MascotteMood { Happy, Inter, Anx }

public class MascotteSimple : MonoBehaviour
{
    [Header("Faces")]
    public GameObject faceHappy;
    public GameObject faceInter;

    [Header("Bubble")]
    public TextMeshProUGUI bubbleText;

    [Header("Position")]
    public Transform storyPoint;

    void Start()
    {
        // On cache au démarrage pour être sûr
        gameObject.SetActive(false);
    }

    public void ShowAtStoryPoint()
    {
        if (storyPoint != null)
        {
            transform.position = storyPoint.position;
            transform.rotation = storyPoint.rotation;
        }
        gameObject.SetActive(true);
    }

    public void SetMood(MascotteMood mood)
    {
        if (faceHappy) faceHappy.SetActive(mood == MascotteMood.Happy);
        if (faceInter) faceInter.SetActive(mood == MascotteMood.Inter);
    }

    public void Say(string msg)
    {
        if (bubbleText) bubbleText.text = msg;
    }

    public IEnumerator StorySequence(System.Action onShowYesButton)
    {
        // Phrase 1 (Happy 4s)
        SetMood(MascotteMood.Happy);
        Say("Notre ville était merveilleuse...");
        yield return new WaitForSeconds(4f);

        // Phrase 2 (switch Inter au moment du texte)
        SetMood(MascotteMood.Inter);
        Say("Mais un virus est arrivé et a attaqué ma chère mémoire");
        yield return new WaitForSeconds(4f);

        // Phrase 3 (Inter)
        Say("Ta tâche sera simple mais nous fera grand plaisir.\n T’es prêt ?");
        yield return new WaitForSeconds(3f);

        onShowYesButton?.Invoke();
    }

    public void HideMascotte()
    {
       gameObject.SetActive(false);
    }

}

