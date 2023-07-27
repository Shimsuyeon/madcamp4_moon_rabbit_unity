using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Text;

public class payshop : MonoBehaviour
{
    public GameObject cupcakeObject;
    public GameObject teapotObject;
    public GameObject breadObject;

    public Button cupcakeButton;
    public Button cupcellButton;

    public Button breadButton;
    public Button breadcellButton;
   
    public Button teapotButton;
    public Button teacellButton;

    public TextMeshProUGUI moneyy;

    public TextMeshProUGUI alert;

    public int money;
    public int[] cafe = new int[3];


    public AudioSource purchase;
    public AudioSource cell;

    string id;

    // Start is called before the first frame update
    void Start()
    {
        cafe[0] = PlayerPrefs.GetInt("cafe1", 0);
        cafe[1] = PlayerPrefs.GetInt("cafe2", 0);
        cafe[2] = PlayerPrefs.GetInt("cafe3", 0);
        money = PlayerPrefs.GetInt("money", 0);
        id = PlayerPrefs.GetString("id");
        alert.text = "";
        if (cafe[0] == 0)
        {
           
            cupcakeObject.gameObject.SetActive(false);
            cupcakeButton.gameObject.SetActive(true);
            cupcellButton.gameObject.SetActive(false);
        } else if (cafe[0] == 1)
        {
            cupcakeObject.gameObject.SetActive(true);
            cupcakeButton.gameObject.SetActive(false);
            cupcellButton.gameObject.SetActive(true);
        }

        if (cafe[1] == 0)
        {
            teapotObject.gameObject.SetActive(false);
            teapotButton.gameObject.SetActive(true);
            teacellButton.gameObject.SetActive(false);
        }
        else if (cafe[1] == 1)
        {
            teapotObject.gameObject.SetActive(true);
            teapotButton.gameObject.SetActive(false);
            teacellButton.gameObject.SetActive(true);
        }

        if (cafe[2] == 0)
        {
            breadObject.gameObject.SetActive(false);
            breadButton.gameObject.SetActive(true);
            breadcellButton.gameObject.SetActive(false);
        }
        else if (cafe[2] == 1)
        {
            breadObject.gameObject.SetActive(true);
            breadButton.gameObject.SetActive(false);
            breadcellButton.gameObject.SetActive(true);
        }
        cupcakeButton.onClick.AddListener(() => UpdateCupcake(id));
        cupcellButton.onClick.AddListener(() => UpdateCellcake(id));

        teapotButton.onClick.AddListener(() => UpdateTea(id));
        teacellButton.onClick.AddListener(() => UpdateCellTea(id));

        breadButton.onClick.AddListener(() => UpdateBread(id));
        breadcellButton.onClick.AddListener(() => UpdateCellBread(id));



    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void UpdateCellcake(string id)
    {
        if (cafe[0] == 1) //팔기
        {

            var url = string.Format("{0}/{1}", "http://34.64.98.2:3000", "api/cafe/cell");
            var req = new Protocols.Packets.req_buyCafe();
            req.id = id;

            cell.Play();
            cafe[0] = 0;
            money += 25;
            PlayerPrefs.SetInt("cafe1", cafe[0]);
            PlayerPrefs.SetInt("money", money);

            moneyy.text = money.ToString();
            req.cafe = cafe;
            req.money = money;
            StartCoroutine(UpdateCafeById(url, JsonConvert.SerializeObject(req), (response) => {

                cupcakeObject.gameObject.SetActive(false);
                cupcakeButton.gameObject.SetActive(true);
                cupcellButton.gameObject.SetActive(false);
                alert.text = "판매했습니다.";
            }));

        }
    }
    void UpdateCupcake(string id)
    {
        //컵케잌 사기
        if (cafe[0] == 0 && money>=50)
        {
            var url = string.Format("{0}/{1}", "http://34.64.98.2:3000", "api/cafe/buy");
            var req = new Protocols.Packets.req_buyCafe();
            req.id = id;

            purchase.Play();
            cafe[0] = 1;
            money -= 50;
            PlayerPrefs.SetInt("cafe1", cafe[0]);
            PlayerPrefs.SetInt("money", money);
            moneyy.text = money.ToString();
            req.cafe = cafe;
            req.money = money;
            Debug.Log("cafe");
            Debug.Log(cafe);
            Debug.Log("money");
            
            
            Debug.Log(money);
            StartCoroutine(UpdateCafeById(url, JsonConvert.SerializeObject(req), (response) => {

                cupcakeObject.gameObject.SetActive(true);
                cupcakeButton.gameObject.SetActive(false);
                cupcellButton.gameObject.SetActive(true);
                alert.text = "구매했습니다.";
            }));
        }
        else if(cafe[0]==0 && money<50)
        {
            alert.text = "별이 부족해서 구매할 수 없습니다.";
        }
        //컵케잌 팔기
    }




    void UpdateCellTea(string id)
    {
        if (cafe[1] == 1) //차팔아
        {

            var url = string.Format("{0}/{1}", "http://34.64.98.2:3000", "api/cafe/cell");
            var req = new Protocols.Packets.req_buyCafe();
            req.id = id;
            cell.Play();
            cafe[1] = 0;
            money += 35;
            PlayerPrefs.SetInt("cafe2", cafe[1]);
            PlayerPrefs.SetInt("money", money);

            moneyy.text = money.ToString();
            req.cafe = cafe;
            req.money = money;
            StartCoroutine(UpdateCafeById(url, JsonConvert.SerializeObject(req), (response) => {

                teapotObject.gameObject.SetActive(false);
                teapotButton.gameObject.SetActive(true);
                teacellButton.gameObject.SetActive(false);
                alert.text = "판매했습니다.";
            }));

        }
    }
    void UpdateTea(string id)
    {
        //차 사와
        if (cafe[1] == 0 && money >= 70)
        {
            var url = string.Format("{0}/{1}", "http://34.64.98.2:3000", "api/cafe/buy");
            var req = new Protocols.Packets.req_buyCafe();
            req.id = id;
            purchase.Play();
            cafe[1] = 1;
            money -= 70;
            PlayerPrefs.SetInt("cafe2", cafe[1]);
            PlayerPrefs.SetInt("money", money);
            moneyy.text = money.ToString();
            req.cafe = cafe;
            req.money = money;
            Debug.Log("cafe");
            Debug.Log(cafe);
            Debug.Log("money");


            Debug.Log(money);
            StartCoroutine(UpdateCafeById(url, JsonConvert.SerializeObject(req), (response) => {

                teapotObject.gameObject.SetActive(true);
                teapotButton.gameObject.SetActive(false);
                teacellButton.gameObject.SetActive(true);
                alert.text = "구매했습니다.";
            }));
        }
        else if (cafe[1] == 0 && money < 70)
        {
            alert.text = "별이 부족해서 구매할 수 없습니다.";
        }

    }

    void UpdateCellBread(string id)
    {
        if (cafe[2] == 1) //빵팔아
        {

            var url = string.Format("{0}/{1}", "http://34.64.98.2:3000", "api/cafe/cell");
            var req = new Protocols.Packets.req_buyCafe();
            req.id = id;
            cell.Play();
            cafe[2] = 0;
            money += 50;
            PlayerPrefs.SetInt("cafe3", cafe[2]);
            PlayerPrefs.SetInt("money", money);

            moneyy.text = money.ToString();
            req.cafe = cafe;
            req.money = money;
            StartCoroutine(UpdateCafeById(url, JsonConvert.SerializeObject(req), (response) => {

                breadObject.gameObject.SetActive(false);
                breadButton.gameObject.SetActive(true);
                breadcellButton.gameObject.SetActive(false);
                alert.text = "판매했습니다.";
            }));

        }
    }

    void UpdateBread(string id)
    {
        //빵 사와
        if (cafe[2] == 0 && money >= 100)
        {
            var url = string.Format("{0}/{1}", "http://34.64.98.2:3000", "api/cafe/buy");
            var req = new Protocols.Packets.req_buyCafe();
            req.id = id;
            purchase.Play();
            cafe[2] = 1;
            money -= 100;
            PlayerPrefs.SetInt("cafe3", cafe[2]);
            PlayerPrefs.SetInt("money", money);
            moneyy.text = money.ToString();
            req.cafe = cafe;
            req.money = money;
            Debug.Log("cafe");
            Debug.Log(cafe);
            Debug.Log("money");


            Debug.Log(money);
            StartCoroutine(UpdateCafeById(url, JsonConvert.SerializeObject(req), (response) => {

                breadObject.gameObject.SetActive(true);
                breadButton.gameObject.SetActive(false);
                breadcellButton.gameObject.SetActive(true);
                alert.text = "구매했습니다.";
            }));
        }
        else if (cafe[2] == 0 && money < 100)
        {
            alert.text = "별이 부족해서 구매할 수 없습니다.";
        }

    }

    

    public static IEnumerator UpdateCafeById(string url, string json, System.Action<string> callback)
    {
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

}
