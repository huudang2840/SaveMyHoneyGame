using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWraith_2 : MonoBehaviour
{
    public GameObject StoneWraith2;
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
        if (direction.x >= 0.0f) transform.localScale = new Vector3(1.0f,1.0f,1.0f);
        else transform.localScale = new Vector3(-1.0f,1.0f,1.0f);

        /// khoang cach de attack
        float distance = Mathf.Abs(player.transform.position.x - transform.position.x);
        if(distance >= 15.0f){
            moving = true;
            animator.SetBool("throwingWraith2", false);
            animator.SetBool("walkingWraith2", moving);
            if(MoveRight){
                transform.Translate(2*Time.deltaTime*speed,0,0);
                transform.localScale = new Vector2(1.0f,1.0f);
                
            } else{
                transform.Translate(-2*Time.deltaTime*speed,0,0);
                transform.localScale = new Vector2(-1.0f,1.0f);
                
            }
        }
        if(distance < 15.0f && Time.time > LastShoot + 2.0f){
            moving = true;
            animator.SetBool("walkingWraith2", false);
            animator.SetBool("throwingWraith2", moving);
            StartCoroutine(Shoot());
            LastShoot = Time.time;   
        }
        
    }

    IEnumerator Shoot(){

        Vector3 direction;
        if(transform.localScale.x > 0.0f) direction = Vector3.right;
        else direction = Vector3.left;

        GameObject stone1 = Instantiate(StoneWraith2, transform.position + direction * 4.5f, Quaternion.identity);
        stone1.GetComponent<shootStone>().SetDirection(direction);
        stone1.GetComponent<shootStone>().dame = 3.0f;
        GameObject stone = Instantiate(StoneWraith2, transform.position + direction * 2.5f, Quaternion.identity);
        stone.GetComponent<shootStone>().SetDirection(direction);
        stone.GetComponent<shootStone>().dame = 3.0f;
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
    }
}
