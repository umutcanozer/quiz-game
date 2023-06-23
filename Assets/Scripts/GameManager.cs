using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    Quiz quiz;
    End end;

    void Awake()
    {
        quiz = FindObjectOfType<Quiz>();
        end = FindObjectOfType<End>();
    }
    void Start()
    {
        quiz.gameObject.SetActive(true);
        end.gameObject.SetActive(false);
    }


    void Update()
    {
        if(quiz.isComplete)
        {
            quiz.gameObject.SetActive(false);
            end.gameObject.SetActive(true);
            end.ShowFinalScore();
        }
    }

    public void OnReplayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
