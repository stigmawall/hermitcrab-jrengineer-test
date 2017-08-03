using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class ManagerScript : MonoBehaviour {
    public static bool HasStart;
    public static bool GameIsOver;
    public static bool CanKick;
    public static ManagerScript Instance;
    private int _score;
    public Text ScoreTxt;
    public Animator GameOverAnim;
    private Vector3 _mousePosClick;
    public Dir Direction;
    public string MainMenuName;
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
        Instance = this;
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
        CameraScript.Instance.ResetCameraPos();
    }	
    void GameOver() {
        GameIsOver = true;
        HasStart = false;
        InputBall.Instance.RestartBallPosition();
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

    public void ManiMenu() {
        SceneManager.LoadScene(MainMenuName, LoadSceneMode.Single);
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

    void InputManager() {
        if (Input.GetMouseButtonDown(0) && !GameIsOver)  {
            InputBall.Instance.ChooseDirection();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray))
            {
                _mousePosClick = ray.direction;
                if (_mousePosClick.x < 0)
                {
                    Direction = Dir.Left;
                }
                else if (_mousePosClick.x > 0)
                {
                    Direction = Dir.Right;
                }              
            }
            if (!HasStart) {
                InputBall.Instance.AddForceBall();
            }
            else
            {
                if (Direction == InputBall.Instance.LastDirection && CanKick)
                {
                    InputBall.Instance.AddForceBall();
                }
                else {
                    Debug.Log("Else cond");
                    Debug.Log("Click  nexxt dir: " + Direction);
                    Debug.Log("Ball dir: " + InputBall.Instance.Direction);
                    Debug.Log("Can? " + CanKick);
                    GameOver();
                }
            }               
        }
    }

    void Update() {
        InputManager();
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
