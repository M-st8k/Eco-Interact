using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Start()
    {
        int score = ScoreManagerL2.instance.GetScore(); // Get the score from the ScoreManager
        scoreText.text = "Your Score: " + score.ToString();
    }
}
