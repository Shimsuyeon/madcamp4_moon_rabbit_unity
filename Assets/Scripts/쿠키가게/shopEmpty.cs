using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shopEmpty : MonoBehaviour
{
    public Image[] cookieImages = new Image[6] ; // 쿠키 이미지들을 배열로 저장 (Inspector에서 이미지들을 할당해야 함)

    // Start is called before the first frame update
    void Start()
    {
        string[] dataArr = PlayerPrefs.GetString("shop").Split(','); // PlayerPrefs에서 불러온 값을 Split 함수를 통해 문자열의 ,로 구분하여 배열에 저장

        int[] number2 = new int[dataArr.Length]; // 문자열 배열의 크기만큼 정수형 배열 생성
        if (PlayerPrefs.GetInt("shop1", 0) == 0)
        {
            cookieImages[0].gameObject.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("shop1", 0) == 1)
        {
            cookieImages[0].gameObject.SetActive(false);
        }


        if (PlayerPrefs.GetInt("shop2", 0) == 0)
        {
            cookieImages[1].gameObject.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("shop2", 0) == 1)
        {
            cookieImages[1].gameObject.SetActive(false);
        }



        if (PlayerPrefs.GetInt("shop3", 0) == 0)
        {
            cookieImages[2].gameObject.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("shop3", 0) ==1)
        {
            cookieImages[2].gameObject.SetActive(false);
        }

        if (PlayerPrefs.GetInt("shop4", 0) == 0)
        {
            cookieImages[3].gameObject.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("shop4", 0) == 1)
        {
            cookieImages[3].gameObject.SetActive(false);
        }

        if (PlayerPrefs.GetInt("shop5", 0) == 0)
        {
            cookieImages[4].gameObject.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("shop5", 0) == 1)
        {
            cookieImages[4].gameObject.SetActive(false);
        }

        if (PlayerPrefs.GetInt("shop6", 0) == 0)
        {
            cookieImages[5].gameObject.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("shop6", 0) == 1)
        {
            cookieImages[5].gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Update 함수는 필요하지 않으므로 비워둡니다.
    }
}
