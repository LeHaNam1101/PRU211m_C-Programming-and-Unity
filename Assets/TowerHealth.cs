using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBarTower healthBarTower;
   
    [SerializeField] public GameObject EndGamePanel;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBarTower.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
            // End game and save data to json
            // 
            
            Time.timeScale = 0f;
            EndGamePanel.SetActive(true);
        }
        healthBarTower.SetHealth(currentHealth);
    }

    public void Heal(int heal)
    {
        currentHealth += heal;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthBarTower.SetHealth(currentHealth);
    }
}
