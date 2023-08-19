using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   

public class Enemy : MonoBehaviour
{
    public float startSpeed = 10f;
    [HideInInspector]
   public float speed;
   public float starthealth = 100;
   private float health;
   public int worth = 50;
   public GameObject deathEffect;

   [Header("Unity Stuff")]
   public Image healthBar; 
    private bool isDead = false;
   void Start()
   {
    speed = startSpeed;
    health = starthealth;
   }
   
   public void TakeDamage (float amount)
   {
    health -= amount;

    healthBar.fillAmount = health / starthealth;
    if (health <=0  && !isDead)
    {
        Die();
    }
   }
   public void Slow (float pct)
   {
    speed = startSpeed * (1f - pct);
   }
   void Die()
   {
    isDead = true;
    PlayerStats.Money += worth;
    GameObject effect = (GameObject) Instantiate(deathEffect, transform.position,Quaternion.identity);
    Destroy(effect,5f);

    WaveSpawner.EnemiesAlive--;
    WaveSpawner2.EnemiesAlive--;
    Destroy(gameObject);
   }
   
}
