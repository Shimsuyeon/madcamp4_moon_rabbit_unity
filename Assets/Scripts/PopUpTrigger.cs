using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpTrigger : MonoBehaviour
{
    // �˾�â�� ���� �޼���
    public string _popupMsg;

    // �˾�â�� ������ �ϴ� Ʈ����
    public void OnClickTrigger()
    {
        PopUpManager.Instance.Open(_popupMsg,
           OnClickConformButton: () =>
           {
               Debug.Log("On Trigger Conform Button");
           }, OnClickCancelButton: () =>
           {
               Debug.Log("On Click Cancel Button");
           });
    }
}