using Clash_Of_Words.Destructable;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject playerPrefab;

    [SerializeField]
    GameObject golfnutPrefab;

    private void Awake()
    {
        Vector3 failSave = transform.position;
        failSave.y = 0.5f;
        transform.position = failSave;
    }

    public Player SpawnPlayer(PlayerDTO playerDTO, GameObject model3D)
    {
        GameObject playerRef = Instantiate(playerPrefab, GetComponentInParent<Transform>());
        Player player = playerRef.GetComponent<Player>();

        player.Model3D = model3D;
        player.PlayerDTO = playerDTO;

        return player;
    }

    public void SpawnGolfNut(Player player)
    {
        Vector3 spawn = player.transform.position;
        spawn += player.transform.forward * 3;
        spawn.y = 7.5f;
        Golfnut nut = Instantiate(golfnutPrefab, spawn, Quaternion.identity, Game.Instance.transform).GetComponent<Golfnut>();
        nut.PlayerDTO = player.PlayerDTO;
    }
}
