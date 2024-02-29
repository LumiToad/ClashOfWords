using TMPro;
using UnityEngine;

public class TimerText : MonoBehaviour
{
    private TextMeshProUGUI textMeshProUGUI;

    private void Awake()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        textMeshProUGUI.text = "";
    }

    public void SetTimerUI(int time)
    {
        textMeshProUGUI.text = time.ToString();
    }
}
