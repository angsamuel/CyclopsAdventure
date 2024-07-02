using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // void OnTriggerStay2D(Collider2D other){
    //     if(Vector3.Distance(transform.position,other.transform.position)<0.25f){
    //         GetComponent<SpriteRenderer>().color = Color.yellow;
    //     }
    //     Debug.Log("Stay Detected");
    // }

    void OnTriggerEnter2D(Collider2D other){

        GetComponent<SpriteRenderer>().color = Color.red;
        Debug.Log("Collision Detected!");
    }
    void OnTriggerExit2D(Collider2D other)
    {
        GetComponent<SpriteRenderer>().color = Color.white;
        Debug.Log("Exit Detected!");
    }
}
