using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    public Player playerHealth;

    public int healthBonus = 25;

    private void OnTriggerEnter(Collider other)
    {
        if (playerHealth.currentHealth < playerHealth.maxHealth)
        {
            playerHealth.currentHealth = playerHealth.currentHealth + healthBonus;
            Destroy(gameObject);
        }
    }

}
