using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWraith_3 : MonoBehaviour
{
public GameObject StoneWraith3;
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
        if(distance >= 30.0f){
            moving = true;
            animator.SetBool("throwingWraith3", false);
            animator.SetBool("walkingWraith3", moving);
            if(MoveRight){
                transform.Translate(2*Time.deltaTime*speed,0,0);
                transform.localScale = new Vector2(1.0f,1.0f);
                
            } else{
                transform.Translate(-2*Time.deltaTime*speed,0,0);
                transform.localScale = new Vector2(-1.0f,1.0f);
                
            }
        }
        if(distance < 30.0f && Time.time > LastShoot + 2.0f){
            moving = true;
            animator.SetBool("walkingWraith3", false);
            animator.SetBool("throwingWraith3", moving);
            StartCoroutine(Shoot());
            LastShoot = Time.time;   
        }
        
    }

    IEnumerator Shoot(){

        Vector3 direction;
        Vector3 dir = player.transform.position;
        dir = Vector3.down;
        if(transform.localScale.x > 0.0f) direction = Vector3.right;
        else direction = Vector3.left;

        GameObject stone1 = Instantiate(StoneWraith3, transform.position + direction * 2.5f, Quaternion.identity);
        stone1.GetComponent<shootStone>().dame = 3.0f;
        stone1.GetComponent<shootStone>().SetDirection(direction);
        GameObject stone2 = Instantiate(StoneWraith3, StoneWraith3.transform.position = new Vector3(player.transform.position.x,15.0f,player.transform.position.z) , Quaternion.identity);
        stone2.GetComponent<shootStone>().SetDirection(dir);
        stone2.GetComponent<shootStone>().dame = 3.0f;
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
