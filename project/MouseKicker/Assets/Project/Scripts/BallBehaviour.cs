using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.transform.tag);

        if(collision.transform.tag == "Floor")
        {
            if(GameManager.Instance.kickStarted)
            {
                GameManager.Instance.currentGameState = GameState.End;
            }
        }
    }
}
