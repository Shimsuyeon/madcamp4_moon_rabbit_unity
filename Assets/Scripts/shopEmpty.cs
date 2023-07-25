using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shopEmpty : MonoBehaviour
{
    public Image[] cookieImages; // 쿠키 이미지들을 배열로 저장 (Inspector에서 이미지들을 할당해야 함)

    // Start is called before the first frame update
    void Start()
    {
        string[] dataArr = PlayerPrefs.GetString("shop").Split(','); // PlayerPrefs에서 불러온 값을 Split 함수를 통해 문자열의 ,로 구분하여 배열에 저장

        int[] number2 = new int[dataArr.Length]; // 문자열 배열의 크기만큼 정수형 배열 생성

        for (int i = 0; i < dataArr.Length; i++)
        {
            number2[i] = System.Convert.ToInt32(dataArr[i]); // 문자열 형태로 저장된 값을 정수형으로 변환후 저장

            // 각 요소에 따라 쿠키 이미지를 표시하거나 숨김 처리
            if (number2[i] == 1)
            {
                cookieImages[i].gameObject.SetActive(false); // i번째 쿠키 이미지를 비활성화
            }
            else if (number2[i] == 0)
            {
                cookieImages[i].gameObject.SetActive(true); // i번째 쿠키 이미지를 활성화
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Update 함수는 필요하지 않으므로 비워둡니다.
    }
}
