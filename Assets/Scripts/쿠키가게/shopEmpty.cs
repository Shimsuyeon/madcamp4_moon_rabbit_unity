using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shopEmpty : MonoBehaviour
{
    public Image[] cookieImages = new Image[6] ; // ��Ű �̹������� �迭�� ���� (Inspector���� �̹������� �Ҵ��ؾ� ��)

    // Start is called before the first frame update
    void Start()
    {
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
        // Update �Լ��� �ʿ����� �����Ƿ� ����Ӵϴ�.
    }
}
