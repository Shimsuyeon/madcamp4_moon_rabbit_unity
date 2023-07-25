using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shopEmpty : MonoBehaviour
{
    public Image[] cookieImages; // ��Ű �̹������� �迭�� ���� (Inspector���� �̹������� �Ҵ��ؾ� ��)

    // Start is called before the first frame update
    void Start()
    {
        string[] dataArr = PlayerPrefs.GetString("shop").Split(','); // PlayerPrefs���� �ҷ��� ���� Split �Լ��� ���� ���ڿ��� ,�� �����Ͽ� �迭�� ����

        int[] number2 = new int[dataArr.Length]; // ���ڿ� �迭�� ũ�⸸ŭ ������ �迭 ����

        for (int i = 0; i < dataArr.Length; i++)
        {
            number2[i] = System.Convert.ToInt32(dataArr[i]); // ���ڿ� ���·� ����� ���� ���������� ��ȯ�� ����

            // �� ��ҿ� ���� ��Ű �̹����� ǥ���ϰų� ���� ó��
            if (number2[i] == 1)
            {
                cookieImages[i].gameObject.SetActive(false); // i��° ��Ű �̹����� ��Ȱ��ȭ
            }
            else if (number2[i] == 0)
            {
                cookieImages[i].gameObject.SetActive(true); // i��° ��Ű �̹����� Ȱ��ȭ
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Update �Լ��� �ʿ����� �����Ƿ� ����Ӵϴ�.
    }
}
