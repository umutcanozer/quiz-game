using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quiz Question", fileName = "New Question")]
public class QuestionSO : ScriptableObject
{
    [TextArea(2,6)]
    [SerializeField] string question = "Enter new question text here";
    [SerializeField] string[] answers = { "Answer1", "Answer2", "Answer3", "Answer4" };
    [SerializeField] int correctAnswer;

    //or->  string[] answers = new string[4];
    //      answers[number(0-1)] = answer;

    public string GetQuestion()
    {
        return question; //get the question text
    }

    public int GetCorrectAnswerIndex()
    {
        return correctAnswer; //get the correct answer index
    }

    public string GetAnswer(int index)
    {
        return answers[index]; //get the four answers text from array by using index
    }
}
