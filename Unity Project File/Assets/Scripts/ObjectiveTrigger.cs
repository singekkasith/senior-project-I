using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveTrigger : MonoBehaviour
{
    [SerializeField] private GameObject treeGrowth0;
    [SerializeField] private GameObject treeGrowth1;
    [SerializeField] private GameObject treeGrowth2;
    [SerializeField] private GameObject treeGrowth3;

    [SerializeField] private GameObject objItem;

    [Range(0,4)]
    // 0 = health, 1 = pistol
    public int treeProgression;

    private objCheck objCheckScript;

    void Start(){
        objCheckScript = GameObject.Find("ObjectiveCheck").GetComponent<objCheck>();
    }
    void OnTriggerEnter(Collider item){
        if (item.CompareTag("Item") && treeProgression < 5) {
            treeProgression += 1;
            Destroy(item.gameObject);
            switch(treeProgression){
                case 0:
                    treeGrowth0.SetActive(false);
                    treeGrowth1.SetActive(false);
                    treeGrowth2.SetActive(false);
                    treeGrowth3.SetActive(false);
                    break;
                case 1:
                    treeGrowth0.SetActive(true);
                    treeGrowth1.SetActive(false);
                    treeGrowth2.SetActive(false);
                    treeGrowth3.SetActive(false);
                    
                    break;
                case 2:
                    treeGrowth0.SetActive(false);
                    treeGrowth1.SetActive(true);
                    treeGrowth2.SetActive(false);
                    treeGrowth3.SetActive(false);
                    
                    break;
                case 3:
                    treeGrowth0.SetActive(false);
                    treeGrowth1.SetActive(false);
                    treeGrowth2.SetActive(true);
                    treeGrowth3.SetActive(false);
                    
                    break;
                case 4:
                    treeGrowth0.SetActive(false);
                    treeGrowth1.SetActive(false);
                    treeGrowth2.SetActive(false);
                    treeGrowth3.SetActive(true);
                    objItem.SetActive(true);
                    break;  
            }
            objCheckScript.TreeUpdate();
            
        }
        
    }
}
