using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class GolfView : BaseView
{
    //bad prototype code
    [SerializeField]
    private TextMeshProUGUI courseNameTextMesh;

    [SerializeField]
    private TextMeshProUGUI redPlayer;

    [SerializeField]
    private TextMeshProUGUI bluePlayer;

    [SerializeField]
    private TextMeshProUGUI greenPlayer;

    [SerializeField]
    private TextMeshProUGUI yellowPlayer;

    private void Start()
    {
        victoryScreen.gameObject.SetActive(false);
    }

    public void ShowGolfOverScreen(List<int> hitsNeeded, List<int> golfnutHP)
    {
        IsBackgroundActive = true;
        victoryScreen.gameObject.SetActive(true);
        string resultText = "<color=black>You cleared all the courses!<br><br>";

        int totalHitsNeeded = 0;
        int totalGolfnutHP = 0;

        for (int i = 0; i < hitsNeeded.Count; i++)
        {
            if (i % 2 == 0)
            { resultText += "<color=black>"; }
            else
            { resultText += "<color=blue>"; }

            resultText += $"Course {i}: {hitsNeeded[i]} / {golfnutHP[i]} hits<br>";

            totalHitsNeeded += hitsNeeded[i];
            totalGolfnutHP += golfnutHP[i];
        }

        victoryScreen.text = resultText;
        victoryScreen.text += $"<br><br><color=red><b>TOTAL SCORE: {totalHitsNeeded} / {totalGolfnutHP} hits</b>";
        victoryScreen.text += "<br><br><color=black>PRESS ESC OR START TO GO BACK TO MAIN MENU";
    }

    public void ShowMultiGolfOverScreen(Dictionary<PlayerDTO, int> playerScores)
    {
        IsBackgroundActive = true;
        victoryScreen.gameObject.SetActive(true);

        victoryScreen.text = "";

        foreach (var player in playerScores.Keys)
        {
            string colorText = FindObjectOfType<PlayerColorManager>().GetColorTextByType(player.ColorType);
            victoryScreen.text += $"<color={colorText}>";
            victoryScreen.text += $"Player {player.PlayerIndex + 1}<color=black> has scored {playerScores[player]}! <br>";
        }

        victoryScreen.text += "<br><br><color=black>PRESS ESC OR START TO GO BACK TO MAIN MENU";
    }

    public void SetCourseName(string courseName)
    {
        courseNameTextMesh.text = courseName;
    }

    public void SetScoreInUI(ColorType colorType, int score)
    {
        string colorText = FindObjectOfType<PlayerColorManager>().GetColorTextByType(colorType);
        string scoreString = $"<color={colorText}>{score}";

        switch (colorType)
        {
            case ColorType.None:
                break;
            case ColorType.Red:
                redPlayer.text = "";
                redPlayer.text += scoreString;
                break;
            case ColorType.Blue:
                bluePlayer.text = "";
                bluePlayer.text += scoreString;
                break;
            case ColorType.Green:
                greenPlayer.text = "";
                greenPlayer.text += scoreString;
                break;
            case ColorType.Yellow:
                yellowPlayer.text = "";
                yellowPlayer.text += scoreString;
                break;
        }
    }
}
