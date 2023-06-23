using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class End : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    Score score;
    void Awake()
    {
        score = FindObjectOfType<Score>();
    }

    public void ShowFinalScore()
    {
        scoreText.text = "Congratulations!\nYou got a score of "+ score.CalculateScore()+"%";
    }

    
}
