using UnityEngine;
using UnityEngine.UI;

public class ExpBarUpdater : MonoBehaviour
{
    [SerializeField] private PlayerExp playerExp;
    [SerializeField] private Slider expSlider;

    private void Awake()
    {
        if (expSlider == null)
            expSlider = GetComponent<Slider>();

        if (expSlider != null)
        {
            expSlider.minValue = 0f;
            expSlider.maxValue = 1f;
        }
    }

    private void OnEnable()
    {
        if (playerExp == null)
            return;

        playerExp.ExpChanged += HandleExpChanged;
        UpdateGauge(playerExp.CurrentExp);
    }

    private void OnDisable()
    {
        if (playerExp != null)
            playerExp.ExpChanged -= HandleExpChanged;
    }

    private void HandleExpChanged(int currentExp)
    {
        UpdateGauge(currentExp);
    }

    private void UpdateGauge(int currentExp)
    {
        if (expSlider == null || playerExp.MaxExp <= 0)
            return;

        expSlider.value =
            Mathf.Clamp01((float)currentExp / playerExp.MaxExp);
    }
}
