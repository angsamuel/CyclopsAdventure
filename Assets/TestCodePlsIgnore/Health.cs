using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int maxHealth = 3;
    [SerializeField] int currentHealth = 3;

    public int GetHealth(){
        return currentHealth;
    }

    public void TakeDamage(int damage){
        if(damage < 0){
            damage = 0;
        }

        currentHealth -= damage;
        if(currentHealth < 0){
            currentHealth = 0;
        }
    }

    public void Heal(int healing){
        if(healing < 0){
            healing = 0;
        }

        currentHealth += healing;
        if(currentHealth > maxHealth){
            currentHealth = maxHealth;
        }
    }
}
