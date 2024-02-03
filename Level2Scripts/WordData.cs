using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WordData : MonoBehaviour
{
    [SerializeField] // Set the value from the inspector
    public TextMeshProUGUI charText; // Variable to store the letter
    [HideInInspector]
    public char charValue; // When we assign a variable to the char data, get it from this variable
    private Button buttonObj;

    private void Awake()
    {
        buttonObj = GetComponent<Button>();

        if (buttonObj)
        {
            buttonObj.onClick.AddListener(() => CharSelected()); // If not null, assign the buttonObj. Whenever it is clicked, CharSelected is called
        }
    }

    public void SetChar(char value) // Method to set the character
    {
        charText.text = value + "";
        charValue = value;
        UpdateText();
    }

    public void UpdateText()
    {
        if (charText != null)
        {
            charText.text = charValue.ToString();
        }
        else
        {
            Debug.LogError("TextMeshProUGUI component is missing on WordData object.");
        }
    }

    public void CharSelected() // Method when the button is clicked
    {
        if (QuizManager.instance.GameStatus == GameStatus.Playing)
        {
            QuizManager.instance.SelectedOption(this);
        }
    }

    public void ResetChar()
    {
        SetChar('_');
    }
}
