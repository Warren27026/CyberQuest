using UnityEngine;

public class CrystalController : MonoBehaviour
{
    public float spinSpeed = 50f;
    public Material greenMat;
    public Material redMat;
    
    // This static variable allows all crystals to check one source of truth
    public static bool isHorrificEventActive = false;
    public static bool playerSolvedGame = false;

    private Renderer rend;

    void Start() {
        rend = GetComponent<Renderer>();
    }

    void Update() {
        if (!isHorrificEventActive || playerSolvedGame) {
            // Spinning and Green state
            transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
            rend.material = greenMat;
        } else {
            // Stopped and Red state
            rend.material = redMat;
        }
    }
}
