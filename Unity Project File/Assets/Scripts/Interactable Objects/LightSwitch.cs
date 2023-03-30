using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LightSwitch : Interactable
{
    [Header ("Light CoolDown Settings")]
    [SerializeField] private int cooldownMin;

    [SerializeField] private int cooldownMax;

    [Space(10)]
    public GameObject m_Light;
    [SerializeField] private Animator switchFlipAnim;

    [SerializeField] private GameObject lightIndicatorGreen;
    [SerializeField] private GameObject lightIndicatorRed;
    [SerializeField] private GameObject realBedroomDoor;
    [SerializeField] private GameObject fakeBedroomDoor;

    [SerializeField] private GameObject eventTrigger;

    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioSource foundSound;

    [SerializeField] private AudioSource heartbeatSound;
    public bool isOn;

    private int cooldownTime;

    public bool isDay2 = false;

    private Objectives objectivesScript;

    private void Start() { 
        objectivesScript = GameObject.Find("Game Manager").GetComponent<Objectives>();

        if (isDay2){
            LightsOff();
        }
        else {
            UpdateLight();
        }
    }

    void UpdateLight() {
        isOn = true;
        heartbeatSound.Stop();
        m_Light.SetActive(true);
        switchFlipAnim.SetTrigger("Flip");
        lightIndicatorRed.SetActive(false);
        lightIndicatorGreen.SetActive(true);
        cooldownTime = Random.Range(cooldownMin, cooldownMax);
        Debug.Log("Light Cooldown Time :" + cooldownTime);
        StartCoroutine(LightsOffCoolDown(cooldownTime));
        objectivesScript.DespawnKnight();
        realBedroomDoor.SetActive(true);
        fakeBedroomDoor.SetActive(false);
        
        if (eventTrigger != null){
            eventTrigger.SetActive(true);
        }
        
    }

    public override float GetHoldDuration() {
        return  1f;
    }

    public override string GetDescription() {
        if (isOn) return "The switch is already on.";
        return "Press [E] to turn <color=green>on</color> the light.";
    }

    public override void Interact() {
        //isOn = !isOn;
        isOn = true;
        UpdateLight();
        
    }

    public void LightsOff(){
        isOn = false;
        m_Light.SetActive(false);
        lightIndicatorRed.SetActive(true);
        lightIndicatorGreen.SetActive(false);
        objectivesScript.SpawnKnight();

        realBedroomDoor.SetActive(false);
        fakeBedroomDoor.SetActive(true);

        audioSource.pitch = Random.Range(0.05f, 0.2f);
        audioSource.Play();
        foundSound.Play();
        heartbeatSound.Play();
    }

    IEnumerator LightsOffCoolDown(int cooldown){
        switchFlipAnim.SetTrigger("FlipBack");
        yield return new WaitForSeconds(cooldown);
        LightsOff();
    }
}
