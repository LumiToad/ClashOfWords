using System.Collections.Generic;

public class Team
{
    private TeamModel model;
    public TeamModel Model
    { get { return model; } }

    private ColorType colorType;
    public ColorType ColorType
    { get { return colorType; } set { colorType = value; } }

    private string currentWord;
    public string CurrentWord
    { 
        get
        { return currentWord; }
        
        set 
        {
            currentWord = value;
            model.CurrentWord = value;
            ResetDictionarys();
        }
    }

    private Dictionary<char, byte> requiredLetters = new Dictionary<char, byte>();
    public Dictionary<char, byte> RequiredLetters
    { get { return requiredLetters; } }

    private Dictionary<char, byte> collectedLetters = new Dictionary<char, byte>();
    public Dictionary<char, byte> CollectedLetters
    { get { return collectedLetters; } }

    private char lastCollected;
    public char LastCollected { get { return lastCollected; } }

    public bool IsWordComplete
    {
        get
        {   
            bool isComplete = CheckWordComplete();
            model.IsWordComplete = isComplete;
            return isComplete;
        }
    }

    private int teamIndex;
    public int TeamIndex 
    {
        get { return teamIndex; } 
        set
        {
            teamIndex = value; 
            model.TeamIndex = teamIndex;
        } 
    }

    private int currentScore = 0;
    public int CurrentScore
    { 
        get { return currentScore; } 
        set
        {
            currentScore = value; 
            model.CurrentScore = currentScore;
        } 
    }

    public Team()
    {

    }

    public Team(TeamModel teamDataModel)
    {
        model = teamDataModel;
        teamIndex = teamDataModel.TeamIndex;
        colorType = teamDataModel.ColorType;
    }


    public bool TryAddCollectedLetter(char letter)
    {
        if (!requiredLetters.ContainsKey(letter)) return false;

        if (collectedLetters[letter] < requiredLetters[letter])
        {
            collectedLetters[letter]++;
            lastCollected = letter;
            model.LastCollected = letter;

            return true;
        }

        return false;
    }

    private void ResetDictionarys()
    {
        collectedLetters.Clear();
        requiredLetters.Clear();

        foreach (char letter in CurrentWord)
        {
            requiredLetters.Add(letter, 0);
            collectedLetters.Add(letter, 0);
        }

        foreach (char letter in CurrentWord)
        {
            requiredLetters[letter]++;
        }
    }

    private bool CheckWordComplete()
    {
        bool isComplete = true;

        foreach (char letter in CurrentWord) 
        {
            if (requiredLetters[letter] != collectedLetters[letter])
            {
                isComplete = false;
            }
        }

        return isComplete;
    }
}
