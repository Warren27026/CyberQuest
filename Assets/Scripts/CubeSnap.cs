using UnityEngine;

public class CubeSnap : MonoBehaviour
{
    public string cubeID;        // ex: "100"
    public float snapDistance = 0.25f;

    private Slot currentSlot;

    void Update()
    {
        if (currentSlot == null) return;

        float d = Vector3.Distance(transform.position, currentSlot.transform.position);
        if (d < snapDistance)
        {
            SnapIntoSlot();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Slot slot = other.GetComponent<Slot>();

        if (slot != null && slot.slotID == cubeID && !slot.isOccupied)
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

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        currentSlot.isOccupied = true;
    }
}

