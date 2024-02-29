using System.Collections.Generic;

public class TeamModel
{
    private ColorType colorType;
    public ColorType ColorType
    { get { return colorType; } set { colorType = value; } }

    private string currentWord;
    public string CurrentWord
    { get { return currentWord; } set { currentWord = value; } }

    private Dictionary<char, byte> requiredLetters;

    private Dictionary<char, byte> collectedLetters;
    public Dictionary<char, byte> CollectedLetters
    { get { return collectedLetters; } set { collectedLetters = value; } }

    private char lastCollected;
    public char LastCollected 
    { get { return lastCollected; } set { lastCollected = value; } }

    private bool isWordComplete;
    public bool IsWordComplete
    { get { return isWordComplete; } set { isWordComplete = value; } }

    private int teamIndex;
    public int TeamIndex
    { get { return teamIndex; } set { teamIndex = value; } }

    private int currentScore;
    public int CurrentScore
    { get { return currentScore; } set { currentScore = value; } }
}
