using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnimationStateChanger : MonoBehaviour
{
    public Animator animator;
    public string currentState = "Idle";


    void Start(){
        ChangeAnimationState("Idle");
    }

    public void ChangeAnimationState(string newState, float speed = 1){

        animator.speed = speed;

        if(currentState == newState){ //already playing desired animation
            return;
        }

        currentState = newState;
        animator.Play(newState);
    }
}
