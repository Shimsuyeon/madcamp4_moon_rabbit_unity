using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunScript : MonoBehaviour {
    public GameObject explosion;

    void Start() {
        
    }

    void Update() {
        
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Player") {
            Instantiate(explosion, transform.position, transform.rotation);
            // Destroy(gameObject);
        }
    }
}
