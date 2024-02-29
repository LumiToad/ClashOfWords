using UnityEngine;
using Clash_Of_Words.Destructable;

public class MultiGolfMode : GameModeBase
{
    public GolfView GolfView { get; private set; }

    private int scoreNeeded = 4;

    public override void Setup(object data, GameModeType type, BaseView view)
    {
        MenuToGameData menuToGameData = Game.Instance.MenuToGameData;

        gameModeType = type;
        GolfView = (GolfView)view;
        
        DisableWalnutSpawner();
        GolfView.SetCourseName($"Course: {menuToGameData.sceneIndexToLoad - 3}");
        AddPlayers();
        SetScoreInUI();
        SpawnGolfnuts();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F3))
        {
            GolfNextLevel();
        }
    }

    private void AddPlayers()
    {
        MenuToGameData menuToGameData = Game.Instance.MenuToGameData;

        foreach (Player player in Game.Instance.PlayerManager.Players)
        {
            if (menuToGameData.golfScore.ContainsKey(player.PlayerDTO)) 
            {
                return;
            }
            menuToGameData.golfScore.Add(player.PlayerDTO, 0);
        }
    }

    public override void GameEnd()
    {
        MenuToGameData menuToGameData = Game.Instance.MenuToGameData;

        Game.Instance.TestAudioPlayer.PlayVictoryBGM();
        GolfView.ShowMultiGolfOverScreen(menuToGameData.golfScore);
        Destroy(Game.Instance.MenuToGameData.gameObject);
    }

    public override void StageObjectInteraction(object item, object data)
    {
        switch (item)
        {
            case GolfGate:
                GolfNextLevel(data as PlayerDTO);
                break;
        }
        return;
    }

    private void GolfNextLevel(PlayerDTO playerDTO)
    {
        MenuToGameData menuToGameData = Game.Instance.MenuToGameData;

        menuToGameData.golfScore[playerDTO]++;
        SetScoreInUI();

        if (menuToGameData.golfScore[playerDTO] > scoreNeeded)
        {
            GameEnd();
            return;
        }

        menuToGameData.sceneIndexToLoad++;
        Debug.Log(menuToGameData.sceneIndexToLoad);
        if (!Game.Instance.LoadSceneBySceneIndex(menuToGameData.sceneIndexToLoad))
        {
            menuToGameData.sceneIndexToLoad = 3;
            Game.Instance.LoadSceneBySceneIndex(menuToGameData.sceneIndexToLoad);
        }
    }

    private void GolfNextLevel()
    {
        MenuToGameData menuToGameData = Game.Instance.MenuToGameData;

        menuToGameData.sceneIndexToLoad++;
        Debug.Log(menuToGameData.sceneIndexToLoad);
        if (!Game.Instance.LoadSceneBySceneIndex(menuToGameData.sceneIndexToLoad))
        {
            menuToGameData.sceneIndexToLoad = 3;
            Game.Instance.LoadSceneBySceneIndex(menuToGameData.sceneIndexToLoad);
        }
    }

    private void DisableWalnutSpawner()
    {
        foreach (var spawner in FindObjectsOfType<WalnutSpawner>())
        {
            spawner.gameObject.SetActive(false);
        }
    }

    private void SetScoreInUI()
    {
        MenuToGameData menuToGameData = Game.Instance.MenuToGameData;

        foreach (Player player in Game.Instance.PlayerManager.Players)
        {
            GolfView.SetScoreInUI(player.PlayerDTO.ColorType, menuToGameData.golfScore[player.PlayerDTO]);
        }
    }

    private void SpawnGolfnuts()
    {
        foreach (Player player in Game.Instance.PlayerManager.Players)
        {
            Game.Instance.SpawnerManager.RedSpawner.SpawnGolfNut(player);
        }
    }
}
