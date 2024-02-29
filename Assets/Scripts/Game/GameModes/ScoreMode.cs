using UnityEngine;

public class ScoreMode : GameModeBase
{
    private int wordsToVictory = 0;
    private int WordsToVictory
    { get { return wordsToVictory; } }

    private string currentWord;
    public string CurrentWord
    { get { return currentWord; } }
    public WordView WordView { get; private set; }

    public override void Setup(object data, GameModeType type, BaseView view)
    {
        gameModeType = type;
        WordView = (WordView)view;
        wordsToVictory = (int)data;
        currentWord = GetRandomWord();
        SetNewWordInPlayers();
    }

    public override bool IsVictoryCondition(object data)
    {
        PlayerDTO playerDTO = (PlayerDTO)data;
        if (playerDTO.CurrentScore >= wordsToVictory)
        {
            return true;
        }
        
        return false;
    }

    public override void GameEnd(object data)
    {
        ((WordView)Game.Instance.View).ShowVictoryScreen(data as PlayerDTO);
        Game.Instance.TestAudioPlayer.PlayVictoryBGM();
    }

    public void OnWordCompleted(PlayerDTO playerDTO)
    {
        if (IsVictoryCondition(playerDTO)) 
        {
            GameEnd(playerDTO);
        }
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
        OnWordCompleted(playerDTO);
    }

    private string GetRandomWord()
    {
        int randomIndex = Random.Range(0, 30);
        Words words = (Words)randomIndex;
        return words.ToString();
    }
}
