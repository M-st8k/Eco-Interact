using UnityEngine;

public class UIManager : MonoBehaviour
{
    private GameObject currentQuestionSet;
    private Draggable[] draggableImagesInstances;

    private void Start()
    {
        LoadQuestionSet();
    }

    private void LoadQuestionSet()
    {
        // Replace "QuestionSetPrefab" with the actual prefab name you want to instantiate
        GameObject questionSetPrefab = Resources.Load<GameObject>("QuestionSetPrefab");
        currentQuestionSet = Instantiate(questionSetPrefab);

        draggableImagesInstances = currentQuestionSet.GetComponentsInChildren<Draggable>();

        Debug.Log("Question Set Loaded.");
    }
}