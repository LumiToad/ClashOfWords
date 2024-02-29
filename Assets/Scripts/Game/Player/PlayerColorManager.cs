using UnityEngine;

public class PlayerColorManager : MonoBehaviour
{
    [SerializeField]
    private GameObject redModel;

    [SerializeField] 
    private GameObject blueModel;

    [SerializeField]
    private GameObject greenModel;

    [SerializeField] 
    private GameObject yellowModel;

    [SerializeField]
    private Color redColor;

    [SerializeField]
    private Color blueColor;

    [SerializeField]
    private Color greenColor;

    [SerializeField]
    private Color yellowColor;

    private float[] ColorToFloatArray(Color c)
    {
        float[] colorRGB = new float[4];

        colorRGB[0] = c.r;
        colorRGB[1] = c.g;
        colorRGB[2] = c.b;
        colorRGB[3] = c.a;

        return colorRGB;
    }

    public GameObject GetModelByType(ColorType type)
    {
        GameObject model = redModel;

        switch (type)
        {
            case ColorType.None:
                Debug.LogWarning("LUMI: NO COLOR SELECTED! (Red Model returned)");
                break;
            case ColorType.Red:
                model = redModel;
                break;
            case ColorType.Blue:
                model = blueModel;
                break;
            case ColorType.Green:
                model = greenModel;
                break;
            case ColorType.Yellow:
                model = yellowModel;
                break;
        }

        return model;
    }

    public Color GetColorByType(ColorType type)
    {
        Color c = redColor;

        switch (type)
        {
            case ColorType.None:
                Debug.LogWarning("LUMI: NO COLOR SELECTED! (Red Color returned)");
                break;
            case ColorType.Red:
                c = redColor;
                break;
            case ColorType.Blue:
                c = blueColor;
                break;
            case ColorType.Green:
                c = greenColor;
                break;
            case ColorType.Yellow:
                c = yellowColor;
                break;
        }

        return c;
    }

    public float[] GetColorFloatArrayByType(ColorType type)
    {
        float[] colorRGB = ColorToFloatArray(redColor);

        switch (type)
        {
            case ColorType.None:
                Debug.LogWarning("LUMI: NO COLOR SELECTED! (Red Color returned)");
                break;
            case ColorType.Red:
                colorRGB = ColorToFloatArray(redColor);
                break;
            case ColorType.Blue:
                colorRGB = ColorToFloatArray(blueColor);
                break;
            case ColorType.Green:
                colorRGB = ColorToFloatArray(greenColor);
                break;
            case ColorType.Yellow:
                colorRGB = ColorToFloatArray(yellowColor);
                break;
        }

        return colorRGB;
    }

    public string GetColorTextByType(ColorType type)
    {
        string result = string.Empty;

        switch (type)
        {
            case ColorType.None:
                Debug.LogWarning("LUMI: NO COLOR SELECTED! (Red Color Text returned)");
                result = "red";
                break;
            case ColorType.Red:
                result = "red";
                break;
            case ColorType.Blue:
                result = "blue";
                break;
            case ColorType.Green:
                result = "green";
                break;
            case ColorType.Yellow:
                result = "yellow";
                break;
        }

        return result;
    }
}
