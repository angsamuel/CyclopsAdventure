using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{

    [SerializeField] Creature playerCreature;

    void Awake(){

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    void Update(){
        //jump logic
        if(Input.GetKeyDown(KeyCode.Space)){
            playerCreature.Jump();
        }

        //managing tool

        if(Input.GetKey(KeyCode.Mouse0)){
            playerCreature.UseTool();
        }

        if(Input.GetKeyDown(KeyCode.F)){
            playerCreature.Interact();
        }

        if(Input.GetKeyDown(KeyCode.R)){
            playerCreature.Reload();
        }

        if(Input.GetKeyDown(KeyCode.Q)){
            TimeController.singleton.ToggleTime();
        }


    }


    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 movement = Vector3.zero;


        if(Input.GetKey(KeyCode.W)){ //holding down W
            movement += new Vector3(0,1,0);
        }
        if(Input.GetKey(KeyCode.S)){
            movement += new Vector3(0,-1,0);
        }
        if(Input.GetKey(KeyCode.A)){
            movement += new Vector3(-1,0,0);
        }
        if(Input.GetKey(KeyCode.D)){
            movement += new Vector3(1,0,0);
        }
        playerCreature.Move(movement);
        playerCreature.AimTool(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        //interaction code


    }
}
