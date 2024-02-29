using Clash_Of_Words.Collectable;
using UnityEngine;

public class LetterItem : MonoBehaviour, ICollectable, IHitable
{
    private char letter = 'B';

    private LetterToText letterToTextFront;
    private LetterToText letterToTextBack;

    private void Awake()
    {
        //bad prototype code
        letterToTextFront = GetComponentsInChildren<LetterToText>()[0];
        letterToTextBack = GetComponentsInChildren<LetterToText>()[1];

        string currentWord = GetCurrentWord();
        letter = currentWord[Random.Range(0, currentWord.Length)];
    }

    private string GetCurrentWord()
    {
        string retVal = string.Empty;

        switch (Game.Instance.GameModeType)
        {
            default:
                break;
            case GameModeType.Score:
                retVal = (Game.Instance.GameMode as ScoreMode).CurrentWord;
                break;
            case GameModeType.Time:
                retVal = (Game.Instance.GameMode as TimeMode).CurrentWord;
                break;
        }

        return retVal;
    }

    void Start()
    {
        letterToTextFront.SetLetterToText(letter);
        letterToTextBack.SetLetterToText(letter);
    }

    public void Collect(Player player)
    {
        if (player.TryAddLetter(letter))
        {

        }
        DeleteCollectable();
    }

    public void DeleteCollectable()
    {
        Destroy(gameObject);
    }

    public void OnHitReceived(HitBoxBase hitBox)
    {
        var playerNutcrackerHitBox = hitBox.GetComponent<PlayerNutcrackerHitBox>();

        if (playerNutcrackerHitBox == null) return;

        Collect(playerNutcrackerHitBox.Player);
    }
}
