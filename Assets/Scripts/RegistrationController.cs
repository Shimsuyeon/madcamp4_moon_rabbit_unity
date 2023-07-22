/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Text;
using UnityEngine.Networking;
using Newtonsoft.Json;
using TMPro;

#region 공통구조체
public class packet
{
    public int cmd;
}
#endregion
//회원가입 시 응답 객체
public class res_join : packet
{
    public int errorno;
}

//회원가입시 전송하는 객체
public class req_join : packet
{
    public string uid;
    public string nickname;
}

public class RegistrationController : MonoBehaviour
{
    public GameObject joinGo;
    //public Text txtUID;
   // public Text txtNickName;
    public Text txtSuccessLogin;
    public Button btn;
    public Button btnSubmit; //서버전송
    public InputField inputField;
    public TextMeshProUGUI loginId;
    public TextMeshProUGUI passwd;


    private string uid;



    void Start()
    {
        //서버에 요청 (device의 uid 등록여부 확인)

        this.Init();



        //버튼 이벤트 등록
        this.btn.onClick.AddListener(() =>
        {
            if (string.IsNullOrEmpty(this.inputField.text))
            {
                Debug.Log("닉네임을 입력해주세요.");
            }
            else
            {
                this.txtNickName.text = this.inputField.text;

                this.inputField.gameObject.SetActive(false);
                this.btn.gameObject.SetActive(false);
                this.txtNickName.gameObject.SetActive(true);
                this.btnSubmit.gameObject.SetActive(true);
            }
        });

        this.btnSubmit.onClick.AddListener(() =>
        {
            var reqJoin = new req_join();
            reqJoin.cmd = 1000;
            //reqJoin.uid = this.txtUID.text;
            //reqJoin.nickname = this.txtNickName.text;

            var json = JsonConvert.SerializeObject(reqJoin);
            Debug.Log(json);

            StartCoroutine(this.Post("api/join", json, (result) =>
            {
                //응답 
                var responseResult = JsonConvert.DeserializeObject<res_join>(result);
                Debug.Log(responseResult);
                if (responseResult.cmd == 200)
                {
                    this.joinGo.SetActive(false);
                }
                else
                {
                    if (responseResult.errorno == 1062)
                    {
                        Debug.Log("이미 회원등록되었습니다.");
                    }
                }
            }));

        });
    }

    private void Init()
    {
        this.uid = SystemInfo.deviceUniqueIdentifier;
        this.txtUID.text = this.uid;
        this.btnSubmit.gameObject.SetActive(false);
        this.txtNickName.gameObject.SetActive(false);

    }

   // private const string apiUrl = "http://34.64.73.79:3000/api/join"; // Replace with your server URL

    private IEnumerator Post(string uri, string data, Action<string> onResponse)
    {
        var url = string.Format("{0}/{1}", "http://34.64.73.79:3000", "/api/join");
        Debug.Log(url);
        Debug.Log(data);

        var req = new UnityWebRequest(url, "POST");
        byte[] body = Encoding.UTF8.GetBytes(data);
        Debug.Log(body);

        req.uploadHandler = new UploadHandlerRaw(body);
        req.downloadHandler = new DownloadHandlerBuffer();
        req.SetRequestHeader("Content-Type", "application/json");

        yield return req.SendWebRequest();

        onResponse(req.downloadHandler.text);
    }
}
*/

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
   
    //public InputField usernameInputField;
    //public InputField passwordInputField;

    private const string apiUrl = "http://34.64.98.2:3000/api/join"; // Replace with your server URL
    void Start()
    {
        registerDone.onClick.AddListener(OnRegisterButtonClicked);

    }
    public void OnRegisterButtonClicked()
    {
        string id = registerId.text;
       // id = id.Substring(0, id.Length - 1);
        string password = registerPw.text;
    //    password = password.Substring(0, password.Length - 1);
        Debug.Log("ddd");
        Debug.Log(id);
        Debug.Log(password);
        PerformRegister(id, password);
       // invoke("delay", 1.0f);
    }
    void PerformRegister(string id, string password)
    {
        
        var url = string.Format("{0}/{1}", "http://34.64.98.2:3000", "api/join");
       // var url = "http://34.64.98.2:3000/api/join";
        var req = new Protocols.Packets.req_Register();
        req.id = id;
        req.pw = password;
        var json = JsonConvert.SerializeObject(req);
        Debug.Log(json);

        StartCoroutine(InsertUser(url, json, (response) =>
        {
            // This is the callback function that will be called after InsertUser finishes
            // You can handle the response from the server here
            Debug.Log("Response from server: " + response);

            // You can also perform other actions based on the server response
            if (response == "success")
            {
                // Do something when registration is successful
                Debug.Log("good");
                SceneManager.LoadScene("mainScene");
            }
            else
            {
                // Do something when registration fails
                Debug.Log("noooo");
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

