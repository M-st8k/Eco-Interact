using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu(fileName = "L3ScriptTable", menuName = "Level3/L3ScriptTable", order = 1)]
public class L3ScriptTable : ScriptableObject
{
    public List<L3QuestionData> questions;
}

[System.Serializable]
public class L3QuestionData
{
    public GameObject imageHolderPrefab;
    public GameObject answerHolderPrefab;
    public List<Sprite> draggableImages; // Change this to a prefab reference
    public List<string> imageTags;
    public int order;
    public string correctImageTag; // Rename this to correctImageTag
}