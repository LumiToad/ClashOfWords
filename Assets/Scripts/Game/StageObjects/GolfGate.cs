using Clash_Of_Words.Destructable;
using UnityEngine;

public class GolfGate : MonoBehaviour
{
    private BoxCollider boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var kickable = other.GetComponent<Kickable>();

        if (kickable != null)
        {
            PlayerDTO playerDTO = other.GetComponent<Golfnut>().PlayerDTO;
            if ( playerDTO != null)
            {
                FootballScore(playerDTO);
                boxCollider.enabled = false;
                return;
            }
            FootballScore();
            boxCollider.enabled = false;
        }
    }

    private void FootballScore()
    {
        Game.Instance.GameMode.StageObjectInteraction(this, null);
    }

    private void FootballScore(PlayerDTO playerDTO)
    {
        Game.Instance.GameMode.StageObjectInteraction(this, playerDTO);
    }
}
