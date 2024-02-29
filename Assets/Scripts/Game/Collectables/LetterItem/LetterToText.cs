using TMPro;
using UnityEngine;

public class LetterToText : MonoBehaviour
{
    private TextMeshPro textMeshPro;

    private void Awake()
    {
        textMeshPro = GetComponent<TextMeshPro>();
    }

    public void SetLetterToText(char letter)
    {
        textMeshPro.text = "";
        textMeshPro.text += letter;
    }
}
