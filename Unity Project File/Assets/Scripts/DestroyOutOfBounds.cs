using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private Vector3 spawnLocation;
    // Start is called before the first frame update
    void Start()
    {
        spawnLocation = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y <= -5){
            gameObject.transform.position = spawnLocation;
        }
    }
}
