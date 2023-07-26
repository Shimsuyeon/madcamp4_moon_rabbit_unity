using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.UI;
using TMPro;

public class yumcookiemake : MonoBehaviour
{
    //public Image[] cookieImages;
    //public int[] havecookies = new int[4];
    public TextMeshProUGUI haveBlue;
    public TextMeshProUGUI needBlue;
    public TextMeshProUGUI haveOrange;
    public TextMeshProUGUI needOrange;
    public TextMeshProUGUI haveYellow;
    public TextMeshProUGUI needYellow;
    public TextMeshProUGUI haveRed;
    public TextMeshProUGUI needRed;

    // Start is called before the first frame update
    void Start()
    {
                    haveBlue.text = PlayerPrefs.GetInt("cookie1", 0).ToString();
                    needBlue.text = "-3";
                    haveYellow.text = PlayerPrefs.GetInt("cookie2", 0).ToString();
                    needYellow.text = "-3";

                    haveOrange.text = PlayerPrefs.GetInt("cookie3", 0).ToString();
                    needOrange.text = "-5";

                    haveRed.text = PlayerPrefs.GetInt("cookie4", 0).ToString();
                    needRed.text = "-5";

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
