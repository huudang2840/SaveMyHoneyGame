using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControBoss3 : MonoBehaviour
{
    public GameObject ExploBoss3;
    public GameObject ExploBoss3_1;
    public GameObject ExploBoss3_2;
    public GameObject player;
    public float Health = 35.0f;
    private float LastShoot;
    private float LastShoot1;
    private float LastShoot2;
    
    public float speed;
    public bool MoveRight;
    private Animator animator;
    bool moving;
    bool skill1 = false;
    bool skill2 = false;
    bool isAttack = false;

    void Start(){
        animator = GetComponent<Animator>();
        moving = false;
        animator.SetBool("idleBoss3",true);
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
        
        if( (Health<=35.0 && Health>=21.0 || Health<10.0) && distance <=40 && Time.time > LastShoot + 2.0f){
            
            moving = true;
            animator.SetBool("walkingBoss3", false);
            animator.SetBool("slashingBoss3", false);
            animator.SetBool("idleBoss3", false);

            animator.SetBool("skillBoss3", moving);
            transform.position = new Vector3(transform.position.x ,0.24f,transform.position.z);
            StartCoroutine(Shoot());
            LastShoot = Time.time;  
            if(Health<10.0) LastShoot += 2.0f;  
            else LastShoot += 8.0f;   
        }
        if( (Health>=10.0 && Health<=18.0) && distance <=40 && Time.time > LastShoot1 + 2.0f && skill1 == false){
            skill1 = true;
            moving = true;
            animator.SetBool("walkingBoss3", false);
            animator.SetBool("slashingBoss3", false);
            animator.SetBool("idleBoss3", false);

            animator.SetBool("skillBoss3", moving);
            transform.position = new Vector3(transform.position.x ,0.24f,transform.position.z);
            StartCoroutine(Thunder());

            LastShoot1 = Time.time;  
            //LastShoot1 += 8.0f;  
        }
        if((Health>18.0 && Health<21.0) && distance <=40 && Time.time > LastShoot2 + 2.0f && skill2 == false ){
            skill2 = true;
            moving = true;
            animator.SetBool("idleBoss3", false);
            animator.SetBool("skillBoss3", false);
            animator.SetBool("slashingBoss3", false);

            animator.SetBool("walkingBoss3", moving);
            StartCoroutine(nearShoot());

            LastShoot2 = Time.time;
        }
        
    }

    IEnumerator Shoot(){
        
        Vector3 direction;
        if(transform.localScale.x > 0.0f) direction = Vector3.right;
        else{
            direction = Vector3.left;
            ExploBoss3_2.transform.localScale = new Vector3(-0.5f,0.5f,0.5f);
        }
        
        yield return new WaitForSeconds(1.5f);

        GameObject stone = Instantiate(ExploBoss3_2, transform.position + direction * 3.5f, Quaternion.identity);
        stone.GetComponent<shootStone>().SetDirection(direction);
        stone.GetComponent<shootStone>().dame = 5.0f;
        Destroy(stone,2.0f);

        yield return new WaitForSeconds(2.0f);
        animator.SetBool("skillBoss3", false);
        animator.SetBool("idleBoss3", true);
        transform.position = new Vector3(transform.position.x ,-0.65f,transform.position.z);
    }
    IEnumerator Thunder(){
        Vector3 ranPos;
        Vector3 direction;
        if(transform.localScale.x > 0.0f) direction = Vector3.right;
        else{
            direction = Vector3.left;
        }
        
        yield return new WaitForSeconds(1.5f);
        GameObject stone = Instantiate(ExploBoss3, ranPos = new Vector3(Random.Range(3.0f, 23.0f),Random.Range(5.0f, 5.5f),0.0f) , Quaternion.identity);
        stone.GetComponent<Explo_Boss>().SetDirection(direction);
        stone.GetComponent<Explo_Boss>().dame = 5.0f;
        Destroy(stone,2.0f);
        
        GameObject stone1 = Instantiate(ExploBoss3_1, ranPos = new Vector3(Random.Range(3.0f, 23.0f),Random.Range(5.0f, 5.5f),0.0f) , Quaternion.identity);
        stone.GetComponent<Explo_Boss>().SetDirection(direction);
        stone.GetComponent<Explo_Boss>().dame = 5.0f;
        Destroy(stone1,1.5f);
        
        GameObject stone2 = Instantiate(ExploBoss3, ranPos = new Vector3(Random.Range(3.0f, 23.0f),Random.Range(5.0f, 5.5f),0.0f) , Quaternion.identity);
        stone.GetComponent<Explo_Boss>().SetDirection(direction);
        stone.GetComponent<Explo_Boss>().dame = 5.0f;
        Destroy(stone2,2.0f);
        
        GameObject stone3 = Instantiate(ExploBoss3_1, ranPos = new Vector3(Random.Range(3.0f, 23.0f),Random.Range(5.0f, 5.5f),0.0f) , Quaternion.identity);
        stone.GetComponent<Explo_Boss>().SetDirection(direction);
        stone.GetComponent<Explo_Boss>().dame = 5.0f;
        Destroy(stone3,1.5f);

        yield return new WaitForSeconds(3);
        animator.SetBool("skillBoss3", false);
        animator.SetBool("idleBoss3", true);
        transform.position = new Vector3(transform.position.x ,-0.65f,transform.position.z);
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
        transform.position = new Vector3(player.transform.position.x + 2.0f,0.65f,player.transform.position.z);
        animator.SetBool("walkingBoss3", false);
        animator.SetBool("slashingBoss3", moving);

        yield return new WaitForSeconds(1.5f);
        isAttack = false;
        transform.position = oldPos;
        animator.SetBool("slashingBoss3", false);
        animator.SetBool("idleBoss3", moving);
        //transform.position = new Vector3(player.transform.position.x + 2.5f,-0.65f,player.transform.position.z);
    }

    public void takeDameFromPlayer(float Dame){
        Health = Health - Dame;
        if(Health <= 0){
            moving = true;
            animator.SetBool("dyingControBoss3", moving);
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
            player.takeDameFromMonster(5.0f);;
        }

    }
}
