using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BaseView : MonoBehaviour
{
    // bad prototype code
    [SerializeField]
    protected TextMeshProUGUI victoryScreen;

    [SerializeField]
    protected Image image;

    public bool IsBackgroundActive
    { get => image.gameObject.activeSelf; set => image.gameObject.SetActive(value); }

    private void Start()
    {
        IsBackgroundActive = false;
    }


}
