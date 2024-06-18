using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class TestInteractable : MonoBehaviour
{
    [SerializeField] protected UnityEvent onInteractEvent;
    public abstract void Interact(Creature creature);
}
