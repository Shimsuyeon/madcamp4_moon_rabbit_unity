using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class baking : MonoBehaviour
{
    // Start is called before the first frame update
    public void gotoBaking()
    {
        SceneManager.LoadScene("bakingScene");
    }
}
