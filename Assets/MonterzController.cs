using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonterzController : MonoBehaviour
{
    public float Speed = 2f;
    public Transform target;
    public int Health = 2;
    public int damage = 15;
    private int currentHealth;
    private Rigidbody2D rb;
    public float knockbackForce;
    public float knockBackTime = 5f;
    private Vector3 initialPosition;
    private PlayerController playerController;
    private MonterzController monterzController;

    void Start()
    {
        currentHealth = Health;
        rb = GetComponent<Rigidbody2D>();

    }

    void FixedUpdate()
    {
        if (target != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            rb.velocity = direction * Speed;
        }
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (monterzController != null)
        {
            damage = (int)monterzController.damage;
        }
        if (collision.transform.tag == "Tower")
        {
            Rigidbody2D rb = transform.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                //rb.isKinematic = false;
                Vector2 dir = -(collision.transform.position - transform.position);
                dir = dir.normalized * 10;
                rb.AddForce(dir, ForceMode2D.Impulse);
                StartCoroutine(knockBack(rb));
            }
        }
    }

    private IEnumerator knockBack(Rigidbody2D rb)
    {
        if (rb != null)
        {
            yield return new WaitForSeconds(knockBackTime);
            rb.velocity = Vector2.zero;
            //rb.isKinematic = true;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) // Nếu va chạm với nhân vật
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                _ = player.attackDamage; // Trừ máu của nhân vật
                Destroy(gameObject); // Biến mất khỏi màn hình
            }
        }
    }

    private void Die()
    {
        GetComponent<LootBag>().InstantiatateLoot(transform.position);
        // Khi quái vật chết, xóa nó khỏi scene
        Destroy(gameObject);
    }
    void OnHit(int damage)
    {
        currentHealth -= damage;
    }
}