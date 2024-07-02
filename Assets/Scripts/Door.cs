using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    [SerializeField] Sprite openDoorSprite;
    [SerializeField] Sprite closedDoorSprite;
    bool isOpen = false;

    void Start(){
        //OpenDoor();
    }

    public void OpenDoor(){
        GetComponent<BoxCollider2D>().enabled = false;
        //
        GetComponent<SpriteRenderer>().sprite = openDoorSprite;
        isOpen = true;
    }

    public void CloseDoor(){
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<SpriteRenderer>().sprite = closedDoorSprite;
        isOpen = false;
    }

    public void ToggleDoor(){
        if(isOpen){
            CloseDoor();
        }else{
            OpenDoor();
        }
    }


}
