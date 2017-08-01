using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerScript : MonoBehaviour {
    public static bool HasStart;
    public static bool GameIsOver;
    public static bool CanKick;
    private int _score;
    public Text ScoreTxt;
    public Animator GameOverAnim;
    Vector3 Ball;
    Vector3 Ground;

    public GameObject ballPos;
    public GameObject groundPos;

    public GameObject ActiveIcon;
    public GameObject InactiveIcon;
    private float _distance;
    public float RangeMin;
    public float RangeMax;

    void Awake() {
        Ball = ballPos.transform.position;
        Ground = new Vector3(0, groundPos.transform.position.y,0);
    }
    // Use this for initialization
    void Start () {
        InitialSet();
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

    void Update() {
        FloorMeasure();
        if (HasStart) {
           if (_distance >= RangeMin && _distance <= RangeMax)
            {
                ActiveIcon.SetActive(true);
                InactiveIcon.SetActive(false);
                CanKick = true;
            }
            else {
                ActiveIcon.SetActive(false);
                InactiveIcon.SetActive(true);
                CanKick = false;
            }
        }        
    }

    void FloorMeasure()
    {
        RaycastHit hit;
        if (Physics.Raycast(Ground, Ball, out hit))
        {
            _distance = hit.distance;
            Debug.Log("Distance: " + _distance);
        }
    }
}
