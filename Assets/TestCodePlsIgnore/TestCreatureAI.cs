using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCreatureAI : MonoBehaviour
{

    [Header("Objects")]
    [SerializeField] Creature myCreature;
    [SerializeField] Creature targetCreature;

    [Header("Config")]
    [SerializeField] float sightRange = 10;
    float sightRangeSquared = 0;
    [SerializeField] LayerMask sightMask;
    [SerializeField] LayerMask terrainMask;

    public delegate void AIState();
    float stateTimer = 0;
    public AIState myState;


    void Awake(){
        myState = AttackState;
        sightRangeSquared = sightRange*sightRange;
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

    void AITick(){
        myState();
        stateTimer += Time.fixedDeltaTime;
    }

    bool CanSeeTarget(){
        //creature too far away
        if((myCreature.transform.position - targetCreature.transform.position).sqrMagnitude > sightRangeSquared){
            return false;
        }

        if(Physics2D.Linecast(myCreature.transform.position,targetCreature.transform.position, sightMask)){
            return false;
        }

        return true;

    }

    void ChangeState(AIState newState){
        stateTimer = 0;
        myState = newState;
    }

    void AttackState(){
        myCreature.AimTool(targetCreature.transform.position);

        myCreature.Move(new Vector3(0,0,0));

        //attack
        if(stateTimer > 1){
            myCreature.UseTool();
            stateTimer = 0;
        }

        if(!CanSeeTarget()){
            ChangeState(PatrolState);
        }
        //aim at target
        //shoot
    }

    Vector3 patrolDirection = new Vector3(1,0,0);
    void PatrolState(){


        //move back and forth
        //look for targets
        if(stateTimer < 1){
            return;
        }

        myCreature.Move(patrolDirection);
        if(Physics2D.OverlapCircle(myCreature.transform.position+patrolDirection,.1f,terrainMask) != null){
            patrolDirection *= -1;
            myCreature.Move(new Vector3(0,0,0));
            stateTimer = 0;
        }else if(Physics2D.OverlapCircle(myCreature.transform.position+patrolDirection-new Vector3(0,1,0),.1f,terrainMask) == null){
            patrolDirection *= -1;
            myCreature.Move(new Vector3(0,0,0));
            stateTimer = 0;
        }

        if(CanSeeTarget()){
            ChangeState(AttackState);
        }
    }
}
