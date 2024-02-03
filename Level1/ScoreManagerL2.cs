using UnityEngine;
using UnityEngine.UI;

public class ScoreManagerL2 : MonoBehaviour
{
    public static ScoreManagerL2 instance;
    private int score;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetScore(int newScore)
    {
        score = newScore;
    }

    public int GetScore()
    {
        return score;
    }
}
