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
    private Dir _lastDir;
    public Dir LastDirection {
        get { return _lastDir; }
    }
    public Dir Direction {
        get { return _direction; }       
    }
    public Transform BallPos;
    public AudioClip kickClip;
    public AudioSource _source;

    public delegate void CollisionAction();
    public static event CollisionAction OnCollisionGround;

    public delegate void ClickAction();
    public static event ClickAction OnKickBall;
    public static InputBall Instance;

    private float _randomForce;
    private float _randomForceUp;
    public float MaxForce;
    public float MinForce;
    private int _randomDir;

    void Awake() {
        Instance = this;
        _rb = GetComponent<Rigidbody>();
        _source = GetComponent<AudioSource>();
        BallPos.transform.position = transform.position;
        BallPos.transform.rotation = transform.rotation;
    }
    // Use this for initialization
    void Start () {
        ChooseDirection();   
	}
   
    public void RestartBallPosition() {
        _rb.velocity = Vector3.zero;
        transform.position=BallPos.transform.position;
        transform.rotation = BallPos.transform.rotation;
    }	
   public void AddForceBall() {
        _source.PlayOneShot(kickClip);
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
   public void ChooseDirection() {
        _randomForce = Random.Range(MinForce, MaxForce);
        _randomForceUp = Random.Range(MinForce, MaxForce);
        _randomDir = Random.Range(0, 2);
        _direction = _randomDir != 0 ? Dir.Left : Dir.Right;
    }
    void AddRandomForce() {
        ChooseDirection();
        if (_direction != _lastDir)
        {
            _randomForce *= 2;
        } 
        if (_direction == Dir.Left) {
            _rb.AddForce(new Vector3(-_randomForce, _randomForceUp, 0) * Impulse,ForceMode.Impulse);
        }
        if (_direction == Dir.Right) {
            _rb.AddForce(new Vector3(_randomForce, _randomForceUp, 0) * Impulse, ForceMode.Impulse);
        }
        _lastDir = _direction;
    }
    
    void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Ground")) {
            _rb.velocity = Vector3.zero;
            if (ManagerScript.HasStart) {
                if (OnCollisionGround != null)
                    OnCollisionGround();
            }
        }
    }
}
