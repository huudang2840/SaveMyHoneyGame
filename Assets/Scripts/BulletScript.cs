using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    public float speed;
    private Rigidbody2D Rigidbody2D;
    private Vector2 Direction;
    public float dameGunn;
    public AudioClip sound;
        // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Rigidbody2D.velocity = Direction * speed;
    }
    
   public void SetDirection(Vector2 direction) {
        Direction = direction;
   }
   public void DestroyBullet() {
        Destroy(gameObject, 1);
   }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag =="Enemy1") {
            MoveGoblin_1 enemy = other.GetComponent<BoxCollider2D>().GetComponent<MoveGoblin_1>(); 
            enemy.takeDameFromPlayer(dameGunn);
            Destroy(gameObject);
        }
        else if(other.tag =="Enemy2") {
            MoveGoblin_2 enemy = other.GetComponent<BoxCollider2D>().GetComponent<MoveGoblin_2>(); 
            enemy.takeDameFromPlayer(dameGunn);
            Destroy(gameObject);
        }
        else if(other.tag =="Enemy3") {
            MoveGoblin_3 enemy = other.GetComponent<BoxCollider2D>().GetComponent<MoveGoblin_3>(); 
            enemy.takeDameFromPlayer(dameGunn);
            Destroy(gameObject);
        }
        else if(other.tag =="Enemy4") {
            MoveWraith_1 enemy = other.GetComponent<BoxCollider2D>().GetComponent<MoveWraith_1>(); 
            enemy.takeDameFromPlayer(dameGunn);
            Destroy(gameObject);
        }
         else if(other.tag =="Enemy5") {
            MoveGolem_1 enemy = other.GetComponent<BoxCollider2D>().GetComponent<MoveGolem_1>(); 
            enemy.takeDameFromPlayer(dameGunn);
            Destroy(gameObject);
        }
        else if(other.tag =="Enemy6") {
            MoveGolem_2 enemy = other.GetComponent<BoxCollider2D>().GetComponent<MoveGolem_2>(); 
            enemy.takeDameFromPlayer(dameGunn);
            Destroy(gameObject);
        }
        else if(other.tag =="Enemy7") {
            MoveGolem_3 enemy = other.GetComponent<BoxCollider2D>().GetComponent<MoveGolem_3>(); 
            enemy.takeDameFromPlayer(dameGunn);
            Destroy(gameObject);
        }
          else if(other.tag =="Enemy8") {
            MoveWraith_2 enemy = other.GetComponent<BoxCollider2D>().GetComponent<MoveWraith_2>(); 
            enemy.takeDameFromPlayer(dameGunn);
            Destroy(gameObject);
        }
        else if(other.tag =="Enemy9") {
            MoveMino_1 enemy = other.GetComponent<BoxCollider2D>().GetComponent<MoveMino_1>(); 
            enemy.takeDameFromPlayer(dameGunn);
            Destroy(gameObject);
        }
        else if(other.tag =="Enemy10") {
            MoveMino_2 enemy = other.GetComponent<BoxCollider2D>().GetComponent<MoveMino_2>(); 
            enemy.takeDameFromPlayer(dameGunn);
            Destroy(gameObject);
        }
        else if(other.tag =="Enemy11") {
            MoveMino_3 enemy = other.GetComponent<BoxCollider2D>().GetComponent<MoveMino_3>(); 
            enemy.takeDameFromPlayer(dameGunn);
            Destroy(gameObject);
        }
        else if(other.tag =="Enemy12") {
            MoveWraith_3 enemy = other.GetComponent<BoxCollider2D>().GetComponent<MoveWraith_3>(); 
            enemy.takeDameFromPlayer(dameGunn);
            Destroy(gameObject);
        }
        else if(other.tag =="Boss1") {
            ControBoss1 enemy = other.GetComponent<BoxCollider2D>().GetComponent<ControBoss1>(); 
            enemy.takeDameFromPlayer(dameGunn);
            Destroy(gameObject);
        }
        else if(other.tag =="Boss2") {
            ControBoss2 enemy = other.GetComponent<BoxCollider2D>().GetComponent<ControBoss2>(); 
            enemy.takeDameFromPlayer(dameGunn);
            Destroy(gameObject);
        }
        else if(other.tag =="Boss3") {
            ControBoss3 enemy = other.GetComponent<BoxCollider2D>().GetComponent<ControBoss3>(); 
         
            enemy.takeDameFromPlayer(dameGunn);

            Destroy(gameObject);
        }
   }

    
}
