using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Slider slider;
    public bool isRunning = false;
    public float runTime = 0f;
    public float maxTime = 100f;

    void Start(){
        SetMaxTime(maxTime);
    }

    void Update(){
        if (!isRunning && runTime < maxTime){
            Recharging();
        }
        if (isRunning){
            StaminaSpending();
        }
    }

    public void SetMaxTime(float maxTime){
        slider.maxValue = maxTime;
        slider.value = maxTime;

        //added
        runTime = maxTime;
    }

    public void SetTime(float time){
        slider.value = time;
    }

    private void Recharging(){
        runTime += Time.deltaTime * 3;
        SetTime(runTime);

        if (runTime >= maxTime){
            isRunning = false;
        }
    }

    private void StaminaSpending(){
        runTime -= Time.deltaTime * 5;
        SetTime(runTime);

        if (runTime <= 0){
            isRunning = false;
            SetTime(-10);
        }
    }
}
