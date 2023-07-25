using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class mainSceneStartButton : MonoBehaviour {
    public Button startButton;
    public Button rankButton;

    void Start() {
        startButton.onClick.AddListener(StartButtonClick);
        rankButton.onClick.AddListener(RankButtonClick);
    }

    void Update() {
        
    }

    void StartButtonClick() {
        SceneManager.LoadScene("MoonRabbit");
    }

    void RankButtonClick() {
        SceneManager.LoadScene("LeaderBoardScene");
    }
}
