using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawnable : MonoBehaviour
{
    public float TotalLifetime;

    private void Start()
    {
        Destroy(this.gameObject, TotalLifetime);
    }
}
