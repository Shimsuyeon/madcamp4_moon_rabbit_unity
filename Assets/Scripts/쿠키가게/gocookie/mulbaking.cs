using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mulbaking : MonoBehaviour
{
    // Start is called before the first frame update
    public void gotomulBaking()
    {
        SceneManager.LoadScene("mulbakingScene");
    }
}
