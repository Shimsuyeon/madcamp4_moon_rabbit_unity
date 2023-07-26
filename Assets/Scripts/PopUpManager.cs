using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PopUpManager : MonoBehaviour
{
    // �̱��� ���� ~~~~~~~~~~~~~~~~~~~~~~~~
    private static PopUpManager _instance;
    public static PopUpManager Instance
    {
        get
        {
            return _instance;
        }
    }
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    public GameObject _popup; // �ν����Ϳ��� �־��� ����

    // �������ڸ��� ȣ���
    private void Awake()
    {
        _popup.SetActive(false); // �˾��� ó���� �����־����
        DontDestroyOnLoad(this); // ���� �Ѿ�� �� ��ũ��Ʈ�� �ı��Ǹ� �ȵ�
        _instance = this; // PopUpManager�� NULL�� �����ʵ���
    }

    System.Action _OnClickConformButton, _OnClickCancelButton;
    // �̱��� ����
    // ~~~~~~~~~~~~~~~~~~~~~~~~~
    public Text _popMsg;

    public void Open(string text,
    System.Action OnClickConformButton, System.Action OnClickCancelButton)
    {
        _popup.SetActive(true);
        _popMsg.text = text;
        _OnClickConformButton = OnClickConformButton;
        _OnClickCancelButton = OnClickCancelButton;
    }

    public void Close()
    {
        _popup.SetActive(false);
    }

    // Ȯ�� ��ư�� ������ ��
    public void OnClickConformButton()
    {
        // �׼� �ݹ��� �Ѿ���� ����(����ó��)
        if (_OnClickConformButton != null)
        {
            Debug.Log("Ȯ�� ��ư ����");
            _OnClickConformButton(); // �ش� ��������Ʈ�� ����� �Լ� ����
        }
        Close(); // ���� �Ŀ��� â�� ���ش�.
    }

    // ��� ��ư�� ������ ��
    public void OnClickCancelButton()
    {
        if (_OnClickCancelButton != null)
        {
            Debug.Log("��� ��ư ����");
            _OnClickCancelButton();
        }
        Close();
    }
}