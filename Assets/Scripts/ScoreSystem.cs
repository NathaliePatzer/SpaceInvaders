using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    public Text score;
    public Text hiscore;
    float scorePoints;
    public static ScoreSystem Instance;
    void Awake()
    {
        Instance = this;
    }

    public void AddScore(int points)
    {
        scorePoints += points;
        score.text = "" + scorePoints;
    }
}
