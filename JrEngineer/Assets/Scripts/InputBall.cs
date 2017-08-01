using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputBall :MonoBehaviour
{
    public float Impulse;
    private Rigidbody _rb;

    public delegate void CollisionAction();
    public static event CollisionAction OnCollisionGround;

    public delegate void ClickAction();
    public static event ClickAction OnKickBall;

    private int randomForce;

    // Use this for initialization
    void Start () {
        _rb=GetComponent<Rigidbody>();
	}
	
    void OnMouseDown() {
        if (!ManagerScript.GameIsOver) {
            if (!ManagerScript.HasStart)
            {
                ManagerScript.HasStart = true;
            }
            _rb.AddForce(transform.up * Impulse);
            if (OnKickBall != null)
                OnKickBall();
        }       
    }

    void OnMouseEnter() {
    }

    void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Ground")) {
            if (ManagerScript.HasStart) {

                Debug.Log("GameOver");
                if (OnCollisionGround != null)
                    OnCollisionGround();

            }
        }
    }
}
