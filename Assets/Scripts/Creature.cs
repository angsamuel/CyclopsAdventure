using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    public static string favoriteFood = "PB&J";
    enum MovementMode{walk, climb};
    public bool isPlayerCreature = false;

    [Header("Stats")]
    [SerializeField] float speed = 10.0f;
    [SerializeField] float jumpForce = 10;


    [Header("Helpers")]
    [SerializeField] LayerMask jumpMask;
    [SerializeField] Transform footMarker;
    [SerializeField] ParticleSystem jumpPoof;
    Health health;

    [Header("Trackers")]
    [SerializeField] Crossbow crossbow;
    [SerializeField] Interactable currentInteractable;
    [SerializeField] MovementMode moveMode = MovementMode.walk;
    [SerializeField] int climbSupports = 0;
    float cachedDefaultGravity = 0;

    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;



    void Awake(){
        rb = GetComponent<Rigidbody2D>();
        cachedDefaultGravity = rb.gravityScale;
        spriteRenderer = GetComponent<SpriteRenderer>();
        health = GetComponent<Health>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Creature start!");
        CreatureManager.singleton.RegisterCreature(this);

    }


    // Update is called once per framehttps://aka.ms/vscode-workspace-trust
    void Update()
    {
        if(health.GetHealth() < 1){
            Die();
        }

    }

    void Die(){
        GetComponent<SpriteRenderer>().color = Color.red;
        GetComponent<CapsuleCollider2D>().isTrigger = true;
    }
    public void Stop(){
        Move(Vector3.zero);
    }
    public void Move(Vector3 movement){

        if(movement == Vector3.zero){
            GetComponent<AnimationStateChanger>().ChangeAnimationState("Idle");
        }else{
            GetComponent<AnimationStateChanger>().ChangeAnimationState("Walk", speed / 5);
        }

        movement *= speed;

        if(movement.x < 0){
            spriteRenderer.flipX = true;
        }
        if(movement.x > 0){
            spriteRenderer.flipX = false;
        }

        if(IsClimbing()){
            rb.velocity = movement;
            return;
        }

        //GetComponent<Transform>().position += movement;
        //transform.position += movement;
        movement.y = 0;
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
        jumpPoof.Play();
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



    //interactable code


    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Interactable")){
            currentInteractable = other.GetComponent<Interactable>();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Interactable")){
            if(other.GetComponent<Interactable>() == currentInteractable){
                currentInteractable = null;
            }
        }
    }

    public void Interact(){
        if(currentInteractable == null){
            return;
        }
        currentInteractable.Interact(this);
    }

    //Climbing Behavior

    public void SetModeWalk(){
        rb.gravityScale = cachedDefaultGravity;
        moveMode = MovementMode.walk;
    }
    public void SetModeClimb(){
        rb.gravityScale = 0;
        moveMode = MovementMode.climb;
    }
    public bool IsClimbing(){
        return moveMode == MovementMode.climb;
    }



    public void AddClimbSupport(){
        climbSupports += 1;
    }
    public void RemoveClimbSupport(){
        climbSupports -= 1;

        //just in case :)
        if(climbSupports < 0){
            climbSupports = 0;
        }

        if(climbSupports == 0 && IsClimbing()){
            SetModeWalk();
        }
    }



}
