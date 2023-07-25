using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpTrigger : MonoBehaviour
{
    // 팝업창에 나올 메세지
    public string _popupMsg;

    // 팝업창이 나오게 하는 트리거
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