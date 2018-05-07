using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointController : MonoBehaviour {
    public string keyRight;
    public string keyLeft;

    HingeJoint joint;
    JointMotor motor;

    void Start() {
        joint = GetComponent<HingeJoint>();
        motor = joint.motor;
    }

    void FixedUpdate() {
        motor.targetVelocity = 0;

        if (Input.GetKey(keyRight)) {
            motor.targetVelocity += 40;
        }
        if (Input.GetKey(keyLeft)) {
            motor.targetVelocity -= 40;
        }

        joint.motor = motor;
    }
}