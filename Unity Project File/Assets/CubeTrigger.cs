using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeTrigger : MonoBehaviour
{

    [SerializeField] private GameObject objItem;

    private objCheck objCheckScript;

    void Start(){
        objCheckScript = GameObject.Find("ObjectiveCheck").GetComponent<objCheck>();
    }
    void OnTriggerEnter(Collider item){
        if (item.CompareTag("Cube")) {
            objItem.SetActive(true);
            Destroy(item.gameObject);
            objCheckScript.CubeUpdate();
        }
        
    }
}
