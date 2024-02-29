using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, IAnimationEventReceiver
{
    [SerializeField]
    private float kickForce = 1000.0f;
    public float KickForce
    { get { return kickForce; } }

    private ColorType colorType;
    public ColorType ColorType
    { get { return colorType; } set { colorType = value; } }

    private InputDevice inputDevice;
    public InputDevice InputDevice
    {
        get { return inputDevice; }
        set 
        {
            inputDevice = value;
            SetInputMethod(value);
        }
    }

    private PlayerDTO playerDTO;
    public PlayerDTO PlayerDTO
    {
        get 
        {
            WriteDataToPlayerDTO();
            return playerDTO; 
        }
        set 
        {
            playerDTO = value;
            SetupPlayerByDTO(playerDTO);
        }
    }

    private PlayerScore playerScore = new PlayerScore();
    public PlayerScore PlayerScore
    { get { return playerScore; } }

    [HideInInspector]
    private GameObject model3D;
    public GameObject Model3D
    { 
        get { return model3D; } 
        set 
        { 
            model3D = value;
            model3D = Instantiate(model3D, transform);
        }
    }

    private int playerIndex;
    public int PlayerIndex
    { get { return playerIndex; } set { playerIndex = value; } }

    private void OnDisable()
    {
        UnsubscribeAnimationTriggers();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F2)) 
        {
            var spawnPoints = FindObjectsOfType<PlayerSpawner>();

            transform.position = spawnPoints[0].transform.position;
        }   
    }

    private void SetupPlayerByDTO(PlayerDTO playerDTO)
    {
        this.InputDevice = playerDTO.InputDevice;
        this.ColorType = playerDTO.ColorType;
        this.PlayerIndex = playerDTO.PlayerIndex;
    }

    public bool TryAddLetter(char letter)
    {
        if (playerScore.TryAddCollectedLetter(letter))
        {
            OnLetterCollected();
            return true;
        }

        return false;
    }

    private void OnLetterCollected()
    {
        Game.Instance.SFXPlayer.PlaySFXByType(SFXClip.CollectLetter, gameObject);
        DebugShowCollectedLetters();


        if (playerScore.IsWordComplete)
        {
            playerScore.CurrentScore++;
            
            switch (Game.Instance.GameModeType)
            {
                default:
                    break;
                case GameModeType.Score:
                    (Game.Instance.GameMode as ScoreMode).PlayerHasCompleteWord(PlayerDTO);
                    break;
                case GameModeType.Time:
                    (Game.Instance.GameMode as TimeMode).PlayerHasCompleteWord(PlayerDTO);
                    break;
            }
        }
        else
        {
            ((WordView)Game.Instance.View).UpdatePlayerLetterUI(PlayerDTO);
        }
    }

    private void SetInputMethod(InputDevice device)
    {
        var gamePadInputMap = GetComponent<GamepadInputMap>();
        var keyboardInputMap = GetComponent<KeyboardInputMap>();

        if (device == null)
        {
            gamePadInputMap.Gamepad = null;
            keyboardInputMap.Keyboard = null;
        }

        if (device is Gamepad)
        {
            gamePadInputMap.Gamepad = device as Gamepad;
        }
        else
        {
            keyboardInputMap.Keyboard = device as Keyboard;
        }
    }

    private void DebugShowCollectedLetters()
    {
        foreach (char letter in playerScore.CollectedLetters.Keys)
        {
            Debug.Log($"Collected Letter: {letter} x {playerScore.CollectedLetters[letter]}");
        }
    }

    private void WriteDataToPlayerDTO()
    {
        playerDTO.InputDevice = InputDevice;
        playerDTO.ColorType = ColorType;
        playerDTO.PlayerIndex = PlayerIndex;
        playerDTO.CurrentScore = playerScore.CurrentScore;
        playerDTO.CurrentWord = playerScore.CurrentWord;
        playerDTO.LastCollected = playerScore.LastCollected;
    }

    #region IAnimationEventReceiver
    public void AnimationStartReceived(string animationName)
    {
        
    }

    public void AnimationEndReceived(string animationName)
    {
        
    }

    public void AnimationEventReceived(string eventName)
    {

    }

    public void SubscribeAnimationTriggers()
    {

    }

    public void UnsubscribeAnimationTriggers()
    {

    }

    #endregion IAnimationEventReceiver
}
