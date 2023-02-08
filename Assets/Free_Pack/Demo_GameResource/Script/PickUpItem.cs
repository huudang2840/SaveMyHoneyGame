
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    
    // public CharacterController_2D player;

    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") {
            Destroy(gameObject);
            // if(player.qtyItemBuffDame < 5){
            //     Destroy(gameObject);
            // }
            // else if(player.qtyItemBuffDame < 5){
            //     Destroy(gameObject);
            // }
            // else if(player.qtyItemBuffDame < 5){
            //     Destroy(gameObject);
            // }
            // else if(player.qtyItemBuffDame < 5){
            //     Destroy(gameObject);
            // }
        }
    }
    
}
