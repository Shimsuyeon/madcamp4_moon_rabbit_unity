using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.UI;
using TMPro;

public class chucookiemake : MonoBehaviour
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
        string[] dataArr = PlayerPrefs.GetString("cookies").Split(','); // PlayerPrefs에서 불러온 값을 Split 함수를 통해 문자열의 ,로 구분하여 배열에 저장

        int[] number2 = new int[dataArr.Length]; // 문자열 배열의 크기만큼 정수형 배열 생성

        for (int i = 0; i < dataArr.Length; i++)
        {
            number2[i] = System.Convert.ToInt32(dataArr[i]); // 문자열 형태로 저장된 값을 정수형으로 변환후 저장
            //havecookies[i] = number2[i];
            //Debug.Log($"havecookies[{i}]: {havecookies[i]}"); // Print the values for debugging

            // 쿠키 종류에 따라 TextMeshProUGUI 요소에 해당 값을 표시
            switch (i)
            {
                case 0:
                    haveBlue.text = PlayerPrefs.GetInt("cookie1", 0).ToString();
                    needBlue.text = "-3";
                    break;
                case 1:
                    haveYellow.text = PlayerPrefs.GetInt("cookie2", 0).ToString();
                    needYellow.text = "-2";
                    break;
                case 2:
                    haveOrange.text = PlayerPrefs.GetInt("cookie3", 0).ToString();
                    needOrange.text = "-3";
                    break;
                case 3:
                    haveRed.text = PlayerPrefs.GetInt("cookie4", 0).ToString();
                    needRed.text = "-3";
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
