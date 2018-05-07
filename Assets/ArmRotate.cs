using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmRotate : MonoBehaviour {
    public string keyRight;
    public string keyLeft;

    Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }  

    void Update() {
        if (Input.GetKey(keyRight)) {
            
        }
        if (Input.GetKey(keyLeft)) {

        }
    }
} 
