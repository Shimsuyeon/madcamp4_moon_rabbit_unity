using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Text;
public class sasacellcookie : MonoBehaviour
{
    string id;
    public int[] shop = new int[6];
    public int money;
    public Button celllcookie;
    public TextMeshProUGUI moneyy;
    public AudioSource cellAudio;
    // Start is called before the first frame update
    void Start()
    {
        shop[0] = PlayerPrefs.GetInt("shop1", 0);
        shop[1] = PlayerPrefs.GetInt("shop2", 0);
        shop[2] = PlayerPrefs.GetInt("shop3", 0);
        shop[3] = PlayerPrefs.GetInt("shop4", 0);
        shop[4] = PlayerPrefs.GetInt("shop5", 0);
        shop[5] = PlayerPrefs.GetInt("shop6", 0);
        money = PlayerPrefs.GetInt("money", 0);
        moneyy.text = PlayerPrefs.GetInt("money", 0).ToString();
        id = PlayerPrefs.GetString("id");
        celllcookie.onClick.AddListener(() => UpdatesaCellData(id));
        DontDestroyOnLoad(cellAudio);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void UpdatesaCellData(string id)
    {
        cellAudio.Play();
        var url = string.Format("{0}/{1}", "http://34.64.98.2:3000", "api/cookie/cell");
        var req = new Protocols.Packets.req_cellCookies();
        req.id = id;

        shop[3] = 0;
        money += 20;
        PlayerPrefs.SetInt("shop4", shop[3]);
        PlayerPrefs.SetInt("money", money);
        moneyy.text = money.ToString();
        req.shop = shop;
        req.money = money;
        StartCoroutine(UpdatesaCookieById(url, JsonConvert.SerializeObject(req), (response) => {
            //var res = JsonConvert.DeserializeObject<Protocols.Packets.res_GetCookie>(response);
            // cookies = res.cookie;

           SceneManager.LoadScene("shopScene");

        }));
    }
    public static IEnumerator UpdatesaCookieById(string url, string json, System.Action<string> callback)
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
