using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Text;

public class LoginController : MonoBehaviour
{
    public TMP_InputField LoginId;
    public TMP_InputField LoginPw;
    public Button LoginDone;
    public TextMeshProUGUI loginResultText;
    public string status;
    public int[] cookies;
    public int[] shop;

    // Start is called before the first frame update
    void Start()
    {
        LoginDone.onClick.AddListener(OnloginButtonClicked);
        cookies = new int[4];
    }
    public void OnloginButtonClicked()
    {
        string id = LoginId.text;
        string password = LoginPw.text;
        PerformLogin(id, password);
    }
    // Update is called once per frame
    void PerformLogin(string id, string password)
    {
        var url = string.Format("{0}/{1}", "http://34.64.98.2:3000", "api/login");
        // var url = string.Format("{0}/{1}", "http://localhost:3000", "api/login");
        var req = new Protocols.Packets.req_Login();


        req.id = id;
        req.pw = password;

        var json = JsonConvert.SerializeObject(req);
     
        StartCoroutine(LoginUser(url, json, (response) =>
        {
            var res = JsonConvert.DeserializeObject<Protocols.Packets.req_Login>(response);
            status = res.status;

            if (status == "success")
            {
                PlayerPrefs.SetString("id", id);
                cookies = res.cookie;
                shop = res.shop;
                PlayerPrefs.SetInt("score", res.score);
                Debug.Log(cookies[0]);
                Debug.Log(shop[0]);
                GetCookieData(id);
                SceneManager.LoadScene("mainScene");
            }
            else if (response == "none")
            {
                loginResultText.text = "���̵� ��й�ȣ�� ��ġ���� �ʽ��ϴ�.";
            }
        }));
    }

    void GetCookieData(string id)
    {
        var url = string.Format("{0}/{1}", "http://34.64.98.2:3000", "api/cookie");
        // var url = string.Format("{0}/{1}", "http://localhost:3000", "api/cookie");
        var req = new Protocols.Packets.req_Login();
        req.id = id;

        StartCoroutine(GetCookieById(url, JsonConvert.SerializeObject(req), (response) => {
            var res = JsonConvert.DeserializeObject<Protocols.Packets.res_GetCookie>(response);
            cookies = res.cookie;
            PlayerPrefs.SetInt("cookie1", cookies[0]);
            PlayerPrefs.SetInt("cookie2", cookies[1]);
            PlayerPrefs.SetInt("cookie3", cookies[2]);
            PlayerPrefs.SetInt("cookie4", cookies[3]);

            Debug.LogFormat("쿠키1 : {0}, 쿠키2 : {1}, 쿠키3 : {2}, 쿠키4 : {3}", cookies[0], cookies[1], cookies[2], cookies[3]);
        }));


    }

    public static IEnumerator LoginUser(string url, string json, System.Action<string> callback)
    {
        var webRequest = new UnityWebRequest(url, "GET");
        var bodyRaw = Encoding.UTF8.GetBytes(json);
        webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
        webRequest.downloadHandler = new DownloadHandlerBuffer();
        webRequest.SetRequestHeader("Content-Type", "application/json");
        yield return webRequest.SendWebRequest();
        if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("��Ʈ��ũ ȯ���� �����Ƽ� ����� �Ҽ� �����ϴ�.");
        }
        else
        {
            Debug.LogFormat("{0}\n{1}\n{2}", webRequest.responseCode, webRequest.downloadHandler.data, webRequest.downloadHandler.text);
            callback(webRequest.downloadHandler.text);
        }
    }

    public static IEnumerator GetCookieById(string url, string json, System.Action<string> callback)
    {
        var webRequest = new UnityWebRequest(url, "GET");
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

    void delay()
    {

    }
}