using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Text;
using System.Security.Cryptography;

public class LoginController : MonoBehaviour
{
    public TMP_InputField LoginId;
    public TMP_InputField LoginPw;
    public Button LoginDone;
    public TextMeshProUGUI loginResultText;
    public string status;
    public int[] shop;
    public int[] cookies;
    public int money;

    // Start is called before the first frame update
    void Start()
    {
        LoginDone.onClick.AddListener(OnloginButtonClicked);
        cookies = new int[4];
    }
    public void OnloginButtonClicked()
    {
        string id = LoginId.text;
        string password = GetSHA256Hash(LoginPw.text);
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
                money = res.money;

                string strArr2 = "";
                PlayerPrefs.SetInt("cookie1", cookies[0]);
                PlayerPrefs.SetInt("cookie2", cookies[1]);
                PlayerPrefs.SetInt("cookie3", cookies[2]);
                PlayerPrefs.SetInt("cookie4", cookies[3]);
                PlayerPrefs.SetInt("shop1", shop[0]);
                PlayerPrefs.SetInt("shop2", shop[1]);
                PlayerPrefs.SetInt("shop3", shop[2]);
                PlayerPrefs.SetInt("shop4", shop[3]);
                PlayerPrefs.SetInt("shop5", shop[4]);
                PlayerPrefs.SetInt("shop6", shop[5]);
                PlayerPrefs.SetInt("money", money);

                //Debug.Log(strArr2);
                Debug.Log(cookies);
                //PlayerPrefs.SetString("shop", strArr2);

                Debug.Log(shop[0]);
                GetCookieData(id);
                SceneManager.LoadScene("mainScene");
            }
            else if (response == "none")
            {
                loginResultText.text = "일치하는 아이디, 비밀번호가 없습니다.";
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

    public static string GetSHA256Hash(string input)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            // 입력 문자열을 바이트 배열로 변환
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // 바이트 배열을 16진수 문자열로 변환하여 반환
            StringBuilder builder = new StringBuilder();
            foreach (byte b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }
    }
}