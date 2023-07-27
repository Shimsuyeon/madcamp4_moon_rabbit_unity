using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buyshop : MonoBehaviour
{
    public GameObject cupcakeObject;
    public GameObject teapotObject;
    public GameObject breadObject;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("cafe1", 0) == 0)
        {
            cupcakeObject.gameObject.SetActive(false);
        }else if (PlayerPrefs.GetInt("cafe1", 0) == 1)
        {
            cupcakeObject.gameObject.SetActive(true);
        }

        if (PlayerPrefs.GetInt("cafe2", 0) == 0)
        {
            teapotObject.gameObject.SetActive(false);
        }else if (PlayerPrefs.GetInt("cafe2", 0) == 1)
        {
            teapotObject.gameObject.SetActive(true);
        }

        if(PlayerPrefs.GetInt("cafe3", 0) == 0)
        {
            breadObject.gameObject.SetActive(false);
        } else if (PlayerPrefs.GetInt("cafe3", 1) == 0)
        {
            breadObject.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
