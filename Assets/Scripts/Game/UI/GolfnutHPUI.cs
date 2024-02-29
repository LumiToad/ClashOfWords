using TMPro;
using UnityEngine;

public class GolfnutHPUI : MonoBehaviour
{
    private Canvas canvas;
    private TextMeshProUGUI textMeshPro;

    public string Text 
    {
        get { return textMeshPro.text; }
        set { textMeshPro.text = value;}
    }

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        canvas.worldCamera = Camera.main;
    }

    private void Update()
    {
        transform.LookAt(Camera.main.transform.position);
    }

    public void SetHP(int currentHP, int maxHP)
    {
        if (currentHP > 3)
        {
            Text = $"{currentHP} / {maxHP}";
        }
        else
        {
            Text = $"<color=red>{currentHP}<color=black> / {maxHP}";
        }
    }
}
