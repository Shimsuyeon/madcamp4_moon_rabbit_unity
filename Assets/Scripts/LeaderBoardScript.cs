using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Linq;
using TMPro;

public class LeaderBoardScript : MonoBehaviour {
    public RankUnit[] rank;
    public GameObject rankPrefab;
    public GameObject scrollContent;
    bool d = false;


    void Start() {
        rank = new RankUnit[80];
        GetRankData();
    }

    void Update() {
        // scrollControl();
        if (!d)
            PrintRank();
    }

    public void GetRankData() {
        var url = string.Format("{0}/{1}", "http://34.64.98.2:3000", "api/rank");

        StartCoroutine(GetRank(url, (response) => {
            var res = JsonConvert.DeserializeObject<RankUnit[]>(response);
            rank = res;
            
            rank = rank.OrderByDescending(unit => unit?.score ?? int.MinValue).ToArray();

            foreach (var item in rank) {
                Debug.LogFormat("{0}: {1}", item.id, item.score);
            }
        }));

        PrintRank();
    }

    public IEnumerator GetRank(string url, System.Action<string> callback)
    {
        var webRequest = new UnityWebRequest(url, "GET");
        Debug.Log(url);

        webRequest.downloadHandler = new DownloadHandlerBuffer();

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

    void PrintRank() {
        // Debug.Log("here2");
        for (int i = 0; i < rank.Length; i++) {
            if (rank[i] != null) {
                // Debug.Log("here1");
                GameObject rankUnit = Instantiate(rankPrefab, scrollContent.transform);
                Vector3 pos = rankUnit.transform.localPosition;
                pos.y = -i * 100 - 50;
                rankUnit.transform.localPosition = pos;
                rankUnit.transform.GetChild(0).GetComponent<TMP_Text>().text = (i + 1).ToString();
                rankUnit.transform.GetChild(1).GetComponent<TMP_Text>().text = rank[i].id;
                rankUnit.transform.GetChild(2).GetComponent<TMP_Text>().text = rank[i].score.ToString();
                d = true;
            }
        }
    }

    void delay() {

    }
}
