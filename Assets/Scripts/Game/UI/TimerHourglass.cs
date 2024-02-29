using UnityEngine;
using UnityEngine.UI;

public class TimerHourglass : MonoBehaviour
{
    private float hourglassValue = 1.0f;

    private float stepSize;

    [SerializeField]
    private Image upperHalf;

    [SerializeField] 
    private Image lowerHalf;

    public void SetupHourglassUI(int timeInSeconds)
    {
        SetHourglassCurrentFill(hourglassValue);
        stepSize = CalculateStepSize(timeInSeconds);
    }

    public void ReduceByStepSize()
    {
        hourglassValue -= stepSize;
        SetHourglassCurrentFill(hourglassValue);
    }

    private void SetHourglassCurrentFill(float value)
    {
        upperHalf.fillAmount = value;

        float reverseValue = 1.0f - value;
        lowerHalf.fillAmount = reverseValue;

        upperHalf.fillAmount = Mathf.Clamp(upperHalf.fillAmount, 0.0f, 1.0f);
        lowerHalf.fillAmount = Mathf.Clamp(lowerHalf.fillAmount, 0.0f, 1.0f);
    }

    private float CalculateStepSize(int timeInSeconds)
    {
        float result = 1.0f;
        result /= (float)timeInSeconds;
        result /= 2;

        return result;
    }
}
