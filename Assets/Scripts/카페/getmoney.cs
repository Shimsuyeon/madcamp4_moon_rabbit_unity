using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;
public class getmoney : MonoBehaviour
{
    public TextMeshProUGUI moneyy;
   
    // Start is called before the first frame update
    void Start()
    {
        moneyy.text = PlayerPrefs.GetInt("money", 0).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
