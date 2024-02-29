using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WordView: BaseView
{
    [SerializeField]
    private WordScoreView[] playerWordViews;

    [SerializeField]
    private TimerHourglass timerHourglass;

    [SerializeField]
    private TimerText timerText;

    private void Awake()
    {
        // bad prototype code
        victoryScreen.text = " ";

        DisableWordScoreViewsOnStartup();
        DisableTimerOnStartup();
    }

    private void DisableTimerOnStartup()
    {
        ShowTimerView(false);
    }

    private void DisableWordScoreViewsOnStartup()
    {
        for (int i = 0; i < playerWordViews.Length; i++)
        {
            ShowWordScoreView(false, i);
        }
    }

    private void ShowWordScoreView(bool value, int index)
    {
        //Debug.Log("Given Index: " + index);
        //Debug.Log("Array size: " + playerWordViews.Length);
        playerWordViews[index].gameObject.SetActive(value);
    }

    private void ShowTimerView(bool value)
    {
        timerHourglass.gameObject.SetActive(value);
        timerText.gameObject.SetActive(value);
    }

    //bad prototype code
    public void SetupPlayerUI(PlayerDTO playerDTO)
    {
        //Debug.Log("PlayerIndex: " + playerDTO.PlayerIndex);

        ShowWordScoreView(true, playerDTO.PlayerIndex);

        playerWordViews[playerDTO.PlayerIndex]
            .CurrentWord = playerDTO.CurrentWord;

        Color color = Game.Instance.PlayerColorManager
            .GetColorByType(playerDTO.ColorType);
        
        playerWordViews[playerDTO.PlayerIndex]
            .SetTeamColor(color);
    }

    public void SetupTimerUI(int roundTime)
    {
        ShowTimerView(true);
        timerHourglass.SetupHourglassUI(roundTime);
        timerText.SetTimerUI(roundTime);
    }

    public void UpdatePlayerWordUI(PlayerDTO playerDTO)
    {
        playerWordViews[playerDTO.PlayerIndex]
            .CurrentWord = playerDTO.CurrentWord;
    }

    public void UpdatePlayerLetterUI(PlayerDTO playerDTO)
    {
        playerWordViews[playerDTO.PlayerIndex]
            .LetterCollected(playerDTO.LastCollected);
    }

    public void UpdatePlayerScoreUI(PlayerDTO playerDTO)
    {
        playerWordViews[playerDTO.PlayerIndex]
            .ShowScore(playerDTO.CurrentScore);
    }

    public void ReduceTimerUI(int time)
    {
        timerHourglass.ReduceByStepSize();
        timerText.SetTimerUI(time);
    }

    public void ShowVictoryScreen(PlayerDTO playerDTO)
    {
        // bad prototype code
        Color color = Game.Instance.PlayerColorManager.GetColorByType(playerDTO.ColorType);

        victoryScreen.color = color;
        victoryScreen.text = "Player " + (playerDTO.PlayerIndex + 1) + " WINS!" + '\n';
        victoryScreen.text += "PRESS ESC OR START TO GO BACK TO MAIN MENU";
    }

    public void ShowTimeOverScreen(PlayerDTO[] playerDTOs)
    {
        string resultText = string.Empty;

        foreach (PlayerDTO playerDTO in playerDTOs) 
        {
            string colorAsText = Game.Instance.PlayerColorManager.GetColorTextByType(playerDTO.ColorType);

            resultText += $"<color={colorAsText}>Player {playerDTO.PlayerIndex + 1} <color=black>has scored {playerDTO.CurrentScore}!<br>";

            victoryScreen.text = resultText;
            victoryScreen.text += "PRESS ESC OR START TO GO BACK TO MAIN MENU";
        }
    }
}
