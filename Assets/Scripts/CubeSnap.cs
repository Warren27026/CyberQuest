//script pour gerer le snap/"l'aimentation" des cubes dans les cases correspondantes
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CubeSnap : MonoBehaviour
{
    public string cubeID;
    public float snapDistance = 0.5f;
    public float snapSpeed = 0f;

    private Slot currentSlot;
    private Rigidbody rb;
    private XRGrabInteractable grab;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        grab = GetComponent<XRGrabInteractable>();

        if (grab != null)
            grab.selectEntered.AddListener(OnGrab);
    }

    void Update()
    {
        if (currentSlot == null) return;

        float d = Vector3.Distance(transform.position, currentSlot.transform.position);

        if (d < snapDistance)
        {
            SnapIntoSlot();
        }
        else
        {
            transform.position = Vector3.Lerp(
                transform.position,
                currentSlot.transform.position,
                Time.deltaTime * snapSpeed
            );
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Slot slot = other.GetComponentInParent<Slot>();

        if (slot != null && !slot.isOccupied)
        {
            currentSlot = slot;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Slot slot = other.GetComponentInParent<Slot>();
        if (slot == currentSlot)
        {
            currentSlot = null;
        }
    }

    void SnapIntoSlot()
    {
        transform.position = currentSlot.transform.position;
        transform.rotation = currentSlot.transform.rotation;

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;

        currentSlot.isOccupied = true;
        currentSlot.isCorrect = (currentSlot.slotID == cubeID);
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        if (currentSlot != null)
        {
            currentSlot.isOccupied = false;
            currentSlot.isCorrect = false;
            currentSlot = null;
        }

        rb.isKinematic = false;
    }
}

