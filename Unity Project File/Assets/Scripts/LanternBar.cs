using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanternBar : MonoBehaviour
{
    public Slider slider;
    public bool isLitting = false;
    public float litTime = 0f;
    //public float currentLitTime;
    public float maxTime = 100f;

    [SerializeField]
    private GameObject lanternLight;

    void Start(){
        SetMaxTime(maxTime);
    }

    void Update(){
        if (isLitting){
            LanternBurning();
            
        }
    }

    public void SetMaxTime(float maxTime){
        slider.maxValue = maxTime;
        slider.value = maxTime;

        //added
        litTime = maxTime;

    }

    public void SetTime(float time){
        slider.value = time;
    }

    public void AddFuel(float litAmount){
        litTime += litAmount;
        if (litTime >= maxTime){
            litTime = maxTime;
        }

        if (litTime <= 0 && !isLitting){
            isLitting = true;
        }
        SetTime(litTime);
    }

    private void LanternBurning(){
        litTime -= Time.deltaTime;
        SetTime(litTime);
        lanternLight.SetActive(true); 

        if (litTime <= 0){
            lanternLight.SetActive(false); 
            litTime = 0;
            SetTime(litTime);
            isLitting = false;
        }
    }
}
