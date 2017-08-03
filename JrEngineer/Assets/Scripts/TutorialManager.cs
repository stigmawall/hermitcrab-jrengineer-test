using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {
    public GameObject LeftFeedback;
    public GameObject RightFeedback;

    public GameObject StartFeedback;
	// Use this for initialization
	void Start () {
        TurnOnStart();
    }
    void OnEnable()
    {
        InputBall.OnCollisionGround += TurnOnStart;
        InputBall.OnKickBall += TurnOffStart;
        InputBall.OnKickBall += OnCheck;
    }
    void OnDisable()
    {
        InputBall.OnCollisionGround -= TurnOnStart;
        InputBall.OnKickBall -= TurnOffStart;
        InputBall.OnKickBall -= OnCheck;
    }
    void TurnOffStart() {
        if(!StartFeedback.activeSelf)
        StartFeedback.SetActive(false);
    }
    void TurnOnStart()
    {
        StartFeedback.SetActive(true);
        LeftFeedback.SetActive(false);
        RightFeedback.SetActive(false);
    }
    public void ActiveFeedbackRight()
    {
        StartFeedback.SetActive(false);
        LeftFeedback.SetActive(false);
        RightFeedback.SetActive(true);
    }
    public void ActiveFeedbackLeft()
    {
        StartFeedback.SetActive(false);
        LeftFeedback.SetActive(true);
        RightFeedback.SetActive(false);
    }

    void OnCheck() {
        if (InputBall.Instance.Direction == Dir.Left) {
            ActiveFeedbackLeft();
        } else if(InputBall.Instance.Direction == Dir.Right) {
            ActiveFeedbackRight();
        }
    } 
}
