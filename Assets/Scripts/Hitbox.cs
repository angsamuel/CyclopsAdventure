using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [SerializeField] int damage = 1;
    bool disabled = false;
    public void DisableHitbox(){
        disabled = true;
    }
    void OnTriggerEnter2D(Collider2D other){
        if(disabled){
            return;
        }
        if(other.GetComponent<Hurtbox>() != null){
            other.GetComponent<Hurtbox>().Hurt(damage);
        }
    }
}
