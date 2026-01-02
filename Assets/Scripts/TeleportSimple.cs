using UnityEngine;

public class TeleportSimple : MonoBehaviour
{
    public Transform xrOrigin;

    public Transform tpEntry;
    public Transform tpCPU;
    public Transform tpDriver;
    public Transform tpRam;

    public void TeleportToEntry()
    {
        if (xrOrigin == null || tpEntry == null) return;
        xrOrigin.SetPositionAndRotation(tpEntry.position, tpEntry.rotation);
    }

    public void TeleportToCPU()
    {
        if (xrOrigin == null || tpCPU == null) return;
        xrOrigin.SetPositionAndRotation(tpCPU.position, tpCPU.rotation);
    }

    public void TeleportToDriver()
    {
        if (xrOrigin == null || tpDriver == null) return;
        xrOrigin.SetPositionAndRotation(tpDriver.position, tpDriver.rotation);
    }

    public void TeleportToRAM()
    {
        if (xrOrigin == null || tpRam == null) return;
        xrOrigin.SetPositionAndRotation(tpRam.position, tpRam.rotation);
    }
}

