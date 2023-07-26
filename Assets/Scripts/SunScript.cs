using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunScript : MonoBehaviour {
    public GameObject explosion;
    public float rotationSpeed = 250f;

    void Update() {
        if (gameObject.activeSelf) {
            transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0);
        }    
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Player") {
            Instantiate(explosion, transform.position, transform.rotation);
            // Destroy(gameObject);
        }
    }
}
