using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ScoreInfo", order = 1)]
public class ScoreInfo : ScriptableObject {
    public int score;

    public void Awake() {
        score = 0;
    }
}