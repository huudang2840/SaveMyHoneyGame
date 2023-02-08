using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    // [SerializeField] private string id;

    // bool isCheckDie;
    // private SpriteRenderer visual;

    // public SpriteRenderer test;


    // private void Start() {
    //     // if(gameObject.activeSelf == false) {
    //     //     isCheckDie = true;
    //     // }
    //     // else {
    //     //     isCheckDie = false;
    //     // }
    // }
//    private void FixedUpdate() {
//         if(gameObject.activeSelf == true) {
//             isCheckDie = false;
//         }
//         else if(gameObject.activeSelf == false){
//             isCheckDie = true;
//         }
//     }
    // [ContextMenu("Genarate ID")]

    // private void GenerateGuid() {
    //     id = System.Guid.NewGuid().ToString();
    // }

    // public void LoadData(GameData data) {
    //     data.monsterDied.TryGetValue(id, out isCheckDie);
    //     if(isCheckDie) {
    //         visual.gameObject.SetActive(false);
    //     }
    // }

    // public void SaveData(ref GameData data) {
    //     if(data.monsterDied.ContainsKey(id)) {
    //         data.monsterDied.Remove(id);
    //     }
    //     data.monsterDied.Add(id, isCheckDie);
    // }

}
