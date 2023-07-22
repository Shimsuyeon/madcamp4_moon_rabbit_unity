using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{
    public string shopScene;
    public void GotoScene()
    {
        SceneManager.LoadScene("shopScene");
    }
}
