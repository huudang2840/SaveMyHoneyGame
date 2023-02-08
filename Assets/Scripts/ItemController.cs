using UnityEngine;
using System;
using System.Collections;

public class ItemController : MonoBehaviour
{
    float originalY;
    private float floatStrength = 0.2f; // You can change this in the Unity Editor to                                // change the range of y positions that are possibl
    void Start()
    {
        this.originalY = this.transform.position.y;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x,
            originalY + ((float)Math.Sin(Time.time) * floatStrength),
            transform.position.z);
    }
}