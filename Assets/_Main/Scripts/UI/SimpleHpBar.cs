using UnityEngine;
using UnityEngine.UI;

public class SimpleHpBar : MonoBehaviour
{
    [SerializeField] Slider slider;
    public void SetValue(float value) => slider.value = value;
}
