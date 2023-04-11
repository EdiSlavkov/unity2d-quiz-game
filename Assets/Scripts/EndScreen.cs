using TMPro;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI finalScore;

    public void SetFinalScoreText(int score, int correctAnswers, int wrongAnswers, int questionNumber)
    {
        finalScore.text = $"Score: {score}%\n" +
            $"Correct answers: {correctAnswers}\n" +
            $"Wrong answers: {wrongAnswers}\n" +
            $"Total questions: {questionNumber}";
    }
}