using UnityEngine;
using UnityEngine.InputSystem;

public class DebugController : MonoBehaviour
{
    private SceneSwitcher sceneSwitcher;

    private void Awake()
    {
        sceneSwitcher = GetComponent<SceneSwitcher>();   
    }

    // Update is called once per frame
    void Update()
    {
        BackToMainMenuOnESC();
    }

    public void BackToMainMenu()
    {
        if (Game.Instance.TestAudioPlayer != null)
        {
            Destroy(Game.Instance.TestAudioPlayer.gameObject);
        }
        
        if (Game.Instance.MenuToGameData != null)
        {
            Destroy(Game.Instance.MenuToGameData.gameObject);
        }

        sceneSwitcher.LoadScene(0);
    }

    private void BackToMainMenuOnESC()
    {
        if (Input.GetKey(KeyCode.Escape)) 
        {
            BackToMainMenu();
        }

        foreach (Gamepad gamepad in Gamepad.all)
        {
            if (gamepad == null) return;

            if (gamepad.startButton.wasPressedThisFrame) 
            {
                BackToMainMenu();
            }
        }
    }

    public bool LoadSceneBySceneIndex(int index)
    {
        return sceneSwitcher.LoadScene(index);
    }
}
