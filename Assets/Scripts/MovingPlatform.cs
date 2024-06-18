using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    [SerializeField] List<Transform> checkpoints;
    [SerializeField] float speed = 5;
    [SerializeField] float pauseTime = 1;

    int checkpointIndex = 0;
    bool moving = true;
    // Start is called before the first frame update
    void Start()
    {
        StartMoving(); //this never happens
    }

    // Update is called once per frame
    void Update()
    {

    }

    void StartMoving(){

        StartCoroutine(MoveRoutine());

        IEnumerator MoveRoutine(){ //NOT MULTITHREADING!!!!!!!!
            while(moving){
                //do some work
                transform.position = Vector3.MoveTowards(transform.position, checkpoints[checkpointIndex].transform.position,speed*Time.deltaTime);
                yield return new WaitForFixedUpdate(); //render the next

                if(Vector3.Distance(transform.position,checkpoints[checkpointIndex].transform.position) < .1f){
                    transform.position = checkpoints[checkpointIndex].transform.position;
                    checkpointIndex++;
                    if(checkpointIndex == checkpoints.Count){
                        checkpointIndex = 0;
                    }
                    yield return new WaitForSeconds(pauseTime);
                }

            }
            yield return null;

        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.GetComponent<Creature>() == null){
            return;
        }

        other.transform.parent = transform;

    }

    void OnTriggerExit2D(Collider2D other){
        if(other.GetComponent<Creature>() == null){
            return;
        }
        if(other.transform.parent != transform){
            return;
        }
        other.transform.parent = null;
    }


}
