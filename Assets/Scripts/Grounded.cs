using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounded : MonoBehaviour
{
    GameObject Player;
    void Start()
    {
        Player = gameObject.transform.parent.gameObject;
    }

    
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.collider.tag == "Ground" ){
            Player.GetComponent<CharacterController_2D>().isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if(other.collider.tag == "Ground" ){
            Player.GetComponent<CharacterController_2D>().isGrounded = false;
        }
    }
}
