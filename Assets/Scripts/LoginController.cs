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
    // Start is called before the first frame update
    void Start()
    {
        LoginDone.onClick.AddListener(OnloginButtonClicked);
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
        var req = new Protocols.Packets.req_Login();
        req.id = id;
        req.pw = password;
        var json = JsonConvert.SerializeObject(req);
        StartCoroutine(LoginUser(url, json, (response) =>
        {
            if (response == "success")
            {
                SceneManager.LoadScene("mainScene");
            }
            else if (response == "none")
            {
                loginResultText.text = "���̵� ��й�ȣ�� ��ġ���� �ʽ��ϴ�.";
            }
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
    }

