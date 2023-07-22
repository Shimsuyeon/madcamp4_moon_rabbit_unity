using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class mainSceneStartButton : MonoBehaviour {
    public Button startButton;

    void Start() {
        startButton.onClick.AddListener(StartButtonClick);
    }

    void Update() {
        
    }

    void StartButtonClick() {
        SceneManager.LoadScene("MoonRabbit");
    }
}
