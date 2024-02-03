using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class DropdownController : MonoBehaviour
{
    
    public TextMeshProUGUI definitionText;
    private bool isDefinitionVisible = false;

    private void Start()
    {
        // Hide the definition text initially
        definitionText.gameObject.SetActive(false);
    }

    public void ToggleDefinition()
    {
        isDefinitionVisible = !isDefinitionVisible;

        if (isDefinitionVisible)
        {
            // Show the definition text
            definitionText.gameObject.SetActive(true);
        }
        else
        {
            // Hide the definition text
            definitionText.gameObject.SetActive(false);
        }
    }
}