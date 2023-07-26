using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.UI;
using TMPro;

public class mulcookiemake : MonoBehaviour
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
        string[] dataArr = PlayerPrefs.GetString("cookies").Split(','); // PlayerPrefs���� �ҷ��� ���� Split �Լ��� ���� ���ڿ��� ,�� �����Ͽ� �迭�� ����

        int[] number2 = new int[dataArr.Length]; // ���ڿ� �迭�� ũ�⸸ŭ ������ �迭 ����

        for (int i = 0; i < dataArr.Length; i++)
        {
            number2[i] = System.Convert.ToInt32(dataArr[i]); // ���ڿ� ���·� ����� ���� ���������� ��ȯ�� ����
            //havecookies[i] = number2[i];
            //Debug.Log($"havecookies[{i}]: {havecookies[i]}"); // Print the values for debugging

            // ��Ű ������ ���� TextMeshProUGUI ��ҿ� �ش� ���� ǥ��
            switch (i)
            {
                case 0:
                    haveBlue.text = PlayerPrefs.GetInt("cookie1", 0).ToString();
                    needBlue.text = "-1";
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
                    needRed.text = "-2";
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
