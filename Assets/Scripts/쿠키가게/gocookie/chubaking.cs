using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class chubaking : MonoBehaviour
{
    // Start is called before the first frame update
    public void gotochuBaking()
    {
        SceneManager.LoadScene("chubakingScene");
    }
}
