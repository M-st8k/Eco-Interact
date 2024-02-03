using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GAMEUI : MonoBehaviour
{
    [SerializeField] private GameCode quizManager;
    [SerializeField] private TextMeshProUGUI imgName;
    [SerializeField] private Image imgQuestion;
    [SerializeField] private List<Button> options;
    [SerializeField] private Color correctAnswer;
    [SerializeField] private Color wrongAnswer;
    [SerializeField] private Color normalQuestion;
  //  [SerializeField] private TextMeshProUGUI scoreTextMeshPro;

    //public TextMeshProUGUI scoreTextMeshPro; // Make sure this is properly defined in your script

    private Question question;
    private bool answered;

    private void Awake()
    {
//        scoreTextMeshPro.gameObject.SetActive(false); // Hide the score text initially
        for (int i = 0; i < options.Count; i++)
        {
            int index = i; // Store the current index to avoid closure issues
            options[i].onClick.AddListener(() => AnswerButton(index));
        }
    }

    public void OperateQuestion(Question question)
    {
        this.question = question;
        imgName.text = question.nameImg;
        imgQuestion.transform.parent.gameObject.SetActive(true);
        imgQuestion.sprite = question.questionImg;

        List<string> answerList = question.options;

        for (int i = 0; i < options.Count; i++)
        {
            string optionValue = answerList[i];

            options[i].name = optionValue;
            options[i].GetComponentInChildren<TextMeshProUGUI>(true).text = optionValue;

            // Reset the button colors
            if (question.answeredCorrectly)
        {
            if (options[i].name == question.correctAns)
            {
                options[i].image.color = correctAnswer;
            }
            else
            {
                options[i].image.color = normalQuestion;
            }
        }
        else
        {
            options[i].image.color = normalQuestion;
        }
    }

        answered = false;
    }
    private void AnswerButton(int index)
    {
        if (!answered)
        {
            answered = true;
            string selectedOption = options[index].name;
            bool isCorrect = selectedOption == question.correctAns;

            options[index].image.color = isCorrect ? correctAnswer : wrongAnswer;

            quizManager.Answer(isCorrect);
        }
    }
    
     public void ResetButtonColors()
    {
        foreach (Button option in options)
        {
            option.image.color = normalQuestion;
        }
    }
}