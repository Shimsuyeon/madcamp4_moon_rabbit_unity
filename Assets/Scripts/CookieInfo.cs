using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CookieInfo", order = 1)]
public class CookieInfo : ScriptableObject {
    public int[] starCookieEaten;

    public void Awake() {
        starCookieEaten = new int[4];
    }
}