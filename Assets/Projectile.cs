using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    bool flying = false;
    Rigidbody2D rb;
    void Awake(){
        rb = GetComponent<Rigidbody2D>();
    }
    public void Launch(float speed){
        flying = true;
        GetComponent<Rigidbody2D>().velocity = transform.up * speed;
    }

    void Update(){
        if(flying){
            transform.rotation = Quaternion.LookRotation(Vector3.forward, rb.velocity);
        }

    }

    void OnTriggerEnter2D(Collider2D other){
       if(other.tag == "Terrain"){
        transform.parent = other.transform;
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        GetComponent<Rigidbody2D>().gravityScale = 0;
        GetComponent<Hitbox>().DisableHitbox();
        flying = false;

       }
    }


}
