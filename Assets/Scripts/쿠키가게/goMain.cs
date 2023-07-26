using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class goMain : MonoBehaviour
{
    public Button gomain;
    // Start is called before the first frame update
    void Start()
    {
        gomain.onClick.AddListener(gomainfunc);
    }

    void gomainfunc()
    {
        SceneManager.LoadScene("mainScene");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
