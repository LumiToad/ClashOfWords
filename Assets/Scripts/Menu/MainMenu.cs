using Unity.VisualScripting;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    SceneSwitcher sceneSwitcher;
    MenuToGameData MenuToGameData => FindObjectOfType<MenuToGameData>();

    private void Awake()
    {
        sceneSwitcher = GetComponent<SceneSwitcher>();
    }

    public void OnStartGameTimeClicked(int time)
    {
        StartTimeGame(time);
    }

    public void OnStartGameThreeClicked()
    {
        StartScoreGame(3);
    }

    public void OnStartGameFiveClicked()
    {
        StartScoreGame(5);
    }

    public void OnStartFreeClicked()
    {
        StartFreeGame();
    }

    public void OnStartGolfClicked()
    {
        StartGolfGame();
    }

    private void StartGolfGame()
    {
        MenuToGameData.GameMode = GameModeType.Golf;
        MenuToGameData.sceneIndexToLoad = 3;
        sceneSwitcher.LoadScene(1);
    }

    private void StartFreeGame()
    {
        MenuToGameData.GameMode = GameModeType.Free;
        sceneSwitcher.LoadScene(1);
    }

    private void StartScoreGame(int score)
    {
        MenuToGameData.GameMode = GameModeType.Score;
        MenuToGameData.wordsToVictory = score;
        sceneSwitcher.LoadScene(1);
    }

    private void StartTimeGame(int time)
    {
        MenuToGameData.GameMode = GameModeType.Time;
        MenuToGameData.roundTime = time;
        sceneSwitcher.LoadScene(1);
    }

    public void OnQuitGameClicked()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        
    }
}
