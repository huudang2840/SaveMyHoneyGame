using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health, maxHealth = 20f;
    private void Start() {
            health = maxHealth;
    }
    // private void OnCollisionEnter2D(CollisionEnter col) {
    //     if(col.tag == )
    // }
    public void TakeDame(float dame) {
        health -= dame;
        Debug.Log(health);
        if(health <=0 ){
            Destroy(gameObject);

        }
    }
}
