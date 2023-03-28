using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour,ICollectible
{
    public static event Action OnCoinCollected;
    public void Collect()
    {
        Debug.Log("Heal collected");
        Destroy(gameObject);
    }
}
