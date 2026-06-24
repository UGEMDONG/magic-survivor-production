using TMPro;
using UnityEngine;

public class TimerTextUpdate :
MonoBehaviour
{
    [SerializeField] private Timer timer;
    [SerializeField] private TMP_Text timerText;

    void OnEnable()
    {
        timer.TimeChanged += UpdateText;
    }
    void OnDisable()
    {
        timer.TimeChanged -= UpdateText;
    }



    private void UpdateText(int time)
    {

    }
}