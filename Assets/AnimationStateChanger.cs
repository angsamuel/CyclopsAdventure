using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateChanger : MonoBehaviour
{
    Animator animator;
    [SerializeField] string defaultState = "Idle";
    [SerializeField] string currentState = "";

    void Awake(){
        animator = GetComponent<Animator>();
    }
    void Start(){
        ChangeAnimationState(defaultState);
    }
    public void ChangeAnimationState(string newState, float speed = 1){
        animator.speed = speed;

        if(newState == currentState){
            return;
        }
        currentState = newState;
        animator.Play(currentState);

    }
}
