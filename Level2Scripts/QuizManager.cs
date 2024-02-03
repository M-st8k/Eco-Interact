using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    public static QuizManager instance;

    [SerializeField]
    private GameObject gameOver;
    [SerializeField]
    private QuizDataScriptTable question;
    [SerializeField]
    private WordData[] answerWordPrefabs;
    [SerializeField]
    private WordData[] optionsPrefabs;
    [SerializeField]
    private Image questionImg;
    [SerializeField]

    private char[] charArray = new char[16];
    private int currentIndex = 0;
    private bool correctAnswer;
    public List<int> selectedWordIndex;
    private int currentQuestionIndex = 0;
    private GameStatus gameStatus = GameStatus.Playing;
    private string answerWord;
    private int lastRandomLetterIndex = -1;
    private WordData lastClickedLetter = null;


    public GameStatus GameStatus => gameStatus;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        selectedWordIndex = new List<int>();
    }

    private void Start()
    {
        currentQuestionIndex = -1;
        SetQuestion();
    }

public void SetQuestion()
{
    if (currentQuestionIndex >= question.questions.Count)
    {
        Debug.Log("All questions answered.");
        ShowCongratulatoryScene();
        return;
    }

    currentQuestionIndex++;
    QuestionData currentQuestion = question.questions[currentQuestionIndex];

    if (currentQuestion.answer.Length > answerWordPrefabs.Length)
    {
        Debug.LogError("Not enough WordData objects in answerWordPrefabs array.");
        return;
    }

    for (int i = 0; i < currentQuestion.answer.Length; i++)
    {
        charArray[i] = char.ToUpper(currentQuestion.answer[i]);
    }

    for (int i = currentQuestion.answer.Length; i < answerWordPrefabs.Length; i++)
    {
        charArray[i] = (char)UnityEngine.Random.Range(65, 91);
    }

    ShuffleArray(charArray);

    currentIndex = 0;
    selectedWordIndex.Clear();

    questionImg.sprite = currentQuestion.questionImg;

    answerWord = currentQuestion.answer;

    ResetQuestion();

    for (int i = 0; i < answerWordPrefabs.Length; i++)
    {
        answerWordPrefabs[i].gameObject.SetActive(i < answerWord.Length); // Show only required tiles
        answerWordPrefabs[i].ResetChar();
    }

    for (int i = 0; i < optionsPrefabs.Length; i++)
    {
        optionsPrefabs[i].gameObject.SetActive(true); // Show all tiles in choices
        optionsPrefabs[i].SetChar(charArray[i]);
    }

    gameStatus = GameStatus.Playing;
}

public void SelectedOption(WordData wordData)
{
    if (gameStatus == GameStatus.Next || currentIndex >= answerWordPrefabs.Length)
        return;

    if (selectedWordIndex.Contains(wordData.transform.GetSiblingIndex()))
    {
        // Reset the letter and move it back to the option bar
        optionsPrefabs[wordData.transform.GetSiblingIndex()].gameObject.SetActive(true);
        optionsPrefabs[wordData.transform.GetSiblingIndex()].SetChar(charArray[wordData.transform.GetSiblingIndex()]);
        selectedWordIndex.Remove(wordData.transform.GetSiblingIndex());
    }
    else if (currentIndex < answerWord.Length)
    {
        answerWordPrefabs[currentIndex].SetChar(wordData.charValue);
        wordData.gameObject.SetActive(false);
        selectedWordIndex.Add(wordData.transform.GetSiblingIndex());
        currentIndex++;
    }

    // Update the last clicked letter
    lastClickedLetter = wordData;

    if (currentIndex >= answerWord.Length)
    {
        bool correctAnswer = CheckAnswer();

        if (correctAnswer)
        {
            Debug.Log("Correct answer!");

            gameStatus = GameStatus.Next;

            if (currentQuestionIndex < question.questions.Count - 1)
            {
                Invoke("SetQuestion", 0.5f);
            }
            else
            {
                ShowCongratulatoryScene();
            }
        }
        else
        {
            Debug.Log("Wrong Answer!");
            StartCoroutine(ResetAnswer());
        }
    }
}


private IEnumerator ResetAnswer()
{
    foreach (WordData wordData in answerWordPrefabs)
    {
        wordData.charText.color = Color.red;
    }

    yield return new WaitForSeconds(1f);

    foreach (WordData wordData in answerWordPrefabs)
    {
        wordData.charText.color = Color.white;
        wordData.ResetChar();
    }

    currentIndex = 0;

    foreach (int index in selectedWordIndex)
    {
        optionsPrefabs[index].gameObject.SetActive(true);
    }

    selectedWordIndex.Clear();
}



    private IEnumerator ShowWrongAnswer()
    {
        foreach (WordData wordData in answerWordPrefabs)
        {
            wordData.charText.color = Color.red;
        }

        yield return new WaitForSeconds(1f);

        foreach (WordData wordData in answerWordPrefabs)
        {
            wordData.charText.color = Color.white;
            wordData.ResetChar();
        }

        currentIndex = 0;
        selectedWordIndex.Clear();
    }

    public void ResetQuestion()
{
    for (int i = 0; i < answerWordPrefabs.Length; i++)
    {
        answerWordPrefabs[i].gameObject.SetActive(false); // Hide all answer tiles
        answerWordPrefabs[i].ResetChar();
    }

    for (int i = 0; i < answerWord.Length; i++)
    {
        answerWordPrefabs[i].gameObject.SetActive(true); // Show required answer tiles
    }

    for (int i = answerWord.Length; i < answerWordPrefabs.Length; i++)
    {
        answerWordPrefabs[i].gameObject.SetActive(false); // Hide extra answer tiles
    }

    foreach (WordData wordData in optionsPrefabs)
    {
        wordData.gameObject.SetActive(true);
    }
}


   public void ResetLastWord()
{
    if (currentIndex > 0)
    {
        currentIndex--;
        int index = selectedWordIndex[currentIndex];
        char charToMoveBack = answerWordPrefabs[currentIndex].charValue;

        // Reset the answer tile
        answerWordPrefabs[currentIndex].ResetChar();

        // Move the letter back to the options
        optionsPrefabs[index].gameObject.SetActive(true);
        optionsPrefabs[index].SetChar(charToMoveBack);

        selectedWordIndex.RemoveAt(currentIndex);
    }
}

    private void ShuffleArray<T>(T[] array)
    {
        int n = array.Length;
        while (n > 1)
        {
            int k = Random.Range(0, n--);
            T temp = array[n];
            array[n] = array[k];
            array[k] = temp;
        }
    }

    private bool CheckAnswer()
{
    string userAnswer = string.Empty;
    string correctAnswer = answerWord.ToUpper();

    for (int i = 0; i < answerWordPrefabs.Length; i++)
    {
        if (answerWordPrefabs[i].charValue != '_')
            userAnswer += answerWordPrefabs[i].charValue;

            Debug.Log($"Character {i} - User: {userAnswer}, Correct: {correctAnswer}");
    }

    return userAnswer.ToUpper() == correctAnswer;
}
public void ResetLastRandomLetter()
{
    if (lastClickedLetter != null)
    {
        // Activate the last clicked letter option
        lastClickedLetter.gameObject.SetActive(true);
        
        // Set the character of the last clicked letter option to its original value
        lastClickedLetter.SetChar(lastClickedLetter.charValue);

        // Clear the last clicked letter
        lastClickedLetter = null;
    }
}


private void ShowCongratulatoryScene()
{
    Debug.Log("Loading Congratulations scene...");
    SceneManager.LoadScene("Congratulations");
}

}

[System.Serializable]
public class QuestionData
{
    public Sprite questionImg;
    public string answer;
}

public enum GameStatus
{
    Playing,
    Next
}
