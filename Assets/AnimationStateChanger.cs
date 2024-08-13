using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateChanger : MonoBehaviour
{

    public bool animationsActive = true;
    Animator animator;
    [SerializeField] string defaultState = "Idle";
    [SerializeField] string currentState = "";

    void Awake(){
        animator = GetComponent<Animator>();
    }
    void Start(){
        if(!animationsActive){
            return;
        }
        ChangeAnimationState(defaultState);
    }
    public void ChangeAnimationState(string newState, float speed = 1){
        if(!animationsActive){
            return;
        }
        animator.speed = speed;

        if(newState == currentState){
            return;
        }
        currentState = newState;
        animator.Play(currentState);

    }
}
