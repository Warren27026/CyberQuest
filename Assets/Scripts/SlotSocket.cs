//ce script gere la logique d'insertion des cubes dans les emplacements/cases
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SlotSocket : MonoBehaviour
{
    public string expectedCubeID;
    //indique si le cube insere est correct ou non:
    public bool isCorrect = false;

    public RamGameManager gameManager;

    private XRSocketInteractor socket;

    //reference a l'aura du cube actuellement insere
    private AuraUI currentCubeAura;

    void Awake()
    {
        socket = GetComponent<XRSocketInteractor>();
        socket.selectEntered.AddListener(OnCubeInserted);
        socket.selectExited.AddListener(OnCubeRemoved);
    }
    
    //quand un cube est insere dans la case:
    void OnCubeInserted(SelectEnterEventArgs args)
    {

        //bloquer toute interaction tant que le jeu n'est pas lance
        if (gameManager != null && !gameManager.IsGameRunning)
        {
             return;
        }

        CubeID cube = args.interactableObject.transform.GetComponent<CubeID>();
        currentCubeAura = args.interactableObject.transform.GetComponent<AuraUI>();

        if (cube != null)
        {
            isCorrect = (cube.cubeID == expectedCubeID);

            //mettre a jour l'aura
            if (currentCubeAura != null)
            {
                currentCubeAura.Show(isCorrect);
            }

            //son feedback immediat
            if (gameManager != null)
            {
                if (isCorrect)
                    gameManager.PlaySuccess();
                else
                    gameManager.PlayFail();
            }
        }
    }
 
    //quand le cube est enleve de la case:
    void OnCubeRemoved(SelectExitEventArgs args)
    {
        isCorrect = false;

        //cacher l'aura quand on enleve le cube
        if (currentCubeAura != null)
        {
            currentCubeAura.Hide();
            currentCubeAura = null;
        }
    }
}
