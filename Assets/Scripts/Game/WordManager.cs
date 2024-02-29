using UnityEngine;

public class WordManager : MonoBehaviour
{
    private string currentWord;
    public string CurrentWord
    { get { return currentWord; } }

    private void Awake()
    {
        ChooseNewCurrentWord();
    }

    public void ChooseNewCurrentWord()
    {
        currentWord = GetRandomWord();
    }

    private string GetRandomWord()
    {
        int randomIndex = Random.Range(0, 10);

        Words words = words = (Words)randomIndex;

        return words.ToString();
    }
}
