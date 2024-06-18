using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TestSwitch : TestInteractable
{

    public override void Interact(Creature creature){

        onInteractEvent.Invoke();
        GetComponent<SpriteRenderer>().flipY = true;
    }
}
