using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameCode : MonoBehaviour
{
    [SerializeField] private GAMEUI quizUi;
    [SerializeField] private gameQuestion quizData;
    private List<Question> questions;
    private Question selectedQuestion;
    private List<Question> unusedQuestions;
    
    private int totalScore = 0;


    private void Start()
    {
        questions = quizData.questions;
       // SelectQuestion();
        unusedQuestions = new List<Question>(questions);
        ShuffleQuestions();
        StartGame();
    }
    private void ShuffleQuestions()
    {
        int count = unusedQuestions.Count;
        while (count > 1)
        {
            count--;
            int index = Random.Range(0, count + 1);
            Question temp = unusedQuestions[index];
            unusedQuestions[index] = unusedQuestions[count];
            unusedQuestions[count] = temp;
        }
    }

    //private void SelectQuestion()
   // {
    //    int value = Random.Range(0, questions.Count);
    //    selectedQuestion = questions[value];
   //     quizUi.OperateQuestion(selectedQuestion);
   // }
     public void StartGame()
    {
        totalScore = 0;
        LoadNextQuestion();
    }
    private void LoadNextQuestion()
    {
        if (unusedQuestions.Count > 0)
        {
            selectedQuestion = unusedQuestions[0];
            unusedQuestions.RemoveAt(0);

            quizUi.OperateQuestion(selectedQuestion);
            quizUi.ResetButtonColors(); // Reset button colors here
        }
        else
        {
            EndGame();
        }
    }


    public void Answer(bool isCorrect)
    {
        if (isCorrect)
        {
            totalScore++;
            selectedQuestion.answeredCorrectly = true; // Set the answeredCorrectly flag
            Debug.Log("Correct answer!");
            // Handle correct answer
        }
        LoadNextQuestion();
    }
     private void EndGame()
    {
        ScoreManagerL2.instance.SetScore(totalScore); // Set the score in the ScoreManager
        SceneManager.LoadScene("Congrats"); // Load the score scene
    }
}


[System.Serializable]
public class Question
{
    public string nameImg;
    public Sprite questionImg;
    public List<string> options;
    public string correctAns;
    public bool isUsed; // Add this field
    public bool answeredCorrectly; // Add this field
}