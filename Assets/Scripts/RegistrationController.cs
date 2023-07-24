using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Text;

public class RegistrationController : MonoBehaviour
{
    public TMP_InputField registerId;
    public TMP_InputField registerPw;
    public Button registerDone;
    public TextMeshProUGUI registerResultText;


    private const string apiUrl = "http://34.64.98.2:3000/api/join"; // Replace with your server URL
    void Start()
    {
        registerDone.onClick.AddListener(OnRegisterButtonClicked);

    }
    public void OnRegisterButtonClicked()
    {
        string id = registerId.text;
        string password = registerPw.text;
        Debug.Log("ddd");
        Debug.Log(id);
        Debug.Log(password);
        PerformRegister(id, password);
       // invoke("delay", 1.0f);
    }
    void PerformRegister(string id, string password)
    {
        
        var url = string.Format("{0}/{1}", "http://34.64.98.2:3000", "api/join");
        var req = new Protocols.Packets.req_Register();
        req.id = id;
        req.pw = password;
        var json = JsonConvert.SerializeObject(req);
        Debug.Log(json);

        StartCoroutine(InsertUser(url, json, (response) =>
        {
            Debug.Log("Response from server: " + response);
            if (response == "success")
            {
                Debug.Log("login ok");
                SceneManager.LoadScene("loginScene");
            }
            else if (response=="exist")
            {
               // ShowToast("dd", 2);
                Debug.Log("exist");
                // SceneManager.LoadScene("mainScene");
                registerResultText.text = "이미 존재하는 아이디입니다";
            }
            else
            {
                Debug.Log("login noooo");
            }
        }));

    }
    

    public static IEnumerator InsertUser(string url, string json, System.Action<string> callback)
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

