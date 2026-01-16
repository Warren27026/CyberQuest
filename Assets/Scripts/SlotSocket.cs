using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SlotSocket : MonoBehaviour
{
    public string expectedCubeID;
    public bool isCorrect = false;

    private XRSocketInteractor socket;

    void Awake()
    {
        socket = GetComponent<XRSocketInteractor>();
        socket.selectEntered.AddListener(OnCubeInserted);
        socket.selectExited.AddListener(OnCubeRemoved);
    }

    void OnCubeInserted(SelectEnterEventArgs args)
    {
        CubeID cube = args.interactableObject.transform.GetComponent<CubeID>();

        if (cube != null)
        {
            isCorrect = (cube.cubeID == expectedCubeID);
        }
    }

    void OnCubeRemoved(SelectExitEventArgs args)
    {
        isCorrect = false;
    }
}
