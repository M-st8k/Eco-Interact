using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestionData", menuName = "QuestionData")]
public class gameQuestion : ScriptableObject
{
    public List<Question> questions;
}