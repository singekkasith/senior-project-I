using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MazeGateManager : MonoBehaviour
{
    public int switchLeft;
    [SerializeField] GameObject gateObject;

    public TextMeshProUGUI switchText;

    // Start is called before the first frame update
    void Start()
    {
        switchLeft = 0;
        switchText.SetText("Gate Switch Activated: " + switchLeft + " / 5");
    }


    public void MazeUpdate()
    {
        switchLeft += 1;
        switchText.SetText("Gate Switch Activated: " + switchLeft + " / 5");
        if (switchLeft >= 5) {
            switchText.SetText("The Gate is Open. Run to the Gate!");
            Destroy(gateObject);
            Debug.Log("The Gate is Open!");
        } 
    }
}
