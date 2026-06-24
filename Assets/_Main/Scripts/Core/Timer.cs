using System;
using UnityEngine;

public class Timer : 
MonoBehaviour
{
    private float currentTime;
    private int currentTimeInt;

    public event Action<int> TimeChanged;

    public int Minutes => currentTimeInt / 60;
    public int Seconds => currentTimeInt % 60;


    void Awake()
    {
        currentTime = 0f;
        currentTimeInt = 0;
    }

    void Update()
    {
        Tick(Time.deltaTime);
    }

    private void Tick(float deltaTime)
    {
        currentTime += deltaTime;

        if (currentTimeInt != (int)currentTime)
        {
            currentTimeInt = (int)currentTime;
            TimeChanged?.Invoke(currentTimeInt);
        }
    }
}
