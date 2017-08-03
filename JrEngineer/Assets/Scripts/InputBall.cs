using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Dir {
    Left,
    Right,
}
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Rigidbody))]
public class InputBall : MonoBehaviour
{
    public float Impulse;
    public float ForceUp;
    private Rigidbody _rb;
    private Dir _direction;
    public Transform BallPos;

    public AudioClip kickClip;
    public AudioSource _source;

    public delegate void CollisionAction();
    public static event CollisionAction OnCollisionGround;

    public delegate void ClickAction();
    public static event ClickAction OnKickBall;
    public static InputBall Instance;

    private float _randomForce;
    public float MaxForce;
    public float MinForce;
    private int _randomDir;

    void Awake() {
        Instance = this;
        _source = GetComponent<AudioSource>();
    }
    // Use this for initialization
    void Start () {
        _rb=GetComponent<Rigidbody>();     
	}
    public void RestartBallPosition() {
        transform.position=BallPos.transform.position;
    }	
   public void AddForceBall() {
        if (!ManagerScript.GameIsOver && ManagerScript.CanKick) {
            if (!ManagerScript.HasStart) {
                ManagerScript.HasStart = true;
            }
            AddRandomForce();
            if (OnKickBall != null)
                OnKickBall();
        } else if (!ManagerScript.CanKick) {
            OnCollisionGround();
        }    
    }
    void AddRandomForce() {
        _randomForce = Random.Range(MinForce, MaxForce);
        _randomDir = Random.Range(0, 2);      
        _direction = _randomDir!=0 ? Dir.Left : Dir.Right;

        if (_direction == Dir.Left) {
            _rb.AddForce(new Vector3(-_randomForce, 1, 0) * Impulse,ForceMode.Impulse);
        }
        if (_direction == Dir.Right) {
            _rb.AddForce(new Vector3(_randomForce, 1, 0) * Impulse, ForceMode.Impulse);
        }
    }
    
    void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Ground")) {
            if (ManagerScript.HasStart) {
                if (OnCollisionGround != null)
                    OnCollisionGround();
            }
        }
    }

}
