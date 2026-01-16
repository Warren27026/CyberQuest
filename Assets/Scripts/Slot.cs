using UnityEngine;

public class Slot : MonoBehaviour
{
    [Header("Slot Identity")]
    public string slotID;     // ex: "14"

    [HideInInspector]
    public bool isOccupied = false;
}
