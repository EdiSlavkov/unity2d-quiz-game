using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private float timeToAnswer = 10f;
    [SerializeField] private float timeToShowResult = 4f;
    private float currentTime;
    public float RemainingTime;
    private Image timer;

    private void Awake()
    {
        timer = GetComponent<Image>();
    }

    private void Update()
    {
        RemainingTime -= Time.deltaTime;
        timer.fillAmount = RemainingTime / currentTime;
    }

    public void SetTimeToShowAnswer()
    {
        currentTime = timeToAnswer;
        RemainingTime = timeToAnswer;
    }

    public void SetTimeToShowResult()
    {
        currentTime = timeToShowResult;
        RemainingTime = timeToShowResult;
    }
}