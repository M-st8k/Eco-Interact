using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestData", menuName = "QData", order = 1)]

public class QuizDataScriptTable : ScriptableObject
{
    public List<QuestionData> questions;
}
