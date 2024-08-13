using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingAI : AI
{

    public delegate void AIState();
    public AIState currentState;

    float stateTime = 0;

    void Awake(){
        base.Awake();
        currentState = PatrolState;
    }

    // Start is called before the first frame update
    void Start()
    {

        if(IsWalkable(marker.transform.position)){
            Debug.Log("Walkable");
        }else{
            Debug.Log("Not walkable");
        }
        // List<Vector3> path = AStarPath(transform.position,targetCreature.transform.position);
        // foreach(Vector3 p in path){
        //     Instantiate(marker, p, Quaternion.identity);
        // }
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





    void PursuitState(){

        if(stateTime<3){
            myCreature.Stop();
            ClearPath();
            return;
        }

        if(path.Count == 0){
            AStarPath(myCreature.transform.position,targetCreature.transform.position);
            // foreach(Vector3 p in path){
            //     Instantiate(marker, p, Quaternion.identity);
            // }
            if(path.Count == 0){
                ChangeState(PatrolState);
                return;
            }
        }

        if(CanSeeTarget()){
            ChangeState(AttackState);
            return;
        }

        Vector3 nextStop = path[path.Count-1];
        myCreature.MoveToward(nextStop);

        if(Vector3.Distance(myCreature.transform.position,nextStop) < myCreature.GetSpeed()*Time.fixedDeltaTime){
            path.RemoveAt(path.Count-1);
        }
        if(path.Count == 0){
            ChangeState(PatrolState);
        }

    }



    void PatrolState(){
        targetCreature = null; //lose target
        SearchForTarget();
        if(CanSeeTarget()){
            ChangeState(AttackState);
        }
    }

    void AttackState(){
        if(CanSeeTarget()){
            myCreature.MoveToward(targetCreature.transform.position);
            return;
        }

        ChangeState(PursuitState);


    }




}
