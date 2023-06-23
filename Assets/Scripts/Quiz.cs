using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    TextMeshProUGUI buttonText;
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO currentQuestion;

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    int correctAnswer;
    bool hasAnsweredEarly = true;

    [Header("Button Colors")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;
    Score score;

    [Header("Bar")]
    [SerializeField] Slider progressBar;

    public bool isComplete;

    void Awake()
    {  
        timer = FindObjectOfType<Timer>();
        score = FindAnyObjectByType<Score>();
        progressBar.maxValue = questions.Count;
        progressBar.minValue = 0;
    }

    void Update()
    {
        timerImage.fillAmount = timer.fillFraction;
        if (timer.loadNextQuestion)
        {
            if (progressBar.value == progressBar.maxValue)
            {
                isComplete = true;
                return;
            }

            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }
        else if (!hasAnsweredEarly && !timer.isAnswering)
        {
            DisplayAnswer(-1); //it automatically detect it like wrong aswer
            SetButtonState(false);
        }

    }
    public void OnAnswerSelected(int index)
    {
        hasAnsweredEarly = true;
        DisplayAnswer(index);
        SetButtonState(false);
        timer.CancelTimer();
        scoreText.text = "Score: " + score.CalculateScore()+"%";
    }

    void DisplayAnswer(int index)
    {
        Image buttonImage;
        if (index == currentQuestion.GetCorrectAnswerIndex())
        {
            questionText.text = "Correct!";
            buttonImage = answerButtons[index].GetComponent<Image>(); //Get component Image from answerButtons array which the correct one's elements.
            buttonImage.sprite = correctAnswerSprite; //change the source image when the correct answer is selected.
            score.IncrementCorrectAnswers();
        }
        else
        {
            correctAnswer = currentQuestion.GetCorrectAnswerIndex();
            questionText.text = "Sorry, the correct answer:\n" + currentQuestion.GetAnswer(correctAnswer);
            buttonImage = answerButtons[correctAnswer].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
    }

    void GetNextQuestion()
    {
        if(questions.Count > 0)
        {
            SetButtonState(true);
            SetDefaultButtonSprites();
            GetRandomQuestion();
            DisplayQuestion();
            score.IncrementSeenQuestion();
            progressBar.value++;
        }
        
    }

    void GetRandomQuestion()
    {
        int index = Random.Range(0, questions.Count);
        currentQuestion = questions[index];

        if(questions.Contains(currentQuestion))
        {
            questions.Remove(currentQuestion);
        }   
    }


    void DisplayQuestion()
    {
        questionText.text = currentQuestion.GetQuestion();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswer(i);
        }
    }

    void SetButtonState(bool state)
    {
        for(int i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();    
            button.interactable = state;
        }
    }

    void SetDefaultButtonSprites()
    {
        for(int i=0; i < answerButtons.Length; i++)
        {
            Image buttonSprite = answerButtons[i].GetComponent<Image>();
            buttonSprite.sprite = defaultAnswerSprite;
        }

    }
}
