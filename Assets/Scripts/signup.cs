using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class signup : MonoBehaviour
{
    // Start is called before the first frame update
    public void gotoSignUp()
    {
        SceneManager.LoadScene("signUpScene");
    }
}

