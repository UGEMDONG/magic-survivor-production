using UnityEngine;
using UnityEngine.UI;

public class SmoothSlider : MonoBehaviour
{
    [SerializeField] Slider mySlider;
    [SerializeField] Slider targetSlider;
    [SerializeField] float lerp = 6f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mySlider.value = targetSlider.value;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Mathf.Approximately(mySlider.value, targetSlider.value))
            mySlider.value = Mathf.Lerp(mySlider.value, targetSlider.value, lerp * Time.deltaTime);
    }
}
