using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointPassScript : MonoBehaviour
{
    public GameObject boss;
    public GameObject pointPass;

    // Update is called once per frame
    void Update()
    {
       if(boss == null) {
            pointPass.SetActive(true);
       }else {
            pointPass.SetActive(false);
       }
    }
}
