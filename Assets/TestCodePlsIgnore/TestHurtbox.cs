using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class TestHurtbox : MonoBehaviour
{

   [SerializeField] TestHealth health;
   public void Hurt(int damage){
      health.TakeDamage(damage);
   }
}
