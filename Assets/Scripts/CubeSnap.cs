using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CubeSnap : MonoBehaviour
{
    public string cubeID;              // ex: "14"
    public float snapDistance = 0.3f;
    public float snapSpeed = 8f;

    private Slot currentSlot;
    private Rigidbody rb;
    private XRGrabInteractable grab;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        grab = GetComponent<XRGrabInteractable>();

        // Quand on reprend le cube → on libère le slot
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
            // Effet aimant (approche progressive)
            transform.position = Vector3.Lerp(
                transform.position,
                currentSlot.transform.position,
                Time.deltaTime * snapSpeed
            );
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Slot slot = other.GetComponent<Slot>();

        if (slot != null && 
            slot.slotID == cubeID && 
            !slot.isOccupied)
        {
            currentSlot = slot;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Slot slot = other.GetComponent<Slot>();
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

        rb.isKinematic = true;   // Le bloque physiquement dans la case
        currentSlot.isOccupied = true;
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        // Quand on reprend le cube → on libère la case
        if (currentSlot != null)
        {
            currentSlot.isOccupied = false;
            currentSlot = null;
        }

        rb.isKinematic = false;
    }
}
