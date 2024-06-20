using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHealth : MonoBehaviour
{

    [SerializeField] int maxHealth = 3;
    [SerializeField] int currentHealth = 3;


    public void TakeDamage(int damage){
        currentHealth -= damage;
        if(currentHealth < 0){
            currentHealth = 0;
        }
    }

    public void Heal(int healing){
        currentHealth += healing;
        if(currentHealth > maxHealth){
            currentHealth = maxHealth;
        }
    }


}
