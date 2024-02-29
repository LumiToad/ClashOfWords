using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField]
    private PlayerSpawner redSpawner;
    public PlayerSpawner RedSpawner
    { get { return redSpawner; } }

    [SerializeField]
    private PlayerSpawner blueSpawner;
    public PlayerSpawner BlueSpawner
    { get { return blueSpawner; } }

    [SerializeField]
    private PlayerSpawner greenSpawner;
    public PlayerSpawner GreenSpawner
    { get { return greenSpawner; } }

    [SerializeField]
    private PlayerSpawner yellowSpawner;
    public PlayerSpawner YellowSpawner
    { get { return yellowSpawner; } }
}
