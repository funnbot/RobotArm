using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {
    Vector3 offset;
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Ground")) return;
        offset = other.transform.position - transform.position;
    }

    private void OnCollisionStay(Collision other) {
        if (other.gameObject.CompareTag("Ground")) return;
        GetComponent<Rigidbody>().isKinematic = true;
        transform.position = other.transform.position - offset;
    }

    private void OnCollisionExit(Collision other) {
        if (other.gameObject.CompareTag("Ground")) return;
        GetComponent<Rigidbody>().isKinematic = false;
    }
}