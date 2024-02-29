using UnityEngine;
using UnityEngine.Playables;

public class PlayerAttack : MonoBehaviour
{
    PlayerNutcrackerHitBox playerNutcrackerHitBox;
    PlayerTailHitBox playerTailHitBox;

    private void Awake()
    {
        playerNutcrackerHitBox = GetComponentInChildren<PlayerNutcrackerHitBox>();
        playerTailHitBox = GetComponentInChildren<PlayerTailHitBox>();

        if (playerNutcrackerHitBox == null)
        {
            Debug.LogWarning("LUMI: PlayerNutcrackerHitBox not found in PlayerAttack.cs!");
            return;
        }

        if (playerTailHitBox == null)
        {
            Debug.LogWarning("LUMI: PlayerTailHitBox not found in PlayerAttack.cs!");
            return;
        }
    }

    public void OnNutcrackerHitStart()
    {
        playerNutcrackerHitBox.EnableHitBox();
    }

    public void OnNutcrackerHitEnd()
    {
        playerNutcrackerHitBox.DisableHitBox();
    }

    public void OnTailHitStart()
    {
        playerTailHitBox.EnableHitBox();
    }

    public void OnTailHitEnd()
    {
        playerTailHitBox.DisableHitBox();
    }
}
