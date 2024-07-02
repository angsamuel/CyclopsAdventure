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
    [SerializeField] LayerMask terrainMask;
    [SerializeField] LayerMask sightMask;


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

    bool CanSeeTarget(Creature tempTarget){
        if(tempTarget == null){ //this was the issue
            return false;
        }
        if((myCreature.transform.position - tempTarget.transform.position).sqrMagnitude > sightRangeSquared){
            return false;
        }
        if(Physics2D.Linecast(myCreature.transform.position,tempTarget.transform.position,sightMask)){
            return false;
        }
        return true;
    }

    bool CanSeeTarget(){
        return CanSeeTarget(targetCreature);
    }

    void AttackState(){
        if(!CanSeeTarget()){
            ChangeState(PatrolState);
            return;
        }
        myCreature.Stop();

        myCreature.AimTool(targetCreature.transform.position);

        if(stateTime < 1){
            return;
        }

        Debug.Log("AttackState");
        myCreature.UseTool();
        ResetStateTime();
    }


    int patrolDirection = 1;
    void PatrolState(){

        SearchForTarget();



        if(CanSeeTarget()){
            ChangeState(AttackState);
            return;
        }

        if(stateTime < 1){
            myCreature.Stop();
            return;
        }

        myCreature.Move(new Vector3(patrolDirection,0,0));


        Vector3 footCheckPosition = myCreature.transform.position + new Vector3(patrolDirection,0,0) - new Vector3(0,1,0);
        Vector3 moveCheckPosition = myCreature.transform.position + new Vector3(patrolDirection,0,0);
        if(Physics2D.OverlapCircle(moveCheckPosition,.1f,terrainMask) != null){
            ResetStateTime();
            Debug.Log("something blocks us");
            patrolDirection *= -1;
        }
        else if(Physics2D.OverlapCircle(footCheckPosition,.1f,terrainMask) == null){
            ResetStateTime();
            Debug.Log("no ground ahead");
            patrolDirection *= -1;
        }
    }

    void SearchForTarget(){

        foreach(Creature c in CreatureManager.singleton.GetCreatures()){
            //
            if(c == myCreature){
                continue; //we don't wanna target ourselves!
            }
            if(CanSeeTarget(c)){
                Debug.Log(c.gameObject.name);
                targetCreature = c;
            }
        }
    }
}
