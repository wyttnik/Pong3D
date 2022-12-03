using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private int scoreLeft = 0;
    private int scoreRight = 0;

    public Text scoreTextLeft;
    public Text scoreTextRight;

    private bool started = false;

    public GameObject ball, player1, player2;
    private BallController ballController;
    private Vector3 startingPosition;

    public Starter starter;

    void Start()
    {
        ballController = ball.GetComponent<BallController>();
        startingPosition = ball.transform.position;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (started)
            {
                if (Time.timeScale > 0)
                    Time.timeScale = 0;
                else Time.timeScale = 1;
            }
            else
            {
                started = true;
                starter.StartCountdown();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }

        if (Input.GetKeyDown(KeyCode.L)) {
            RacketController racket = player1.GetComponent<RacketController>();
            racket.isPlayer = !racket.isPlayer;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            RacketController racket = player2.GetComponent<RacketController>();
            racket.isPlayer = !racket.isPlayer;
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            ResetUI();
        }
    }

    public void ScoreGoalLeft()
    {
        scoreRight += 1;
        UpdateUI();
        ResetBall();
    }

    public void ScoreGoalRight()
    {
        scoreLeft += 1;
        UpdateUI();
        ResetBall();
    }

    void UpdateUI()
    {
        scoreTextLeft.text = scoreLeft.ToString();
        scoreTextRight.text = scoreRight.ToString();
    }

    void ResetUI()
    {
        ballController.Stop();
        ball.transform.position = startingPosition;
        started = false;
        scoreTextLeft.text = "0";
        scoreTextRight.text = "0";
    }

    void ResetBall()
    {
        ballController.Stop();
        ball.transform.position = startingPosition;
        starter.StartCountdown();
    }

    public void StartGame()
    {
        ballController.Go();
    }
}
