using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Utilities;
[System.Serializable]

public class GameData {
    public float health;
    // public Vector3 playerPosition;
    public int totalBullet;
    public int qtyItemHealth;
    public int qtyItemProtected;
    public int qtyItemBuffDame;
    public int indexScene;
    // public SerializableDictionary<string, bool> monsterDied;

    public GameData(){
        this.health = 30;
        // this.playerPosition = Vector3.zero;
        this.totalBullet = 10;
        this.qtyItemHealth = 2;
        this.qtyItemProtected = 2;
        this.qtyItemBuffDame = 2;
        this.indexScene=0;
        // monsterDied = new SerializableDictionary<string, bool>();
    }
}
