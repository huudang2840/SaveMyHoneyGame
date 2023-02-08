using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootStone : MonoBehaviour {

    public float speed;
    private Rigidbody2D Rigidbody2D;
    private Vector2 Direction;

    public AudioClip sound;
    public float dame;
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Camera.main.GetComponent<AudioSource>().PlayOneShot(sound);
    }

    private void FixedUpdate() {
        Rigidbody2D.velocity = Direction * speed;
    }
    public void SetDirection(Vector2 direction) {
        Direction = direction;
    }

    public void DestroyBullet() {
        Destroy(gameObject,2);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        CharacterController_2D player = other.GetComponent<CharacterController_2D>(); 
        // MoveGoblin_2 grunt = other.GetComponent<MoveGoblin_2>(); 
        // MoveGoblin_3 grunt7 = other.GetComponent<MoveGoblin_3>(); 
        // MoveGolem_2 grunt2 = other.GetComponent<MoveGolem_2>();
        // MoveMino_2 grunt3 = other.GetComponent<MoveMino_2>();
        // MoveWraith_1 grunt4 = other.GetComponent<MoveWraith_1>();
        // MoveWraith_2 grunt5 = other.GetComponent<MoveWraith_2>();
        // MoveWraith_3 grunt6 = other.GetComponent<MoveWraith_3>();
        // MoveMino_3 grunt8 = other.GetComponent<MoveMino_3>();
        // MoveGolem_3 grunt9 = other.GetComponent<MoveGolem_3>();

        if(player!=null) {
            player.takeDameFromMonster(dame);
            Destroy(gameObject); 
        }
        Destroy(gameObject,2);
    }


    
}