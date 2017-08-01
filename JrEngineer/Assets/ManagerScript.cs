using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerScript : MonoBehaviour {
    public static bool HasStart;
    public static bool GameIsOver;
    public int Score;
    public Text ScoreTxt;
    public Animator GameOverAnim;

    // Use this for initialization
    void Start () {
        InitialSet();
    }
    void InitialSet() {
        HasStart = false;
        Score = 0;
        GameIsOver = false;
        ScoreTxt.text = Score.ToString();
    }	
    void GameOver() {
        GameIsOver = true;
        HasStart = true;
        GameOverAnim.SetTrigger("GameOver");
    }

    public void RestartGame() {
        GameOverAnim.SetTrigger("Restart");
        InitialSet();
    }
    void IncrementScore() {
        Score += 1;
        ScoreTxt.text = Score.ToString();
    }
    void OnEnable()
    {
        InputBall.OnCollisionGround += GameOver;
        InputBall.OnKickBall += IncrementScore;
    }
    void OnDisable()
    {
        InputBall.OnCollisionGround -= GameOver;
        InputBall.OnKickBall -= IncrementScore;
    }

}
