using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Image;
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnClickStartButton()
    {
        Image.SetActive(false);
    }
}
