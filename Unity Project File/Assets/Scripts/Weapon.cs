using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class Weapon : MonoBehaviour , IUsable
{
    [field: SerializeField]

    public UnityEvent OnUse { get; private set; }

    public void Use(GameObject actor)
    {
        OnUse?.Invoke();
    }
}
