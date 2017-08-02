using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    static public InputManager Instance;

    private void OnEnable()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {

        //Entered Kick
#if UNITY_EDITOR_WIN || UNITY_STANDALONE || UNITY_WEBGL
        if (Input.GetMouseButton(0))
        {
            //If hold the mouse fire button it adds the kickPower
            if (GameManager.Instance.kickPower < GameManager.Instance.maxKickPower)
            {
                GameManager.Instance.kickPower += Time.deltaTime;
            }
        }
#endif
        //The android version has a bug that raycasts are not hitting on objects on movement... =(
#if UNITY_ANDROID && !UNITY_EDITOR
        Debug.Log("Android");
        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            //If hold the mouse fire button it adds the kickPower
            if (GameManager.Instance.kickPower < GameManager.Instance.maxKickPower)
            {
                GameManager.Instance.kickPower += Time.deltaTime;
            }
        }

#endif

        //Kicked
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                //If hits the ball and is inside the kixkable area does the kick
                if (hit.transform.tag == "Ball" && Input.mousePosition.y <= GameManager.Instance.kickAreaHeight)
                {
                    //Count a kick if not on menu
                    if (GameManager.Instance.currentGameState == GameState.Game)
                    {
                        if (GameManager.Instance.kickStarted)
                        {
                            GameManager.Instance.kickCount++;
                        }
                        else
                        {
                            GameManager.Instance.kickCount++;
                            GameManager.Instance.kickStarted = true;
                        }
                    }

                    Vector3 direction = GameManager.Instance.ball.transform.up;

                    //Gets the distance for positioning the kick on the right side of the ball
                    float distance = GameManager.Instance.ball.transform.position.x - hit.point.x;

                    Vector3 forcePosition = new Vector3(GameManager.Instance.ball.transform.position.x - distance, GameManager.Instance.ball.transform.position.y - 0.5f, GameManager.Instance.ball.transform.position.z - (Random.Range(-0.3f, 0.3f)));

                    //Used explosion because of its nature that simulate an impact on the ball... AddForce and AddForceAtPosition works too
                    GameManager.Instance.ball.GetComponent<Rigidbody>().AddExplosionForce(GameManager.Instance.kickPower * 1000, forcePosition, 5);
                }

            }

            //Resets the kick power
            GameManager.Instance.kickPower = GameManager.Instance.minKickPower;
        }
    }
}
