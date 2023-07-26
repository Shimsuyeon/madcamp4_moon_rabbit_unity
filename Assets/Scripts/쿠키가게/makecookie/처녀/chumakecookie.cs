using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Text;
public class chumakecookie : MonoBehaviour
{
    string id;
    public int[] cookies = new int[4]; // Initialize the cookies array with size 4
    public int[] shop = new int[6];
    public Button sendcookie;
    public TextMeshProUGUI nocookie;
    public AudioSource purchaseAudio;

    // Start is called before the first frame update
    void Start()
    {
        cookies[0] = PlayerPrefs.GetInt("cookie1", 0);
        cookies[1] = PlayerPrefs.GetInt("cookie2", 0);
        cookies[2] = PlayerPrefs.GetInt("cookie3", 0);
        cookies[3] = PlayerPrefs.GetInt("cookie4", 0);
        shop[0] = PlayerPrefs.GetInt("shop1", 0);
        shop[1] = PlayerPrefs.GetInt("shop2", 0);
        shop[2] = PlayerPrefs.GetInt("shop3", 0);
        shop[3] = PlayerPrefs.GetInt("shop4", 0);
        shop[4] = PlayerPrefs.GetInt("shop5", 0);
        shop[5] = PlayerPrefs.GetInt("shop6", 0);


        id = PlayerPrefs.GetString("id");
        sendcookie.onClick.AddListener(() => UpdateCookieData(id));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // UpdateCookie
    void UpdateCookieData(string id)
    {
        if (cookies[0] - 3 < 0 || cookies[1] - 2 < 0 || cookies[2] - 3 < 0 || cookies[3] - 3 < 0)
        {
            nocookie.text = "��Ű�� �����մϴ�";
        }
        else
        {
            purchaseAudio.Play();
            var url = string.Format("{0}/{1}", "http://34.64.98.2:3000", "api/cookie/make");
            var req = new Protocols.Packets.req_MakeCookie();
            req.id = id;

            cookies[0] -= 3;
            cookies[1] -= 2;
            cookies[2] -= 3;
            cookies[3] -= 3;
            shop[2] = 1;

            PlayerPrefs.SetInt("cookie1", cookies[0]);
            PlayerPrefs.SetInt("cookie2", cookies[1]);
            PlayerPrefs.SetInt("cookie3", cookies[2]);
            PlayerPrefs.SetInt("cookie4", cookies[3]);
            PlayerPrefs.SetInt("shop3", shop[2]);
            req.cookie = cookies;
            req.shop = shop;
            StartCoroutine(UpdateCookieById(url, JsonConvert.SerializeObject(req), (response) =>
            {
                //var res = JsonConvert.DeserializeObject<Protocols.Packets.res_GetCookie>(response);
                // cookies = res.cookie;

                Debug.LogFormat("Cookie Updated {0}, {1}, {2}, {3}", cookies[0], cookies[1], cookies[2], cookies[3]);
                Debug.LogFormat("shop", shop[0], shop[1], shop[2], shop[3], shop[4], shop[5]);
                SceneManager.LoadScene("shopScene");

            }));
        }
    }

    public static IEnumerator UpdateCookieById(string url, string json, System.Action<string> callback)
    {
        var webRequest = new UnityWebRequest(url, "POST");
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
}
