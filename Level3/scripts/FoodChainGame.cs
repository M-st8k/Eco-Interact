using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FoodChainGame : MonoBehaviour
{
    [System.Serializable]
    public class Question
    {
        public Transform optionHolder;
        public Transform answerHolder;
    }

    public TextMeshProUGUI messageText;
    public Button nextButton;
    public Question[] questions;

    private GameObject[] options;
    private bool[] placedCorrectly;
    private int numCorrectPlacements;
    private int totalOptions;

    private int currentQuestionIndex = 0;

    private void Start()
    {
        options = new GameObject[questions[currentQuestionIndex].optionHolder.childCount];
        placedCorrectly = new bool[questions[currentQuestionIndex].optionHolder.childCount];
        totalOptions = questions[currentQuestionIndex].optionHolder.childCount;
        numCorrectPlacements = 0;

        for (int i = 0; i < questions[currentQuestionIndex].optionHolder.childCount; i++)
        {
            GameObject option = questions[currentQuestionIndex].optionHolder.GetChild(i).gameObject;
            options[i] = option;

            DragAndDrop dragAndDrop = option.AddComponent<DragAndDrop>();
        }

        nextButton.onClick.AddListener(NextQuestion);

        SetupQuestion();
    }

    private void SetupQuestion()
    {
        Question currentQuestion = questions[currentQuestionIndex];

        // Reset placements
        for (int i = 0; i < currentQuestion.optionHolder.childCount; i++)
        {
            GameObject option = currentQuestion.optionHolder.GetChild(i).gameObject;
            option.transform.SetParent(currentQuestion.optionHolder);
            option.transform.localPosition = Vector3.zero;
            placedCorrectly[i] = false;
        }

        numCorrectPlacements = 0;
        messageText.text = "Drag all images into the circle.";

        // Enable/disable answer images
        for (int i = 0; i < currentQuestion.answerHolder.childCount; i++)
        {
            GameObject answer = currentQuestion.answerHolder.GetChild(i).gameObject;
            answer.SetActive(true);
        }
    }

    private void NextQuestion()
    {
        currentQuestionIndex++;
        if (currentQuestionIndex >= questions.Length)
        {
            // Game finished
            messageText.text = "Congratulations! You completed all questions!";
            nextButton.interactable = false;
        }
        else
        {
            SetupQuestion();
        }
    }

    public void CheckPlacements()
    {
        numCorrectPlacements = 0;
        Question currentQuestion = questions[currentQuestionIndex];

        for (int i = 0; i < currentQuestion.answerHolder.childCount; i++)
        {
            GameObject answer = currentQuestion.answerHolder.GetChild(i).gameObject;

            for (int j = 0; j < currentQuestion.optionHolder.childCount; j++)
            {
                GameObject option = currentQuestion.optionHolder.GetChild(j).gameObject;

                if (answer.CompareTag(option.tag))
                {
                    if (answer.transform.childCount > 0)
                    {
                        GameObject placedObject = answer.transform.GetChild(0).gameObject;
                        if (placedObject == option)
                        {
                            placedCorrectly[j] = true;
                            numCorrectPlacements++;
                        }
                        else
                        {
                            placedCorrectly[j] = false;
                            placedObject.GetComponent<DragAndDrop>().OnEndDrag(null);
                        }
                    }
                }
            }
        }

        if (numCorrectPlacements == totalOptions)
        {
            messageText.text = "Congratulations! You placed everything correctly!";
            nextButton.interactable = true;
        }
        else
        {
            messageText.text = "Some items are not placed correctly.";
            nextButton.interactable = false;
        }
    }
}