using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbInteractable : Interactable
{
    public override void Interact(Creature creature)
    {
        if(creature.IsClimbing()){
            creature.SetModeWalk();
        }else{
            creature.SetModeClimb();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Creature")){
            other.GetComponent<Creature>().AddClimbSupport();
        }
    }

    void OnTriggerExit2D(Collider2D other){
        if(other.CompareTag("Creature")){
            other.GetComponent<Creature>().RemoveClimbSupport();
        }
    }
}
