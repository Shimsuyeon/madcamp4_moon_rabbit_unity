using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Rabbit : MonoBehaviour {

    // Cube
    public float x_speed;
    float acc_x;
    float acc_y;
    public Rigidbody rb;

    // UI
    public Slider slider;

    public float fill = 0f;
    public float k = 1f;
    private Gradient grad;
    private GradientColorKey[] colorKeys;
    public Image sliderBack;
    public TMP_Text acc_y_text;
    public TMP_Text x_text;
    public TMP_Text y_text;
    public TMP_Text z_text;

    // Jump
    bool isJumping = false;
    public TMP_Text isJumpingText;

    // Animation
    public Material[] materials;
    public float changeInterval; // 토끼 Sprite 변경 간격 (초)
    private float timer = 0f;
    private int currentIndex = 0;
    public Renderer rabbitRenderer;

    // Planet Spawn & Move
    public GameObject[] planetPrefabs;
    public float backgroundVelocity = 2f;
    public GameObject moon;
    public GameObject[] planets;

    // Star Cookie
    public GameObject[] starCookiePrefabs;
    GameObject[] starCookies;
    int[] starCookieStatus;
    // public GameObject[] starCookieEffects;



    void Start() {
        // Rabbit init
        rabbitRenderer.material = materials[currentIndex];
        isJumping = false;
        isJumpingText.text = "0";

        // Color gradient init
        grad = new Gradient();
        colorKeys = new GradientColorKey[3];
        colorKeys[0].color = Color.green;
        colorKeys[0].time = 0f;
        colorKeys[1].color = Color.yellow;
        colorKeys[1].time = 0.5f;
        colorKeys[2].color = Color.red;
        colorKeys[2].time = 1f;
        grad.colorKeys = colorKeys;

        // Star Cookie init
        starCookies = new GameObject[15];
        starCookieStatus = new int[15];

        // Planet init
        PlanetInit();
    }


    void Update() {
        // Print x, y, z
        x_text.text = "x: " + transform.position.x.ToString();
        y_text.text = "y: " + transform.position.y.ToString();
        z_text.text = "z: " + transform.position.z.ToString();

        // Update Acceleration
        acc_x = Input.acceleration.x;
        acc_y = Input.acceleration.y;

        // Update Cube
        Vector3 dir = Vector3.zero;
        dir.x = acc_x;
        dir.y = acc_y;

        if (dir.sqrMagnitude > 1)
            dir.Normalize();

        dir *= Time.deltaTime;

        rb.velocity = new Vector3(dir.x*x_speed, 0, rb.velocity.z);
        
        // Update Slider
        fill += acc_y * Time.deltaTime * k;
        fill = (fill > 0f) ? (fill < 1f ? fill : 1f) : 0f;
        acc_y_text.text = "acc_y: " + acc_y.ToString();
        slider.value = fill;
        Color sliderColor = grad.Evaluate(fill);
        sliderBack.color = sliderColor;

        // Jump 
        if (acc_y < -0.5f && !isJumping && fill > 0.001f) {
            Jump();
        }

        // Animation
        timer += Time.deltaTime;

        if (timer >= changeInterval) {
            timer = 0f;
            currentIndex = (currentIndex + 1) % materials.Length;
            rabbitRenderer.material = materials[currentIndex];
        }

        // Background Move
        if (isJumping) {
            float delta = Time.deltaTime * backgroundVelocity;
            for (int i = 0; i < 5; i++) {
                planets[i].transform.position += new Vector3(0f, -delta, 0);
            }
            if (moon.transform.position.y >= -5f) {
                moon.transform.position += new Vector3(0f, -delta, 0f);
            }
            PlanetSpawn();
            StarCookieMove();
        }

        // Game Over
        if (transform.position.z > 500f) {
            SceneManager.LoadScene("mainScene");
        }

    }

    // Rabbit 충돌 처리
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("ground")) {
            if(isJumping)
                Debug.Log("Hit ground!!!");

            isJumping = false;
            isJumpingText.text = "0";
        }
    }

    private void Jump() {
        if (!isJumping) {
            float jumpHeight = 6 * fill;
            // float jumpTime = 2 * Mathf.Sqrt(2 * jumpHeight / 9.81f);
            float jumpSpeed = Mathf.Sqrt(2 * 9.81f * jumpHeight);
            
            rb.AddForce(new Vector3(0f, 0f, -jumpSpeed), ForceMode.VelocityChange);
            isJumping = true;
            isJumpingText.text = "1";
            Debug.Log("Jump!!!");
            fill = 0;
        }
    }

    float abs_f(float x) {
        return (x > 0) ? x : -x;
    }


    // Planet Control
    void PlanetInit() { // 처음 5개 만들고
        for (int i = 0; i < 5; i++) {
            int rand = Random.Range(0, 10);

            float x_offset = Random.Range(-2, 2);

            GameObject planetPrefab = Instantiate(planetPrefabs[rand], new Vector3(x_offset, 5f * (i+1), 0f), Quaternion.Euler(90f, 0f, 0f));
            planets[i] = planetPrefab;
            if (i > 0)
                StarCookieSpawn(i, 5f*(i+1));
        }
    }

    void PlanetSpawn() { // 하나가 지나갈 때마다, 그걸 없애고 새로 스폰함
        for (int i = 0; i < 5; i++) {
            if (planets[i].transform.position.y < -5f) {
                Destroy(planets[i]);
                int rand = Random.Range(0, 10);
                float x_offset = Random.Range(-2f, 2f);
                int currLast = (i == 0 ? 4 : i - 1);
                GameObject planetPrefab = Instantiate(planetPrefabs[rand], new Vector3(x_offset, planets[currLast].transform.position.y + 5f, 0f), Quaternion.Euler(90f, 0f, 0f));
                planets[i] = planetPrefab;
                StarCookieSpawn(i, planets[i].transform.position.y);
            }
        }
    }

    // Star Cookie Control
    void StarCookieSpawn(int planetIdx, float baseY) { // baseY에서 -4 ~ -1 만큼 범위에서 0~3 개의 별을 스폰함
        int cnt = Random.Range(0, 4);
        for (int i = 0; i < cnt; i++) {
            int rand = Random.Range(0, 4);
            float x_offset = Random.Range(-2f, 2f);
            float y_offset = Random.Range(-4f, -1f);
            GameObject starCookiePrefab = Instantiate(starCookiePrefabs[rand], new Vector3(x_offset, baseY + y_offset, 0f), Quaternion.Euler(0f, 0f, 0f));
            starCookies[3*planetIdx + i] = starCookiePrefab;
            starCookieStatus[3*planetIdx + i] = rand + 1;
        }
    }

    void StarCookieMove() { // 배경 속도에 따라 이동하며, y좌표가 -5 이하면 삭제함
        for (int i = 0; i < 15; i++) {
            if (starCookies[i] == null) {
                starCookieStatus[i] = 0;
                continue;
            }
            if (starCookieStatus[i] > 0) {
                starCookies[i].transform.position += new Vector3(0f, -Time.deltaTime * backgroundVelocity, 0f);
                if (starCookies[i].transform.position.y < -5f) {
                    Destroy(starCookies[i]);
                    starCookieStatus[i] = 0;
                }
            }
        }
    }

}
