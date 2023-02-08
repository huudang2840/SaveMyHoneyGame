using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_1 : MonoBehaviour
{
    [SerializeField] float health, maxHealth = 20f;
    private void Start() {
            health = maxHealth;
    }
    // private void OnCollisionEnter2D(CollisionEnter col) {
    //     if(col.tag == "Weapon")
    // }
    public void TakeDame(float dame) {
        health -= dame;

        if(health <=0 ){
            Destroy(gameObject);
        }
    }
}
