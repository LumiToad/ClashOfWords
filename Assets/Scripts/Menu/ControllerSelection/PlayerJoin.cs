using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerJoin : MonoBehaviour
{
    [SerializeField]
    private ColorType colorType;
    public ColorType ColorType
    { get { return colorType; } }

    private InputDevice inputDevice;
    public InputDevice InputDevice
    {
        get { return inputDevice; }
        set 
        { 
            inputDevice = value;
            if (inputDevice == null)
            {
                PlayerLeaving();
            }
            else
            {
                PlayerJoining();
            }
        } 
    }

    private PlayerDTO playerDTO = new PlayerDTO();
    public PlayerDTO PlayerDTO
    { get { return playerDTO; } }

    private int playerIndex;
    public int PlayerIndex
    { get { return playerIndex; } set { playerIndex = value; } }

    PlayerColorManager playerColorManager;

    PlayerInfoBox playerInfoBox;

    private void Awake()
    {
        playerInfoBox = GetComponentInChildren<PlayerInfoBox>();
        playerColorManager = FindObjectOfType<PlayerColorManager>();
        
        Image image = GetComponent<Image>();
        image.color = playerColorManager.GetColorByType(colorType);

        DisableInfoBox();
    }

    private void Update()
    {
        if (inputDevice == null) return;
    }

    private void PlayerJoining()
    {
        ResetDataModels();

        playerDTO.InputDevice = InputDevice;
        PlayerDTO.ColorType = colorType;

        ExpandCanvas(true);
    }

    private void PlayerLeaving()
    {
        inputDevice = null;

        ExpandCanvas(false);
    }

    private void ResetDataModels()
    {
        playerDTO = new PlayerDTO();
    }

    public float[] GetColor()
    {
        return playerColorManager.GetColorFloatArrayByType(colorType);
    }

    private void ExpandCanvas(bool value)
    {
        playerInfoBox.gameObject.SetActive(value);

        if (value) 
        {
            string controllerName = "";

            if (inputDevice != null) 
            {
                controllerName = inputDevice.ToString();
            }

            Color color = playerColorManager.GetColorByType(colorType);
            GameObject model3D = playerColorManager.GetModelByType(colorType);

            playerInfoBox.Setup(color, controllerName, playerIndex, model3D);
        }
    }

    private void DisableInfoBox()
    {
        playerInfoBox.gameObject.SetActive(false);
    }
}
