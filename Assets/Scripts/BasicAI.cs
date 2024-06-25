using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAI : MonoBehaviour
{

    [Header("Objects")]
    [SerializeField] Creature myCreature;
    [SerializeField] Creature targetCreature;

    [Header("Config")]
    [SerializeField] float sightRange = 5;
    float sightRangeSquared;


    public delegate void AIState();
    public AIState currentState;

    float stateTime = 0;

    void Awake(){

        currentState = PatrolState;
        sightRangeSquared = sightRange * sightRange;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        AITick();
    }

    void ResetStateTime(){
        stateTime = 0;
    }

    void ChangeState(AIState newState){
        currentState = newState;
        ResetStateTime();
    }

    void AITick(){
        stateTime+=Time.fixedDeltaTime;
        currentState();
    }

    bool CanSeeTarget(){
        if((myCreature.transform.position - targetCreature.transform.position).sqrMagnitude <= sightRangeSquared){
            return true;
        }
        return false;
    }

    void AttackState(){
        if(!CanSeeTarget()){
            ChangeState(PatrolState);
            return;
        }

        myCreature.AimTool(targetCreature.transform.position);

        if(stateTime < 1){
            return;
        }

        Debug.Log("AttackState");
        myCreature.UseTool();
        ResetStateTime();


    }

    void PatrolState(){
        Debug.Log("PatrolState");
        if(CanSeeTarget()){
            ChangeState(AttackState);
        }
    }
}
