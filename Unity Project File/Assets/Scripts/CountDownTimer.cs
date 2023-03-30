using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountDownTimer : MonoBehaviour
{
    [SerializeField] private float currentTime = 0f;
    [SerializeField] private float startingTime = 120f;
    [SerializeField] TextMeshProUGUI countdownText;

    private PauseMenu pauseMenuScript;

    private bool isTimeStop = false;

    private float min;
    private float sec;

    private float timeAlert;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = startingTime;
        timeAlert = 0.2f * startingTime;
        pauseMenuScript = GameObject.Find("Canvas").GetComponent<PauseMenu>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!isTimeStop){
            currentTime -= 1 * Time.deltaTime;
            min = Mathf.FloorToInt(currentTime / 60f);
            sec = Mathf.FloorToInt(currentTime % 60f);
            countdownText.text = string.Format("Time Left: "+"{0:00}:{1:00}",min, sec);
            
            if (currentTime <= timeAlert){
                countdownText.text = string.Format("Time Left: "+"<color=red>{0:00}:{1:00}</color>",min, sec);
            }

            if (currentTime <= 0){
                pauseMenuScript.GameOver();
            }
        }
    }

    public void CountDownStop(){
        isTimeStop = true;
    }
}
