using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] string checkpointName;
    [SerializeField] Creature playerCreature;


    void Start(){
        if(PlayerPrefs.GetString(SaveFlags.checkpointKey) == checkpointName){
            playerCreature.transform.position = transform.position;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<Creature>() != playerCreature){
            return;
        }
        Debug.Log("You reached checkpoint " + checkpointName);
        PlayerPrefs.SetString(SaveFlags.checkpointKey,checkpointName);
    }

}
