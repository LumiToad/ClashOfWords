using UnityEngine;
using UnityEngine.Playables;

public class TimeMode : GameModeBase
{
    private PlayableDirector playableDirector;

    private int roundTime = 10;
    public int RoundTime
    { get { return roundTime; } }

    private string currentWord;
    public string CurrentWord
    { get { return currentWord; } }
    
    public WordView WordView { get; private set; }

    public override void Setup(object data, GameModeType type, BaseView view)
    {
        gameModeType = type;
        WordView = (WordView)view;
        roundTime = (int)data;
        playableDirector = GetComponent<PlayableDirector>();
        playableDirector.Play();
        WordView.SetupTimerUI(roundTime);
        currentWord = GetRandomWord();
        SetNewWordInPlayers();
    }

    public override bool IsVictoryCondition(object data)
    {
        if (roundTime <= (int)data) return true;

        return false;
    }

    public void TimerMinusOneSecond()
    {
        roundTime -= 1;
        ((WordView)Game.Instance.View).ReduceTimerUI(roundTime);

        if (IsVictoryCondition(0))
        {
            GameEnd();
            playableDirector.Stop();
        }
    }

    public override void GameEnd()
    {
        PlayerDTO[] playerDTOs = new PlayerDTO[Game.Instance.PlayerManager.PlayerCount];
        int iterator = 0;

        foreach (Player player in Game.Instance.PlayerManager.Players)
        {
            playerDTOs[iterator] = player.PlayerDTO;
            iterator++;
        }

        ((WordView)Game.Instance.View).ShowTimeOverScreen(playerDTOs);
        Game.Instance.TestAudioPlayer.PlayVictoryBGM();
    }

    private void SetNewWordInPlayers()
    {
        foreach (var player in Game.Instance.PlayerManager.Players)
        {
            player.PlayerScore.CurrentWord = CurrentWord;
            ((WordView)Game.Instance.View).SetupPlayerUI(player.PlayerDTO);
        }
    }

    public void PlayerHasCompleteWord(PlayerDTO playerDTO)
    {
        foreach (var letterItem in FindObjectsOfType<LetterItem>())
        {
            Destroy(letterItem.gameObject);
        }

        currentWord = GetRandomWord();
        SetNewWordInPlayers();

        ((WordView)Game.Instance.View).UpdatePlayerScoreUI(playerDTO);
    }

    private string GetRandomWord()
    {
        int randomIndex = Random.Range(0, 30);
        Words words = (Words)randomIndex;
        return words.ToString();
    }
}
