using UnityEngine;

public class WindmillRotation : MonoBehaviour
{
    public float rotationSpeed = 100f; // Geschwindigkeit der Rotation

    // Update wird einmal pro Frame aufgerufen
    void Update()
    {
        // Drehung um die lokale Y-Achse des Objekts
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}