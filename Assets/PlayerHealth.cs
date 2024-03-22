using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Deals with player character's health

public class PlayerHealth : MonoBehaviour
{

    // Initialises appropiate states regarding players health numbers

    public static int maxHealth = 100;
    public int health;
    public HealthBar healthBar;
    public bool dead = false;

    // Start is called before the first frame update, sets health and health bar to 100
    void Start()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame, checks if player has died
    void Update()
    {
        if (health <= 0 && dead == false)
        {
            dead = true;
            SoundManagerScript.PlaySound("deathSound");
        }
    }

    // Responsible for player taking damage
    public void PlayerDamage(int dmg)
    {
        health -= dmg;
        healthBar.Health(health);
        SoundManagerScript.PlaySound("hitSound");
    }

    // Increases player max health to desired value
    public void IncreaseMaxHealth(int newMaxHealth)
    {
        maxHealth = newMaxHealth;
        health = maxHealth;
    }
}
