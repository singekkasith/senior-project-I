using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour, IUsable
{
    public UnityEvent OnUse => throw new System.NotImplementedException();

    private int healthBoost = 1;

    public void Use(GameObject actor)
    {
        //actor.GetComponent<Player>().AddHealth(healthBoost);
        //Destroy(gameObject);
    }
}
