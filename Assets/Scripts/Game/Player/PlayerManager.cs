using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private List<Player> players = new List<Player>();
    public List<Player> Players
    { get { return players; } }

    public int PlayerCount => players.Count;
}
