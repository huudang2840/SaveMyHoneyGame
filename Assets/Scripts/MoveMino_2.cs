using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMino_2 : MonoBehaviour
{
    public GameObject StoneMino;
    public GameObject player;
    public float Health = 5.0f;
    private float LastShoot;
    
    public float speed;
    public bool MoveRight;
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
        if(distance >= 10.0f){
            moving = true;
            animator.SetBool("throwingMino", false);
            animator.SetBool("walkingMino2", moving);
            if(MoveRight){
                transform.Translate(2*Time.deltaTime*speed,0,0);
                transform.localScale = new Vector2(0.6f,0.6f);
                
            } else{
                transform.Translate(-2*Time.deltaTime*speed,0,0);
                transform.localScale = new Vector2(-0.6f,0.6f);
                
            }
        }
        if(distance < 10.0f && Time.time > LastShoot + 1.5f){
            moving = true;
            animator.SetBool("walkingMino2", false);
            animator.SetBool("throwingMino", moving);
            StartCoroutine(Shoot());
            LastShoot = Time.time;   
        }
        
    }

    IEnumerator Shoot(){

        Vector3 direction;
        if(transform.localScale.x > 0.0f) direction = Vector3.right;
        else{
            direction = Vector3.left;
            StoneMino.transform.localScale = new Vector3(-1.0f,1.0f,1.0f);
        }

        GameObject stone = Instantiate(StoneMino, transform.position + direction * 3.0f, Quaternion.identity);
        stone.GetComponent<shootStone>().dame = 2.0f;
        stone.GetComponent<shootStone>().SetDirection(direction);
        yield return new WaitForSeconds(5);
    }
    public void takeDameFromPlayer(float Dame){
        Health = Health - Dame;
        if(Health <= 0){
            moving = true;
            animator.SetBool("dyingGoblin1", moving);
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
        
    }

}
