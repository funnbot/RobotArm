using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ArmHinge : MonoBehaviour {
    public string keyRight = "o";
    public string keyLeft = "p";
    public float speed = 1;
    public Vector2 Bounds;
    public Transform pivot;

    Rigidbody rb;

    Vector3 startOffset;
    Quaternion startRot;

    float angle = 0f;

    void Start() {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

        startOffset = rb.transform.position - pivot.position;
        startRot = rb.transform.rotation;
    }

    void FixedUpdate() {
        if (Input.GetKey(keyRight)) {
            angle += speed;
        }
        if (Input.GetKey(keyLeft)) {
            angle += -speed;
        }

        angle = Mathf.Clamp(angle, Bounds.x, Bounds.y);

        Quaternion q = Quaternion.AngleAxis(angle, rb.transform.right);
        rb.MovePosition(q * ((startOffset + pivot.position) - pivot.position) + pivot.position);
        rb.MoveRotation(startRot * q);
    }
}