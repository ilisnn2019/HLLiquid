using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour{
    
    public float speed;

    void Update(){
        transform.eulerAngles += Vector3.right * speed;

        if(transform.rotation.x == 90)
        {
            transform.eulerAngles = new Vector3(-180, 0, 0);
        }
    }
}
