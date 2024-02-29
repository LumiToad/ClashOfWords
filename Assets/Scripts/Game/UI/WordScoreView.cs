using TMPro;
using UnityEngine;

public class WordScoreView : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI[] letterSlot;

    [SerializeField]
    TextMeshProUGUI scoreNumber;

    private string currentWord;
    public string CurrentWord 
    {  
        set 
        { 
            currentWord = value;
            ResetWord();
        }
    }

    public void SetTeamColor(Color color)
    {
        foreach (var slot in letterSlot) 
        {
            slot.color = color;
        }
        scoreNumber.color = color;
    }

    public void ResetWord()
    {
        for (int i = 0; i < letterSlot.Length; i++) 
        {
            WriteChar(i, ' ');
        }

        for (int i = 0; i < currentWord.Length; i++)
        {
            WriteChar(i, '_');
        }
    }

    public void LetterCollected(char letter)
    {
        int index = GetLetterPlacement(0, letter);

        if (index >= 0) 
        {
            WriteChar(index, letter);
        }
    }

    private int GetLetterPlacement(int startIndex, char letter)
    {
        Debug.Log($"letter: {letter} startIndex: {startIndex} wordLength: {currentWord.Length - 1} currentWord: {currentWord}");

        int index = currentWord.IndexOf(letter, startIndex, currentWord.Length - (startIndex));

        Debug.Log($"index: {index}");

        if (index < 0) return index;

        if (letterSlot[index].text == letter.ToString())
        {
            index = GetLetterPlacement(index + 1, letter);
        }

        return index;
    }

    private void WriteChar(int index, char letter)
    {
        letterSlot[index].text = "";
        letterSlot[index].text += letter;
    }

    public void ShowScore(int score)
    {
        scoreNumber.text = "";
        scoreNumber.text = score.ToString();
    }
}
