using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SlotSocket : MonoBehaviour
{
    public string expectedCubeID;
    public bool isCorrect = false;

    public RamGameManager gameManager;

    private XRSocketInteractor socket;

    // Référence à l'aura du cube actuellement inséré
    private AuraUI currentCubeAura;

    void Awake()
    {
        socket = GetComponent<XRSocketInteractor>();
        socket.selectEntered.AddListener(OnCubeInserted);
        socket.selectExited.AddListener(OnCubeRemoved);
    }

    void OnCubeInserted(SelectEnterEventArgs args)
    {
        CubeID cube = args.interactableObject.transform.GetComponent<CubeID>();
        currentCubeAura = args.interactableObject.transform.GetComponent<AuraUI>();

        if (cube != null)
        {
            isCorrect = (cube.cubeID == expectedCubeID);

            // Mettre à jour l'aura
            if (currentCubeAura != null)
            {
                currentCubeAura.Show(isCorrect);
            }

            // Son feedback immédiat (optionnel)
            if (gameManager != null)
            {
                if (isCorrect)
                    gameManager.PlaySuccess();
                else
                    gameManager.PlayFail();
            }
        }
    }

    void OnCubeRemoved(SelectExitEventArgs args)
    {
        isCorrect = false;

        // Cacher l'aura quand on enlève le cube
        if (currentCubeAura != null)
        {
            currentCubeAura.Hide();
            currentCubeAura = null;
        }
    }
}
