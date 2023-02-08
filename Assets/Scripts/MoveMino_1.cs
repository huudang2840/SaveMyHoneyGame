using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMino_1 : MonoBehaviour
{
   public GameObject player;
    public float Health = 5.0f;
    public bool MoveRight;
    private float LastShoot;
    public float speed;
    private Animator animator;
    bool moving;

    void Start(){
        animator = GetComponent<Animator>();
        moving = false;
    }
    private void Update(){
        /// quay mat theo huong player
        if(player == null) return;
        
        Vector3 direction = player.transform.position - transform.position;
        if (direction.x >= 0.0f) transform.localScale = new Vector3(0.6f,0.6f,0.6f);
        else transform.localScale = new Vector3(-0.6f,0.6f,0.6f);

        /// khoang cach de attack
        float distance = Mathf.Abs(player.transform.position.x - transform.position.x);
        if(distance > 3.0f){
            moving = true;
            animator.SetBool("slashingMino1", false);
            animator.SetBool("walkingMino1", moving);
            if(MoveRight){
                transform.Translate(2*Time.deltaTime*speed,0,0);
                transform.localScale = new Vector2(0.6f,0.6f);
            } else{
                transform.Translate(-2*Time.deltaTime*speed,0,0);
                transform.localScale = new Vector2(-0.6f,0.6f);
                
            }
        }
        
        if(distance <= 3.0f && Time.time > LastShoot + 1.5f){
            moving = true;
            animator.SetBool("walkingMino1", false);
            animator.SetBool("slashingMino1", moving);
            StartCoroutine(nearShoot());
            LastShoot = Time.time;   
        }
        
    }

    IEnumerator nearShoot(){
        Vector3 direction;
        if(transform.localScale.x > 7.0f) direction = Vector3.right;
        else direction = Vector3.left;
        
        yield return new WaitForSeconds(5);
    }

    public void takeDameFromPlayer(float Dame){
        Health = Health - Dame;
        if(Health <= 0){
            moving = true;
            animator.SetBool("dying", moving);
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Terrain")){
            if(MoveRight){
                MoveRight = false;
            } else{
                MoveRight = true;
            }
        }


        CharacterController_2D player = other.GetComponent<CharacterController_2D>(); 
            if(player!=null) {
                player.takeDameFromMonster(3.0f);
        }

    }
}
