using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explo_Boss : MonoBehaviour
{
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
        
        if(player!=null) {
            player.takeDameFromMonster(dame);
        }
        Destroy(gameObject,2);
    }
}
