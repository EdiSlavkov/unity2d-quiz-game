using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    public int CorrectAnswers = 0;
    public int WrongAnswers = 0;
    private Quiz quiz;
    private int totalQuestions;
    private const int percentage = 100;

    void Start()
    {
        quiz = GetComponent<Quiz>();
        totalQuestions = quiz.GetQuestionsNumber();
    }

    public int GetScore()
    {
        return Mathf.RoundToInt((float)percentage / totalQuestions * CorrectAnswers);
    }

    public int GetCorrectAnswersNumber()
    {
        return CorrectAnswers;
    }

    public int GetWrongAnswersNumber()
    {
        return WrongAnswers;
    }
}
