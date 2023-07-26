using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Text;

public class Rabbit : MonoBehaviour {
    string id;
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
    public GameObject inGameUI;
    public TMP_Text amt1;
    public TMP_Text amt2;
    public TMP_Text amt3;
    public TMP_Text amt4;

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
    public CookieInfo cookieInfo;
    public int[] cookies;

    // GameOver UI
    bool isGameOver = false;
    public GameObject gameOverUI;
    public TMP_Text cookie1Text;
    public TMP_Text cookie2Text;
    public TMP_Text cookie3Text;
    public TMP_Text cookie4Text;
    public GameObject effect1;
    public GameObject effect2;
    public GameObject effect3;
    public GameObject effect4;
    public Button backToMoon;

    // Jump Animation
    float jumpFullTime;
    float jumpProgressTime;
    public Material[] jumpMaterials;
    bool isSuperJumping = false;

    // Score Management
    public ScoreInfo scoreInfo;
    public TMP_Text scoreText;
    
    // Tracker
    public GameObject tracker;

    // Black Hole
    public bool isMeetBlackHole = false;
    float shrinkProgress = 1;
    public float rotationSpeed = 250f;

    // Pause UI
    public GameObject pauseUI;
    public Button pauseButton;
    public Button resumeButton;
    public Button goToMainButton;
    bool isPaused = false;


    void Start() {
        id = PlayerPrefs.GetString("id");

        // Rabbit init
        rabbitRenderer.material = materials[currentIndex];
        isJumping = false;
        isJumpingText.text = "0";

        // Color gradient init
        // grad = new Gradient();
        // colorKeys = new GradientColorKey[3];
        // colorKeys[0].color = Color.green;
        // colorKeys[0].time = 0f;
        // colorKeys[1].color = Color.yellow;
        // colorKeys[1].time = 0.5f;
        // colorKeys[2].color = Color.red;
        // colorKeys[2].time = 1f;
        // grad.colorKeys = colorKeys;

        // Star Cookie init
        starCookies = new GameObject[15];
        starCookieStatus = new int[15];

        // Button listener
        backToMoon.onClick.AddListener(BackToMoonButton);

        // Planet init
        PlanetInit();

        // Init Cookies
        cookies = new int[4];        
        GetCookieData(id);

        // Pause UI
        pauseButton.onClick.AddListener(PauseButtonListener);
        resumeButton.onClick.AddListener(ResumeButtonListener);
        goToMainButton.onClick.AddListener(GoToMainButtonListener);

    }


    void Update() {
        if(isPaused) return;
        
        if(isMeetBlackHole) {
            transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
            transform.localScale = new Vector3(1/ shrinkProgress, 1 / shrinkProgress, 1 / shrinkProgress);
            Vector3 oldPosition = transform.position;
            // transform.position = new Vector3(oldPosition.x, oldPosition.y, 0f);
            rb.velocity = new Vector3(0f, 0f, 0f);
            shrinkProgress += 0.5f;
        }

        if(isGameOver) return;
        
        amt1.text = (PlayerPrefs.GetInt("cookie1") + cookieInfo.starCookieEaten[0]).ToString();
        amt2.text = (PlayerPrefs.GetInt("cookie2") + cookieInfo.starCookieEaten[1]).ToString();
        amt3.text = (PlayerPrefs.GetInt("cookie3") + cookieInfo.starCookieEaten[2]).ToString();
        amt4.text = (PlayerPrefs.GetInt("cookie4") + cookieInfo.starCookieEaten[3]).ToString();

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
        if (slider != null) {
            fill += acc_y * Time.deltaTime * k;
            fill = (fill > 0f) ? (fill < 1f ? fill : 1f) : 0f;
            // acc_y_text.text = "acc_y: " + acc_y.ToString();
            slider.value = 1 - fill;
            //Color sliderColor = grad.Evaluate(fill);
            //sliderBack.color = sliderColor;
        }

        // Jump 
        if (acc_y < -0.25f && !isJumping && fill > 0.001f) {
            Jump();
        }

        // None-Jump Animation
        if (!isJumping || jumpFullTime < 1f) {
            timer += Time.deltaTime;

            if (timer >= changeInterval) {
                timer = 0f;
                currentIndex = (currentIndex + 1) % materials.Length;
                rabbitRenderer.material = materials[currentIndex];
            }
        }

        // Background Move
        if (isJumping) {
            float delta = Time.deltaTime * backgroundVelocity;
            for (int i = 0; i < 5; i++) {
                if (planets[i] != null) {
                    planets[i].transform.position += new Vector3(0f, -delta, 0);
                }
            }
            if (moon != null) {
                if (moon.transform.position.y >= -5f) {
                    moon.transform.position += new Vector3(0f, -delta, 0f);
                }
            }
            PlanetSpawn();
            StarCookieMove();
        }

        // Game Over
        if (transform.position.z > 200f || isMeetBlackHole) {
            GameOverUI();
            UpdateCookieData(id);
            isGameOver = true;
        }

        // Jump Animation
        if (isJumping && jumpFullTime > 1f && jumpFullTime < 1.5f) {
            float ratio = jumpProgressTime / jumpFullTime;
            int idx = ((int)(ratio * jumpMaterials.Length)) % jumpMaterials.Length;
            rabbitRenderer.material = jumpMaterials[idx];
            jumpProgressTime += Time.deltaTime;
        } else if (isJumping && jumpFullTime >= 1.5f && jumpFullTime < 2f) {
            float ratio = jumpProgressTime / jumpFullTime * 2;
            int idx = ((int)(ratio * jumpMaterials.Length)) % jumpMaterials.Length;
            rabbitRenderer.material = jumpMaterials[idx];
            jumpProgressTime += Time.deltaTime;
        } else if (isJumping && jumpFullTime >= 2f && jumpFullTime < 3f) {
            float ratio = jumpProgressTime / jumpFullTime * 4;
            int idx = ((int)(ratio * jumpMaterials.Length)) % jumpMaterials.Length;
            rabbitRenderer.material = jumpMaterials[idx];
            jumpProgressTime += Time.deltaTime;
        } else if (isJumping && jumpFullTime >= 3f) {
            float ratio = jumpProgressTime / jumpFullTime * 50;
            int idx = ((int)(ratio * jumpMaterials.Length)) % jumpMaterials.Length;
            rabbitRenderer.material = jumpMaterials[idx];
            jumpProgressTime += Time.deltaTime;
        }

        // Score Update
        scoreText.text = "점수: " + scoreInfo.score.ToString() + "점";

        // Background Velocity Control
        backgroundVelocity = !isSuperJumping ? 2.5f + scoreInfo.score / 500f : 5f;
        acc_y_text.text = backgroundVelocity.ToString();

        // Tracker Control
        tracker.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    // Rabbit 충돌 처리
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("ground")) {
            if(isJumping)
                Debug.Log("Hit ground!!!");
            scoreInfo.score += 5;
            isJumping = false;
            isSuperJumping = false;
            isJumpingText.text = "0";
        } else if (collision.gameObject.CompareTag("sun")) {
            isSuperJumping = true;
            backgroundVelocity = 5f;
            scoreInfo.score += 100;
            int jumpRand = Random.Range(3, 7);
            jumpFullTime = 4.005f /4f * (float)jumpRand;
            float jumpHeight = (jumpFullTime/2f)*(jumpFullTime/2f)*9.81f/2f;
            jumpProgressTime = 0f;
            float jumpSpeed = Mathf.Sqrt(2 * 9.81f * jumpHeight);

            rb.AddForce(new Vector3(0f, 0f, -jumpSpeed), ForceMode.VelocityChange);
            isJumping = true;
            isJumpingText.text = "1";
            Debug.Log("Super Jump!!!");
            fill = 0;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("blackhole")) {
            isMeetBlackHole = true;
        }
    }

    // 점프점프
    private void Jump() {
        if (!isJumping) {
            float jumpHeight = 6 * fill;
            jumpFullTime = 2 * Mathf.Sqrt(2 * jumpHeight / 9.81f);
            jumpProgressTime = 0f;
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
            int rand = Random.Range(0, planetPrefabs.Length);

            float x_offset = Random.Range(-2, 2);

            GameObject planetPrefab = Instantiate(planetPrefabs[rand], new Vector3(x_offset, 5f * (i+1), 0f), Quaternion.Euler(90f, 0f, 0f));
            float scale = Random.Range(1f, 2f);
            planetPrefab.transform.localScale = new Vector3(scale, 0.001f, scale);
            planets[i] = planetPrefab;
            if (i > 0)
                StarCookieSpawn(i, 5f*(i+1));
        }
    }

    void PlanetSpawn() { // 하나가 지나갈 때마다, 그걸 없애고 새로 스폰함
        for (int i = 0; i < 5; i++) {
            if (planets[i] != null && planets[i].transform.position.y < -2f) {
                Destroy(planets[i]);
                int rand = Random.Range(0, planetPrefabs.Length);
                float x_offset = Random.Range(-2f, 2f);
                int currLast = (i == 0 ? 4 : i - 1);
                GameObject planetPrefab = Instantiate(planetPrefabs[rand], new Vector3(x_offset, planets[currLast].transform.position.y + 5f, 0f), Quaternion.Euler(90f, 0f, 0f));
                float scale = Random.Range(1f, 2f);
                planetPrefab.transform.localScale = new Vector3(scale, 0.001f, scale);
                planets[i] = planetPrefab;
                StarCookieSpawn(i, planets[i].transform.position.y);
            }
        }
    }

    // Star Cookie Control
    void StarCookieSpawn(int planetIdx, float baseY) { // baseY에서 -4 ~ -1 만큼 범위에서 0~3 개의 별을 스폰함
        int cnt = Random.Range(0, 4);
        for (int i = 0; i < cnt; i++) {
            int rand = Random.Range(0, starCookiePrefabs.Length);
            float x_offset = Random.Range(-2f, 2f);
            float y_offset = (rand != starCookiePrefabs.Length - 1) ? Random.Range(-4f, -1f) : Random.Range(-3f, -2f);
            GameObject starCookiePrefab = Instantiate(starCookiePrefabs[rand], new Vector3(x_offset, baseY + y_offset, 0.01f*rand), Quaternion.Euler(0f, 0f, 0f));
            starCookies[3*planetIdx + i] = starCookiePrefab;
            if (rand != starCookiePrefabs.Length - 1) {
                starCookies[3*planetIdx + i].GetComponent<StarCookie>().cookieType = rand + 1;
            }
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

    // GameOverUI
    void GameOverUI() {
        effect1.SetActive(true);
        effect2.SetActive(true);
        effect3.SetActive(true);
        effect4.SetActive(true);

        inGameUI.SetActive(false);
        gameOverUI.SetActive(true);
        tracker.SetActive(false);
        pauseUI.SetActive(false);
        pauseButton.gameObject.SetActive(false);

        cookie1Text.text = PlayerPrefs.GetInt("cookie1").ToString() + " + " + cookieInfo.starCookieEaten[0].ToString();
        cookie2Text.text = PlayerPrefs.GetInt("cookie2").ToString() + " + " + cookieInfo.starCookieEaten[1].ToString();
        cookie3Text.text = PlayerPrefs.GetInt("cookie3").ToString() + " + " + cookieInfo.starCookieEaten[2].ToString();
        cookie4Text.text = PlayerPrefs.GetInt("cookie4").ToString() + " + " + cookieInfo.starCookieEaten[3].ToString();

        for (int i = 0; i < 5; i++) {
            if (planets[i] != null)
                Destroy(planets[i]);
        }
        
        for (int i = 0; i < 15; i++) {
            if (starCookies[i] != null)
                Destroy(starCookies[i]);
        }

        Invoke("GameOverUI2", 2f);

    }

    void GameOverUI2() {
        cookie1Text.text = PlayerPrefs.GetInt("cookie1").ToString();
        cookie2Text.text = PlayerPrefs.GetInt("cookie2").ToString();
        cookie3Text.text = PlayerPrefs.GetInt("cookie3").ToString();
        cookie4Text.text = PlayerPrefs.GetInt("cookie4").ToString();
    }

    void BackToMoonButton() {
        SceneManager.LoadScene("mainScene");
    }


    // Network

    // GetCookie
    void GetCookieData(string id) {
        // var url = string.Format("{0}/{1}", "http://34.64.98.2:3000", "api/cookie");
        var url = string.Format("{0}/{1}?id={2}", "http://34.64.98.2:3000", "api/cookie", id);
        // var url = string.Format("{0}/{1}", "http://localhost:3000", "api/cookie");
        var req = new Protocols.Packets.req_GetCookie();
        req.id = id;
        
        StartCoroutine(GetCookieById(url, (response) => {
            var res = JsonConvert.DeserializeObject<Protocols.Packets.res_GetCookie>(response);
            cookies = res.cookie;
            PlayerPrefs.SetInt("cookie1", cookies[0]);
            PlayerPrefs.SetInt("cookie2", cookies[1]);
            PlayerPrefs.SetInt("cookie3", cookies[2]);
            PlayerPrefs.SetInt("cookie4", cookies[3]);

            Debug.LogFormat("쿠키1 : {0}, 쿠키2 : {1}, 쿠키3 : {2}, 쿠키4 : {3}", cookies[0], cookies[1], cookies[2], cookies[3]);
        }));
    }

    public static IEnumerator GetCookieById(string url, System.Action<string> callback) {
        var webRequest = new UnityWebRequest(url, "GET");
        webRequest.downloadHandler = new DownloadHandlerBuffer();
        webRequest.SetRequestHeader("Content-Type", "application/json");

        yield return webRequest.SendWebRequest();
        if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("네트워크 환경이 안좋아서 통신을 할수 없습니다.");
        }
        else
        {
            Debug.LogFormat("{0}\n{1}\n{2}", webRequest.responseCode, webRequest.downloadHandler.data, webRequest.downloadHandler.text);
            callback(webRequest.downloadHandler.text);
        }
    }

    // UpdateCookie
    void UpdateCookieData(string id) {
        var url = string.Format("{0}/{1}", "http://34.64.98.2:3000", "api/cookie/update");
        // var url = string.Format("{0}/{1}?id={2}", "http://34.64.98.2:3000", "api/cookie/update", id);
        // var url = string.Format("{0}/{1}", "http://localhost:3000", "api/cookie/update");
        var req = new Protocols.Packets.req_UpdateCookie();
        req.id = id;
        req.score = scoreInfo.score;
        for (int i = 0; i < 4; i++) {
            cookies[i] += cookieInfo.starCookieEaten[i];
        }
        PlayerPrefs.SetInt("cookie1", cookies[0]);
        PlayerPrefs.SetInt("cookie2", cookies[1]);
        PlayerPrefs.SetInt("cookie3", cookies[2]);
        PlayerPrefs.SetInt("cookie4", cookies[3]);
        req.cookie = cookies;
        
        StartCoroutine(UpdateCookieById(url, JsonConvert.SerializeObject(req), (response) => {
            // var res = JsonConvert.DeserializeObject<Protocols.Packets.res_GetCookie>(response);
            // cookies = res.cookie;
            
            Debug.LogFormat("Cookie Updated {0}, {1}, {2}, {3}", cookies[0], cookies[1], cookies[2], cookies[3]);
        }));
    }

    public static IEnumerator UpdateCookieById(string url, string json, System.Action<string> callback) {
        var webRequest = new UnityWebRequest(url, "POST");
        var bodyRaw = Encoding.UTF8.GetBytes(json);
        webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
        webRequest.downloadHandler = new DownloadHandlerBuffer();
        webRequest.SetRequestHeader("Content-Type", "application/json");

        yield return webRequest.SendWebRequest();
        if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("네트워크 환경이 안좋아서 통신을 할수 없습니다.");
        }
        else
        {
            Debug.LogFormat("{0}\n{1}\n{2}", webRequest.responseCode, webRequest.downloadHandler.data, webRequest.downloadHandler.text);
            callback(webRequest.downloadHandler.text);
        }
    }

    // Pause UI
    void PauseButtonListener() {
        isPaused = true;
        pauseButton.gameObject.SetActive(false);
        pauseUI.SetActive(true);
        inGameUI.SetActive(false);
    }

    void ResumeButtonListener() {
        isPaused = false;
        pauseButton.gameObject.SetActive(true);
        pauseUI.SetActive(false);
        inGameUI.SetActive(true);
    }

    void GoToMainButtonListener() {
        SceneManager.LoadScene("mainScene");
    }
}


