using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventInteractable : Interactable
{

    [SerializeField] UnityEvent onInteractEvent;



    public override void Interact(Creature creature)
    {
        onInteractEvent.Invoke();
    }
}
