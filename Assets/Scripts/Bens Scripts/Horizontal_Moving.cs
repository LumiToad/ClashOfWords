using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horizontal_Moving : MonoBehaviour
{
    public Transform startPoint; // Startposition
    public Transform endPoint; // Endposition
    public float speed = 5f; // Geschwindigkeit, mit der das Objekt bewegt wird

    private float journeyLength; // Länge der Bewegung zwischen den beiden Positionen
    private float startTime; // Zeitpunkt, an dem die Bewegung gestartet wird

    void Start()
    {
        // Berechnung der Länge der Bewegung zwischen den beiden Positionen
        journeyLength = Vector3.Distance(startPoint.position, endPoint.position);
        startTime = Time.time; // Speichern des Startzeitpunkts
    }

    void Update()
    {
        // Berechnung des Fortschritts der Bewegung basierend auf der Zeit
        float distCovered = (Time.time - startTime) * speed;
        float fractionOfJourney = distCovered / journeyLength;

        // Bewegung des Objekts zwischen den beiden Positionen mit Lerp
        transform.position = Vector3.Lerp(startPoint.position, endPoint.position, fractionOfJourney);

        // Wenn das Objekt das Endziel erreicht hat, setze die Startzeit neu, um die Bewegung umzukehren
        if (fractionOfJourney >= 1)
        {
            startTime = Time.time;
            // Vertausche Start- und Endpositionen, um die Bewegung umzukehren
            Transform temp = startPoint;
            startPoint = endPoint;
            endPoint = temp;
        }
    }
}
