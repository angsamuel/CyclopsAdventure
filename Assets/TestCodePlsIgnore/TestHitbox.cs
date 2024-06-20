using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHitbox : MonoBehaviour
{
    [SerializeField] int damage = 1;
    public void ActivateHB(){
        GetComponent<Collider2D>().enabled = true;
    }

    public void DisableHB(){
        GetComponent<Collider2D>().enabled = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("TRIGGER 1");
        if(other.CompareTag("Hurtbox")){
            Debug.Log("Trigger 2");
            other.GetComponent<TestHurtbox>().Hurt(damage);
        }
    }
}
