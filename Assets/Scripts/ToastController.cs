using UnityEngine;

public class ToastController : MonoBehaviour {
    public void ShowToastMessage(string message) {
        // Java 코드 호출
        AndroidJavaClass unityToast = new AndroidJavaClass("com.defaultcompany.myproject.UnityToast"); // 패키지 이름에 맞게 수정
        unityToast.CallStatic("showToast", message);
    }
}