using UnityEngine;
using UnityEngine.UI;

public class CopySlider : MonoBehaviour
{
    public Slider slider;
    public Slider sliderToMimic;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }
    // Update is called once per frame
    void Update()
    {
        if (slider != null && sliderToMimic != null)
            slider.value = sliderToMimic.value;
    }
}
