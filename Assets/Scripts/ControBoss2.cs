using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControBoss2 : MonoBehaviour
{
    public GameObject ExploBoss2;
    public GameObject player;
    public float Health = 35.0f;
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
        animator.SetBool("idleBoss2",true);
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
        /// khoang cach de attack
        float distance = Mathf.Abs(player.transform.position.x - transform.position.x);
        
        if( (Health<=35.0 && Health>15.0 || Health<12.0) && distance <=40 &&Time.time > LastShoot + 2.0f){
            
            moving = true;
            animator.SetBool("walkingBoss2", false);
            animator.SetBool("slashingBoss2", false);
            animator.SetBool("idleBoss2", false);

            animator.SetBool("skillBoss2", moving);
            
            StartCoroutine(Shoot());
            LastShoot = Time.time;
            if(Health<12.0) LastShoot += 0.5f;  
            else LastShoot += 5.0f;  
        }
        
        if((Health>=12.0 && Health<=15.0) && distance <= 40.0 && Time.time > LastShoot2 + 3.0f && skill2 == false ){
            skill2 = true;
            moving = true;
            animator.SetBool("idleBoss2", false);
            animator.SetBool("skillBoss2", false);
            animator.SetBool("slashingBoss2", false);

            animator.SetBool("walkingBoss2", moving);
            StartCoroutine(nearShoot());

            LastShoot2 = Time.time;
        }
        
    }

    IEnumerator Shoot(){
        
        Vector3 direction;
        if(transform.localScale.x > 0.0f) direction = Vector3.right;
        else{
            direction = Vector3.left;
        }
        transform.position = new Vector3(transform.position.x ,transform.position.y + 1.3f,transform.position.z);
        transform.position = new Vector3(transform.position.x ,transform.position.y - 1.3f,transform.position.z);
        
        yield return new WaitForSeconds(1.5f);
        var ranPos = new Vector3(Random.Range(4.0f, 22.0f),Random.Range(0.6f, 1.5f),0.0f);
        GameObject stone = Instantiate(ExploBoss2, ranPos , Quaternion.identity);
        stone.GetComponent<Explo_Boss>().SetDirection(direction);
        stone.GetComponent<Explo_Boss>().dame = 5.0f;

        Destroy(stone,2.0f);

        yield return new WaitForSeconds(3);
        animator.SetBool("skillBoss2", false);
        animator.SetBool("idleBoss2", true);
        
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

        transform.position = new Vector3(player.transform.position.x + 2.5f,player.transform.position.y+1.3f,player.transform.position.z);

        animator.SetBool("walkingBoss2", false);
        animator.SetBool("slashingBoss2", moving);

        yield return new WaitForSeconds(1.5f);
        isAttack = false;

        transform.position = oldPos;
        animator.SetBool("slashingBoss2", false);
        animator.SetBool("idleBoss2", moving);
    }

    public void takeDameFromPlayer(float Dame){
        Health = Health - Dame;
        if(Health <= 0){
            moving = true;
            animator.SetBool("dyingControBoss2", moving);
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
