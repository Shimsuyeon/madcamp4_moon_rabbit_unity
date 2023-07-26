using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class yabaking : MonoBehaviour
{
    // Start is called before the first frame update
    public void gotoyaBaking()
    {
        SceneManager.LoadScene("yabakingScene");
    }
}
