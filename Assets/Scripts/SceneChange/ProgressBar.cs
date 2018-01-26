using UnityEngine;


public class ProgressBar : MonoBehaviour
{
    UnityEngine.UI.Slider slider;
    void Start()
    {
        slider = GetComponent<UnityEngine.UI.Slider>();
    }

    void Update()
    {
        // Get the load percentage. Value is between 0 and 100.
        slider.value = LoadScreen.Instance.Progress;
    }
}