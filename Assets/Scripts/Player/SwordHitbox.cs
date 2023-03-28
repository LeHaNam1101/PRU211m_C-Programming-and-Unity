using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHitbox : MonoBehaviour
{
    public int swordDamage = 1;
    public Collider2D swordCollider;
   
    // Start is called before the first frame update
    void Start()
    {
        if (swordCollider == null)
        {
            Debug.LogWarning("not set");
        }
        //swordCollider.GetComponent<Collider2D>();

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionEnter2D(Collision2D col)
    {
        col.gameObject.SendMessage("OnHit", swordDamage);
    }
    
    
}
