﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class ManagerScript : MonoBehaviour {
    public static bool HasStart;
    public static bool GameIsOver;
    public static bool CanKick;
    private int _score;
    public Text ScoreTxt;
    public Animator GameOverAnim;

    public AudioClip gameOverClip;
    public AudioSource source;
   
    public GameObject ActiveIcon;
    public GameObject InactiveIcon;
    private float _distance;
    public float RangeMin;
    public float RangeMax;  
    // Use this for initialization
    void Start () {
        InitialSet();
    }
    void Awake() {
        source = GetComponent<AudioSource>();
    }
    void InitialSet() {
        HasStart = false;
        _score = 0;
        GameIsOver = false;
        ScoreTxt.text = _score.ToString();
        DeactiveUIIcons();
        CanKick = true;
        InputBall.Instance.RestartBallPosition();
    }	
    void GameOver() {
        GameIsOver = true;
        HasStart = true;
        GameOverAnim.SetTrigger("GameOver");
        if (_score > PlayerPrefs.GetInt("Score")) {
            PlayerPrefs.SetInt("Score",_score);
        }       
        source.PlayOneShot(gameOverClip);
        DeactiveUIIcons();
    }
    void DeactiveUIIcons() {
        ActiveIcon.SetActive(false);
        InactiveIcon.SetActive(false);
    }
    public void RestartGame() {
        GameOverAnim.SetTrigger("Restart");
        InitialSet();
    }
    void IncrementScore() {
        _score += 1;
        ScoreTxt.text = _score.ToString();
    }
    void OnEnable() {
        InputBall.OnCollisionGround += GameOver;
        InputBall.OnKickBall += IncrementScore;
    }
    void OnDisable()  {
        InputBall.OnCollisionGround -= GameOver;
        InputBall.OnKickBall -= IncrementScore;
    }

    void Update() {       
        if (HasStart) {
           if (DistanceManager.Instance.isInRange)  {
                ActiveIcon.SetActive(true);
                InactiveIcon.SetActive(false);
                CanKick = true;
            } else {
                ActiveIcon.SetActive(false);
                InactiveIcon.SetActive(true);
                CanKick = false;
            }
        }        
    }
}
