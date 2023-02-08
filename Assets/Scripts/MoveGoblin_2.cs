using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGoblin_2 : MonoBehaviour
{

    // [SerializeField] private string id;

    public GameObject Stone;
    public GameObject player;
    public float Health = 5.0f;
    private float LastShoot;
    // bool isDied = false;
    
    public float speed;
    public bool MoveRight;
    private Animator animator;
    bool moving;
    private SpriteRenderer visual;

    // [ContextMenu("Genarate ID")]
    // private void GenerateGuid() {
    //     id = System.Guid.NewGuid().ToString();
    // }

    // public void LoadData(GameData data) {
    //     data.monsterDied.TryGetValue(id, out isDied);
    //     if(isDied) {
    //         visual.gameObject.SetActive(false);
    //     }
    // }

    // public void SaveData(ref GameData data) {
    //     if(data.monsterDied.ContainsKey(id)) {
    //         data.monsterDied.Remove(id);
    //     }
    //     data.monsterDied.Add(id, isDied);
    // }
    void Start(){
        animator = GetComponent<Animator>();
        moving = false;
    }
    private void Update(){
        /// quay mat theo huong player
        if(player == null) return;
        
        Vector3 direction = player.transform.position - transform.position;
        if (direction.x >= 0.0f) transform.localScale = new Vector3(0.4f,0.4f,0.4f);
        else transform.localScale = new Vector3(-0.4f,0.4f,0.4f);

        /// khoang cach de attack
        float distance = Mathf.Abs(player.transform.position.x - transform.position.x);
        if(distance >= 10.0f){
            moving = true;
            animator.SetBool("throwing", false);
            animator.SetBool("walkingGoblin2", moving);
            if(MoveRight){
                transform.Translate(2*Time.deltaTime*speed,0,0);
                transform.localScale = new Vector2(0.4f,0.4f);
                
            } else{
                transform.Translate(-2*Time.deltaTime*speed,0,0);
                transform.localScale = new Vector2(-0.4f,0.4f);
                
            }
        }
        if(distance < 10.0f && Time.time > LastShoot + 1.5f){
            moving = true;
            animator.SetBool("walkingGoblin2", false);
            animator.SetBool("throwing", moving);
            StartCoroutine(Shoot());
            LastShoot = Time.time;   
        }
        
    }

    IEnumerator Shoot(){

        Vector3 direction;
        if(transform.localScale.x > 0.0f) direction = Vector3.right;
        else direction = Vector3.left;

        GameObject stone = Instantiate(Stone, transform.position + direction * 2.0f, Quaternion.identity);
        stone.GetComponent<shootStone>().SetDirection(direction);
        stone.GetComponent<shootStone>().dame = 2.0f;
        yield return new WaitForSeconds(5);
    }

    public void takeDameFromPlayer(float Dame){
        Health = Health - Dame;
        if(Health <= 0){
            moving = true;
            animator.SetBool("dying", moving);
            // isDied = true;

            gameObject.SetActive(false);
        }
    }
    void OnTriggerEnter2D(Collider2D other){
        CharacterController_2D player = other.GetComponent<CharacterController_2D>(); 
        if(player!=null) {

            // player.takeDameFromMonster(2.0f);
        }
        if(other.gameObject.CompareTag("Terrain")){
            if(MoveRight){
                MoveRight = false;
            } else{
                MoveRight = true;
            }
        }
        
    }

}
