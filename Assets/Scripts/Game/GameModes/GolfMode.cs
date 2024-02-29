using UnityEngine;
using Clash_Of_Words.Destructable;

public class GolfMode : GameModeBase
{
    private Golfnut golfnut;

    public GolfView GolfView { get; private set; }

    public override void Setup(object data, GameModeType type, BaseView view)
    {
        MenuToGameData menuToGameData = Game.Instance.MenuToGameData;

        gameModeType = type;
        GolfView = (GolfView)view;
        DisableWalnutSpawner();
        GolfView.SetCourseName($"Course: {menuToGameData.sceneIndexToLoad - 3}");
        SpawnGolfnuts();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F3))
        {
            GetGolfnut();
            GolfNextLevel();
        }
    }

    public override void GameEnd()
    {
        MenuToGameData menuToGameData = Game.Instance.MenuToGameData;

        Game.Instance.TestAudioPlayer.PlayVictoryBGM();
        GolfView.ShowGolfOverScreen(menuToGameData.golfnutHitsNeeded, menuToGameData.golfnutHP);
        Destroy(Game.Instance.MenuToGameData.gameObject);
    }

    public override void StageObjectInteraction(object item, object data)
    {
        switch (item)
        {
            case GolfGate:
                GetGolfnut();
                GolfNextLevel();
                break;
        }
        return;
    }

    private void GolfNextLevel()
    {
        MenuToGameData menuToGameData = Game.Instance.MenuToGameData;

        menuToGameData.golfnutHitsNeeded.Add(golfnut.hitsTaken);
        menuToGameData.golfnutHP.Add(golfnut.StartHP);
        menuToGameData.sceneIndexToLoad++;
        Debug.Log(menuToGameData.sceneIndexToLoad);
        if (!Game.Instance.LoadSceneBySceneIndex(menuToGameData.sceneIndexToLoad))
        {
            GameEnd();
        }
    }

    private void GetGolfnut()
    {
        golfnut = FindObjectOfType<Golfnut>();
        if (golfnut == null)
        {
            Debug.LogWarning("LUMI: No Golfnut placed!");
        }
    }

    private void DisableWalnutSpawner()
    {
        foreach (var spawner in FindObjectsOfType<WalnutSpawner>())
        {
            spawner.gameObject.SetActive(false);
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
