using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarCookie : MonoBehaviour {
    public GameObject particlePrefab;
    public int cookieType; // 1, 2, 3, 4
    public CookieInfo cookieInfo;

    void Start() {
        // Debug.Log("Star Cookie Hit");
    }

    void Update() {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            Debug.Log("Star Cookie Hit");
            // 파티클 스폰을 위한 코루틴 시작
            StartCoroutine(SpawnParticleCoroutine());
            cookieInfo.starCookieEaten[cookieType - 1]++;
            Destroy(gameObject);
        }
    }

    private IEnumerator SpawnParticleCoroutine() {
        // 파티클 스폰
        if (particlePrefab != null) {
            Instantiate(particlePrefab, transform.position, Quaternion.identity);
        }
        yield return new WaitForSeconds(1f);
    }

}
