using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{

    [SerializeField] int health = 3;
    [SerializeField] float speed = 10.0f;
    [SerializeField] float jumpForce = 10;
    [SerializeField] LayerMask jumpMask;
    [SerializeField] Transform footMarker;

    [SerializeField] Crossbow crossbow;

    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;


    void SetHealth(int newHealth){
        health = newHealth;
        if(health < 0){
            health = 0;
        }
    }

    void Awake(){
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Creature start!");

    }

    // Update is called once per framehttps://aka.ms/vscode-workspace-trust
    void Update()
    {


    }

    public void Move(Vector3 movement){
        movement *= speed;

        if(movement.x < 0){
            spriteRenderer.flipX = true;
        }
        if(movement.x > 0){
            spriteRenderer.flipX = false;
        }

        //GetComponent<Transform>().position += movement;
        //transform.position += movement;

        rb.velocity = movement + new Vector3(0,rb.velocity.y,0);
        //GetComponent<Rigidbody2D>().AddForce(movement * Time.fixedDeltaTime);
        //GetComponent<Rigidbody2D>().MovePosition(GetComponent<Transform>().position + movement * Time.fixedDeltaTime);
    }

    IEnumerator JumpBuffer(){
        float bufferTime = .25f;
        float timer = 0;
        while(timer < bufferTime){
            yield return null;
            timer+=Time.deltaTime;
            if(CanJump()){
                Jump();
                break;
            }
        }
        yield return null;
    }

    bool CanJump(){
        if(Physics2D.OverlapCircle(footMarker.position,.25f,jumpMask) != null){
            return true;
        }else{
            return false;
        }
    }

    public void Jump(){
        if(!CanJump()){
            StartCoroutine(JumpBuffer());
            return;
        }
        rb.velocity = new Vector2(rb.velocity.x,0);
        rb.AddForce(new Vector3(0,1,0)*jumpForce,ForceMode2D.Impulse);
    }

    public void AimTool(Vector3 targetPosition){
        crossbow.Aim(targetPosition);
    }
    public void UseTool(){
        crossbow.Shoot();
    }

    public Crossbow GetTool(){
        return crossbow;
    }



}
