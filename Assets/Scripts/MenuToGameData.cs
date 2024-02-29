using System.Collections.Generic;
using UnityEngine;

public class MenuToGameData : MonoBehaviour
{
    [HideInInspector]
    List<PlayerDTO> playerDTOs = new List<PlayerDTO>();
    
    [HideInInspector]
    public List<PlayerDTO> PlayerDTOs
    { get { return playerDTOs; } }

    [HideInInspector]
    private GameModeType gameMode;
    
    [HideInInspector]
    public GameModeType GameMode
    { get { return gameMode; } set { gameMode = value; } }

    [HideInInspector]
    public int wordsToVictory = 0;

    [HideInInspector]
    public int roundTime = 0;


    public int sceneIndexToLoad;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        sceneIndexToLoad = 2;
    }

    ///////BAD CODE//////
    ///NEEDS OWN DTO

    public List<int> golfnutHitsNeeded = new();

    public List<int> golfnutHP = new();

    public Dictionary<PlayerDTO, int> golfScore = new();
}
