using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ArmHinge : MonoBehaviour {
    public string keyRight = "o";
    public string keyLeft = "p";

    Rigidbody rb;
    Transform origin;

    float angle = 0f;

    void Start() {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        origin = transform.GetChild(0);
    }

    void Update() {
        if (Input.GetKey(keyRight)) {
            angle = 1;
        }
        if (Input.GetKey(keyLeft)) {
            angle = -1;
        }

        Quaternion q = Quaternion.AngleAxis(angle, rb.transform.right);
        rb.MovePosition(q * (rb.transform.position - origin.position) + origin.position);
        rb.MoveRotation(rb.transform.rotation * q);

        angle = 0;
    }
}