using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private List<QuestionSO> questions = new List<QuestionSO>();
    [SerializeField] private List<Button> buttons = new List<Button>();
    [SerializeField] private Timer timer;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Slider progressBar;
    [SerializeField] private Button JokerButton;
    private GameManager gameManager;
    private int questionCount;
    private ScoreKeeper scoreKeeper;
    private QuestionSO currentQuestion;
    private bool isAnswered = false;
    private int correctAnswerIndex;
    private EndScreen endScreen;

    private void Awake()
    {
        endScreen = FindObjectOfType<EndScreen>();
        gameManager = FindObjectOfType<GameManager>();
        questionCount = questions.Count;
        progressBar.maxValue = questionCount;
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        SetQuizScreenData();
        timer.SetTimeToShowAnswer();
    }

    private void Update()
    {
        if (isAnswered)
        {
            SetButtonState(false);
            timer.SetTimeToShowResult();
            scoreText.text = $"Score:{scoreKeeper.GetScore()}%";
            isAnswered = false;
            JokerButton.enabled = false;
        }
        if (questions.Count > 0 && timer.RemainingTime <= 0)
        {
            JokerButton.enabled = true;
            SetButtonState(true);
            SetDefaultButtonsColor();
            SetQuizScreenData();
            timer.SetTimeToShowAnswer();
        }
        else if (questions.Count <= 0 && timer.RemainingTime <= 0)
        {
            
            endScreen.SetFinalScoreText(scoreKeeper.GetScore(),
                                        scoreKeeper.GetCorrectAnswersNumber(),
                                        scoreKeeper.GetWrongAnswersNumber(),
                                        GetQuestionsNumber());

            gameManager.GoToEndScreen();
        }
    }

    public QuestionSO GetRandomQuestion()
    {
        int randomIndex = Random.Range(0, questions.Count);
        return questions[randomIndex];
    }

    public void DeleteQuestion(QuestionSO question)
    {
        questions.Remove(question);
    }

    public void FillButtonsWithAnswers(QuestionSO question)
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            TextMeshProUGUI buttonText = buttons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = question.GetAnswer(i);
        }
    }

    public void CheckSubmitedAnswer(int buttonIndex)
    {
        Image buttonImage = buttons[buttonIndex].gameObject.GetComponent<Image>();

        if (buttonIndex == correctAnswerIndex)
        {
            buttonImage.color = Color.green;
            questionText.text = "Correct!";
            scoreKeeper.CorrectAnswers++;
        }
        else
        {
            buttonImage.color = Color.red;
            questionText.text = "Wrong!\n Correct answer is " + currentQuestion.GetAnswer(correctAnswerIndex);
            buttons[correctAnswerIndex].GetComponent<Image>().color = Color.green;
            scoreKeeper.WrongAnswers++;
        }
        isAnswered = true;
    }

    public void SetButtonState(bool state)
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].interactable = state;
        }
    }

    public void SetDefaultButtonsColor()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            Image buttonImage = buttons[i].gameObject.GetComponent<Image>();
            buttonImage.color = Color.white;
        }
    }

    public int GetQuestionsNumber()
    {
        return questionCount;
    }

    public void SetQuizScreenData()
    {
        currentQuestion = GetRandomQuestion();
        questionText.text = currentQuestion.GetQuestion();
        FillButtonsWithAnswers(currentQuestion);
        DeleteQuestion(currentQuestion);
        progressBar.value++;
        correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
    }

    public void UseJoker()
    {
        int buttonsToDisable = 2;
        for (int i = 0; i < buttonsToDisable; i++)
        {
            EliminateButton();

        }
        DisableJokerButton();
    }

    public void DisableJokerButton()
    {
        JokerButton.interactable = false;
        JokerButton.GetComponent<Image>().color = Color.red;
    }

    public int GetWrongButtonIndex()
    {
        int randomButtonIndex = Random.Range(0, buttons.Count);
        if (buttons[randomButtonIndex].interactable == false ||
            randomButtonIndex == correctAnswerIndex)
        {
            return GetWrongButtonIndex();
        }
        else
        {
            return randomButtonIndex;
        }
    }

    public void EliminateButton()
    {
        int index = GetWrongButtonIndex();
        buttons[index].GetComponentInChildren<TextMeshProUGUI>().text = "";
        buttons[index].interactable = false;
    }
}