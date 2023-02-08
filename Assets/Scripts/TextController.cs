using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    public CharacterController_2D player;
    public Text QtyHealth;
    public Text QtyProtected;
    public Text QtyBullet;
    public Text QtyBuffDame;

    void Start()
    {
        QtyHealth.text = "X0";
        QtyProtected.text = "X0";
        QtyBullet.text = "X0";
        QtyBuffDame.text = "X0";
    }

    // Update is called once per frame
    void Update()
    {
        QtyHealth.text = "X"+player.qtyItemHealth.ToString();
        QtyProtected.text = "X"+player.qtyItemProtected.ToString();
        QtyBullet.text = "X"+player.totalBullet.ToString();
        QtyBuffDame.text = "X"+player.qtyItemBuffDame.ToString();
    }
}
