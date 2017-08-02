using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    static public UIManager Instance;

    [Header("General UI Vars")]
    public GameObject GameUI;
    public GameObject EndUI;

    [Header("Feedback UI Vars")]
    public Text kickCountText;
    public RectTransform kickAreaImage;
    public RectTransform kickPowerFeedback;
    public float kickPowerFeedbackCircleDiameter = 2;
    public float kickPowerFeedbackSpeed;

    [Header("End Screen Vars")]
    public Text scoreText;

    private void OnEnable()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch (GameManager.Instance.currentGameState)
        {
            case GameState.Game:

                if (EndUI.activeSelf)
                    EndUI.SetActive(false);

                if (!GameUI.activeSelf)
                    GameUI.SetActive(true);

                break;
            case GameState.End:

                GameUI.SetActive(false);
                scoreText.text = "Your Score: \n" + GameManager.Instance.kickCount;
                EndUI.SetActive(true);

                break;
        }

        kickCountText.text = GameManager.Instance.kickCount.ToString();
        kickAreaImage.sizeDelta = new Vector2(kickAreaImage.sizeDelta.x, GameManager.Instance.kickAreaHeight);

        kickPowerFeedback.position = Input.mousePosition;
        kickPowerFeedback.sizeDelta = Vector2.Lerp(kickPowerFeedback.sizeDelta, new Vector2((GameManager.Instance.kickPower * 20) + kickPowerFeedbackCircleDiameter, (GameManager.Instance.kickPower * 20) + kickPowerFeedbackCircleDiameter), kickPowerFeedbackSpeed * Time.deltaTime);
    }
}
