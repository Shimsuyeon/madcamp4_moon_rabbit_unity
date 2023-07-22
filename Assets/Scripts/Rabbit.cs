using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    // Animation
    public Material[] materials;
    public float changeInterval; // Material 변경 간격 (초)
    private float timer = 0f;
    private int currentIndex = 0;
    public Renderer rabbitRenderer;

    // Planet Spawn & Move
    public GameObject[] planetPrefabs;
    // public int planetCnt = 5;
    // public int planetIdx = 0;
    public float backgroundVelocity = 2f;
    public GameObject moon;
    public GameObject[] planets;


    void Start() {
        rabbitRenderer.material = materials[currentIndex];

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

        // Planet init
        PlanetInit();
    }


    void Update() {

        // Print x, y, z
        x_text.text = "x: " + transform.position.x.ToString();
        y_text.text = "y: " + transform.position.y.ToString();
        z_text.text = "z: " + transform.position.z.ToString();

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

        if (isJumping && abs_f(transform.position.z) < 0.51) {
            Debug.Log("Hit ground!!!");
            isJumping = false;
        }

        Color sliderColor = grad.Evaluate(fill);
        sliderBack.color = sliderColor;

        // Jump 
        if (acc_y < -0.5f && !isJumping) {
            Jump();
        }

        // Animation
        timer += Time.deltaTime;

        if (timer >= changeInterval)
        {
            timer = 0f;
            currentIndex = (currentIndex + 1) % materials.Length;
            rabbitRenderer.material = materials[currentIndex];
        }

        // Planet Move
        if (isJumping) {
            float delta = Time.deltaTime * backgroundVelocity;
            for (int i = 0; i < 5; i++) {
                planets[i].transform.position += new Vector3(0f, -delta, 0);
            }
            if (moon.transform.position.y >= -5f) {
                moon.transform.position += new Vector3(0f, -delta, 0f);
            }
            PlanetSpawn();
        }

    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("ground")) {
            if(isJumping)
                Debug.Log("Hit ground!!!");

            isJumping = false;
        }
    }

    private void Jump() {
        if (!isJumping) {
            float jumpHeight = 6 * fill;
            // float jumpTime = 2 * Mathf.Sqrt(2 * jumpHeight / 9.81f);
            float jumpSpeed = Mathf.Sqrt(2 * 9.81f * jumpHeight);
            
            rb.AddForce(new Vector3(0f, 0f, -jumpSpeed), ForceMode.VelocityChange);
            isJumping = true;
            Debug.Log("Jump!!!");
        }
    }

    float abs_f(float x) {
        return (x > 0) ? x : -x;
    }


    // Planet Spawn & Move

    // 처음 5개를 만들고
    void PlanetInit() {
        for (int i = 0; i < 5; i++) {
            int rand = Random.Range(0, 10);

            float x_offset = Random.Range(-2, 2);

            GameObject planetPrefab = Instantiate(planetPrefabs[rand], new Vector3(x_offset, 5f * (i+1), 0f), Quaternion.Euler(90f, 0f, 0f));
            planets[i] = planetPrefab;
        }
    }

    // 하나가 지나갈 때마다, 그걸 없애고 새로 스폰함
    void PlanetSpawn() {
        for (int i = 0; i < 5; i++) {
            if (planets[i].transform.position.y < -5f) {
                Destroy(planets[i]);
                int rand = Random.Range(0, 10);
                float x_offset = Random.Range(-2, 2);
                int currLast = (i == 0 ? 4 : i - 1);
                GameObject planetPrefab = Instantiate(planetPrefabs[rand], new Vector3(x_offset, planets[currLast].transform.position.y + 5f, 0f), Quaternion.Euler(90f, 0f, 0f));
                planets[i] = planetPrefab;
            }
        }
    }
}
