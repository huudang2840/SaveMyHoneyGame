using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCript : MonoBehaviour
{
    public GameObject BulletPrefab;
    public float speed;
    public float jumpForce;
    private Rigidbody2D Rigidbody2D;
    private Animator Animator;
    private float Horizontal;
    public bool isGround;
    private float lastShoot;
    //public AudioClip audio;
    private float healthPlayer = 10.0f;
    
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        // source = GetComponent<AudioSource>().PlayOneShot(audio);
    }

    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        if(Horizontal < 0.0f) {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }else if(Horizontal > 0.0f){
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        }
        // Animator.SetBool("isRunning", Horizontal != 0.0f);

        //Debug.DrawRay(transform.position, Vector3.down * 0.1f, Color.red);


        if(Physics2D.Raycast(transform.position, Vector3.down, 0.1f)){
             isGround = true;
        }else {
            isGround = false;
        }



        if(Input.GetKeyDown(KeyCode.W) && isGround){
            
            JumpAction();
        }
         if(Input.GetKeyDown(KeyCode.Space) && Time.time > lastShoot + 0.25f){
            ShootActtion();
            lastShoot = Time.time;
        }
    }

    private void JumpAction() {
        // if(source.isPlaying ){
        //      source.Stop () ;
        // } else {
        //     source.PlayDelayed (1f);
        // }
        Rigidbody2D.AddForce(Vector2.up * jumpForce);
        
        // audio.Play();
    }
    private void ShootActtion() {
        Vector3 direction;
        if(transform.localScale.x == 1.0f) direction = Vector2.right;
        else direction = Vector2.left;

        GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);
        bullet.GetComponent<shootStone>().SetDirection(direction);
    }
    private void FixedUpdate() {
        Rigidbody2D.velocity = new Vector2(Horizontal, Rigidbody2D.velocity.y);
    }


    public void Hit() {
        healthPlayer = healthPlayer - 2.0f;
        if(healthPlayer ==0 ) Destroy(gameObject);
    }
    

}