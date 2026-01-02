using UnityEngine;

public class TeleportSimple : MonoBehaviour
{
    public Transform xrOrigin;
    public Transform tpEntry;
    public Transform tpRam;

    public void TeleportToEntry()
    {
        if (xrOrigin == null || tpEntry == null) return;
        xrOrigin.SetPositionAndRotation(tpEntry.position, tpEntry.rotation);
    }

    public void TeleportToRAM()
    {
        if (xrOrigin == null || tpRam == null) return;
        xrOrigin.SetPositionAndRotation(tpRam.position, tpRam.rotation);
    }
}
