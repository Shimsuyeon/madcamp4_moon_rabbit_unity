using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleScript : MonoBehaviour {
    public float rotationSpeed = 250f;

    void Update() {
        if (gameObject.activeSelf) {
            transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
        }    
    }
}
