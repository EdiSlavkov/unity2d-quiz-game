using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Question", fileName = "Question")]
public class QuestionSO : ScriptableObject
{
    [TextArea(2, 5)]
    [SerializeField] private string questionText = "Enter your question";
    [SerializeField] private string[] answers = new string[4];
    [Range(0, 3)]
    [SerializeField] private int correctAnswerIndex;

    public string GetQuestion()
    {
        return questionText;
    }

    public string GetAnswer(int index)
    {
        return answers[index];
    }

    public int GetCorrectAnswerIndex()
    {
        return correctAnswerIndex;
    }
}
