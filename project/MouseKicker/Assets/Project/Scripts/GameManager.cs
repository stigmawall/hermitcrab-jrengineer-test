using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Menu,
    Game,
    End
}

public class GameManager : MonoBehaviour
{

    static public GameManager Instance;

    [Header("Extra Game Vars")]
    public GameState currentGameState = GameState.Menu;

    [Header("Game Vars")]
    public int kickCount;
    public float maxKickPower = 3, minKickPower = 0.5f;
    public float kickPower = 0.5f;
    public float maxKickBallDistance = 1;
    public float kickAreaHeight = 390;
    public float minKickAreaHeight = 100;
    public bool canShrinkKickArea = false;
    public bool kickStarted = false;

    [Header("GO References")]
    public GameObject ball;

    private void OnEnable()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start()
    {
        kickAreaHeight = Screen.height * 0.45f;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentGameState)
        {
            case GameState.Game:

                if (kickCount % 10 == 0 && canShrinkKickArea)
                {
                    if (kickAreaHeight - 10 >= minKickAreaHeight)
                    {
                        kickAreaHeight -= 10;
                        canShrinkKickArea = false;
                    }
                }

                if (kickCount % 10 == 9)
                {
                    canShrinkKickArea = true;
                }

                break;
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void RestartGame()
    {
        kickCount = 0;
        kickStarted = false;
        ball.transform.position = new Vector3(0,5,0);
        ball.transform.rotation = Quaternion.identity;
        ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        kickAreaHeight = 340;
        currentGameState = GameState.Game;
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}