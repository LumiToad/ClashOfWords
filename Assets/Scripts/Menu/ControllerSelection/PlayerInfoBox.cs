using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoBox : MonoBehaviour
{
    Image image;

    [SerializeField]
    TextMeshProUGUI controllerText;

    [SerializeField]
    TextMeshProUGUI playerText;

    [SerializeField]
    GameObject rotateModel;

    GameObject model3DRef;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void Setup(Color c, string controller, int playerID, GameObject model3D)
    {
        image.color = c;

        controllerText.text = controller;
        playerText.text = "PLAYER " + playerID;

        model3DRef = Instantiate(model3D, rotateModel.transform);
    }
}
