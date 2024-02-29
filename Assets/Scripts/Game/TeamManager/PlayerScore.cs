using System.Collections.Generic;

public class PlayerScore
{
    private string currentWord;
    public string CurrentWord
    { 
        get
        { return currentWord; }
        
        set 
        {
            currentWord = value;
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

    public bool IsWordComplete => CheckWordComplete();

    private int currentScore = 0;
    public int CurrentScore
    { get { return currentScore; } set { currentScore = value; } }

    public bool TryAddCollectedLetter(char letter)
    {
        if (!requiredLetters.ContainsKey(letter)) return false;

        if (collectedLetters[letter] < requiredLetters[letter])
        {
            collectedLetters[letter]++;
            lastCollected = letter;

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
            if (collectedLetters.ContainsKey(letter)) continue;

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
