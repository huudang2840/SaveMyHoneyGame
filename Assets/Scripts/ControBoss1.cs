using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControBoss1 : MonoBehaviour
{
    
    public GameObject ExploBoss1;
    public GameObject player;
    public float Health = 20.0f;
    private float LastShoot;
    private float LastShoot2;
    
    public float speed;
    public bool MoveRight;
    private Animator animator;
    bool moving;
    bool skill2 = false;
    bool isAttack = false;
    void Start(){
        animator = GetComponent<Animator>();
        moving = false;
        animator.SetBool("idleBoss1",true);
    }
    private void Update(){
        /// quay mat theo huong player
        if(player == null) return;
        if (isAttack == true) {
            this.GetComponent<BoxCollider2D>().enabled = true;
        }else {
            this.GetComponent<BoxCollider2D>().enabled = false;
        }
        Vector3 direction = player.transform.position - transform.position;
        if (direction.x >= 0.0f) transform.localScale = new Vector3(0.5f,0.5f,0.5f);
        else transform.localScale = new Vector3(-0.5f,0.5f,0.5f);
        float distance = Mathf.Abs(player.transform.position.x - transform.position.x);
        /// khoang cach de attack
        
        if( (Health<=35.0 && Health>11.0 || Health < 10) && distance <= 40.0f && Time.time > LastShoot + 3.0f){
            
            moving = true;
            animator.SetBool("walkingBoss1", false);
            animator.SetBool("slashingBoss1", false);
            animator.SetBool("idleBoss1", false);

            animator.SetBool("skillBoss1", moving);
            transform.position = new Vector3(transform.position.x ,transform.position.y + 1.3f,transform.position.z);
            StartCoroutine(Shoot());
            LastShoot = Time.time;  
            //LastShoot += 20.0f;  
        }
        
        if((Health >= 10.0 && Health <= 11.0f) && distance <= 40.0f &&Time.time > LastShoot2 + 3.0f && skill2 == false ){
            skill2 = true;
            moving = true;
            animator.SetBool("idleBoss1", false);
            animator.SetBool("skillBoss1", false);
            animator.SetBool("slashingBoss1", false);

            animator.SetBool("walkingBoss1", moving);
            StartCoroutine(nearShoot());

            LastShoot2 = Time.time;
        }
        
    }

    IEnumerator Shoot(){
        
        Vector3 direction;
        if(transform.localScale.x > 0.0f) direction = Vector3.right;
        else{
            direction = Vector3.left;
            ExploBoss1.transform.localScale = new Vector3(-0.5f,0.5f,0.5f);
        }
        yield return new WaitForSeconds(1.5f);
        transform.position = new Vector3(transform.position.x ,transform.position.y - 1.3f,transform.position.z);
        GameObject stone = Instantiate(ExploBoss1, transform.position + direction * 3.0f, Quaternion.identity);
        stone.GetComponent<shootStone>().SetDirection(direction);
        stone.GetComponent<shootStone>().dame = 5.0f;
        Destroy(stone,2.0f);

        yield return new WaitForSeconds(3);
        animator.SetBool("skillBoss1", false);
        animator.SetBool("idleBoss1", true);
        
    }
    IEnumerator nearShoot(){
        moving = true;
        Vector3 oldPos = transform.position;

        Vector3 direction;
        if(transform.localScale.x == 30.0f) direction = Vector3.right;
        else {
            direction = Vector3.left;
        }
        isAttack = true;
        transform.position = new Vector3(player.transform.position.x + 2.5f,player.transform.position.y + 1.3f,player.transform.position.z);
        animator.SetBool("slashingBoss1", moving);
        animator.SetBool("walkingBoss1", false);
        //Debug.Log('a');
        yield return new WaitForSeconds(1.5f);
        isAttack = false;
        transform.position = oldPos;
        animator.SetBool("slashingBoss1", false);
        animator.SetBool("idleBoss1", moving);
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

      CharacterController_2D player = other.GetComponent<CharacterController_2D>(); 
        if(player!=null) {
            player.takeDameFromMonster(3.0f);
        }

    }
}
