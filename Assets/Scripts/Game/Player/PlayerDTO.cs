using UnityEngine.InputSystem;

public class PlayerDTO
{
    private InputDevice inputDevice;
    public InputDevice InputDevice
    { get { return inputDevice; } set { inputDevice = value; } }

    private ColorType colorType;
    public ColorType ColorType
    { get { return colorType; } set {  colorType = value; } }

    private int playerIndex;
    public int PlayerIndex
    { get { return playerIndex; } set {  playerIndex = value; } }

    private int currentScore = 0;
    public int CurrentScore
    { get { return currentScore; } set {  currentScore = value; } }

    private string currentWord;
    public string CurrentWord
    { get { return currentWord; } set { currentWord = value; } }

    private char lastCollected;
    public char LastCollected 
    { get { return lastCollected; } set { lastCollected = value; } }
}
