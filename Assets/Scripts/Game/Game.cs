using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public static Game Instance { get; private set; }

    public MenuToGameData MenuToGameData => FindObjectOfType<MenuToGameData>();

    [SerializeField]
    private DebugController debugController;

    [SerializeField]
    private SFXPlayer sfxPlayer;
    public SFXPlayer SFXPlayer
    { get {  return sfxPlayer; } }

    [SerializeField]
    private PlayerManager playerManager;
    public PlayerManager PlayerManager 
    { get { return playerManager; } }

    [SerializeField]
    private SpawnerManager spawnerManager;
    public SpawnerManager SpawnerManager
    { get { return spawnerManager; } }

    [SerializeField]
    private PlayerColorManager playerColorManager;
    public PlayerColorManager PlayerColorManager
    { get { return playerColorManager; } }

    [SerializeField]
    private GameObject gameTimerPrefab;

    private GameModeBase gameMode;
    public GameModeBase GameMode
    { get { return gameMode; } }

    public GameModeType GameModeType => gameMode.GameModeType;

    public TestAudioPlayer TestAudioPlayer => FindObjectOfType<TestAudioPlayer>();

    [SerializeField]
    private BaseView view;
    public BaseView View
    { get { return view; } }

    private void Start()
    {
        Instance = this;
        if (!MenuToGameDataExists()) return;

        SetAsSingleton();
        SetupRoundByGameMode(MenuToGameData.GameMode);
    }

    private bool MenuToGameDataExists()
    {
        if (MenuToGameData != null) return true;
        debugController.BackToMainMenu();
        return false;
    }

    private void SetAsSingleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void SetupRoundByGameMode(GameModeType gameMode)
    {
        foreach (var playerDTO in MenuToGameData.PlayerDTOs)
        {
            Player player = CreateAndSpawnPlayer(playerDTO);
            playerManager.Players.Add(player);
        }

        switch (gameMode)
        {
            case GameModeType.None:
                break;
            case GameModeType.Score:
                SetupScoreRound();
                Destroy(MenuToGameData.gameObject);
                break;
            case GameModeType.Time:
                SetupTimeRound();
                Destroy(MenuToGameData.gameObject);
                break;
            case GameModeType.Free:
                SetupFreePlay();
                Destroy(MenuToGameData.gameObject);
                break;
            case GameModeType.Golf:
                SetupGolf();
                break;
            case GameModeType.MultiGolf:
                SetupMultiGolf();
                break;
        }
    }

    private void SetupScoreRound()
    {
        gameMode = gameObject.AddComponent<ScoreMode>();
        gameMode.Setup(MenuToGameData.wordsToVictory, GameModeType.Score, view);
    }

    private void SetupTimeRound()
    {
        GameObject gameTimer = Instantiate(gameTimerPrefab, transform);

        gameMode = gameTimer.GetComponent<TimeMode>();
        gameMode.Setup(MenuToGameData.roundTime, GameModeType.Time, view);
    }

    private void SetupFreePlay()
    {
        gameMode = gameObject.AddComponent<FreeMode>();
        gameMode.Setup(null, GameModeType.Free, view);
        view.gameObject.SetActive(false);
    }

    private void SetupGolf()
    {
        gameMode = gameObject.AddComponent<GolfMode>();
        gameMode.Setup(null, GameModeType.Golf, view);
    }

    private void SetupMultiGolf()
    {
        gameMode = gameObject.AddComponent<MultiGolfMode>();
        gameMode.Setup(null, GameModeType.MultiGolf, view);
    }

    private Player CreateAndSpawnPlayer(PlayerDTO playerModel)
    {
        GameObject model3D = playerColorManager.GetModelByType(playerModel.ColorType);
        Player player = null;

        switch (playerModel.ColorType)
        {
            case ColorType.None:
                Debug.LogWarning("LUMI: NO COLOR IN PLAYER!");
                break;
            case ColorType.Red:
                player = SpawnerManager.RedSpawner.SpawnPlayer(playerModel, model3D);
                break;
            case ColorType.Blue:
                player = SpawnerManager.BlueSpawner.SpawnPlayer(playerModel, model3D);
                break;
            case ColorType.Green:
                player = SpawnerManager.GreenSpawner.SpawnPlayer(playerModel, model3D);
                break;
            case ColorType.Yellow:
                player = SpawnerManager.YellowSpawner.SpawnPlayer(playerModel, model3D);
                break;
        }

        return player;
    }

    public bool LoadSceneBySceneIndex(int index)
    {
        return debugController.LoadSceneBySceneIndex(index);
    }
}
