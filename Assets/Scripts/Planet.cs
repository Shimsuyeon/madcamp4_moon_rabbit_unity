using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    float rotationSpeed;
    void Start() {
        rotationSpeed = Random.Range(-20f, 20f);
    }

    void Update() {
        if (gameObject.activeSelf) {
            transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0);
        }
    }
}
