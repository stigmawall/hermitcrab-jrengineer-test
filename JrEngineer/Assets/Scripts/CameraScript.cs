using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    public Transform Target;
    public static CameraScript Instance;
    public Transform myInitPos;
    public float speed;
    private float _distanceBetweenBall;
	public float FollowDistance;
    public float Velocity;
    // Update is called once per frame
    void Awake() {
        Instance = this;
        myInitPos = GetComponent<Transform>();
        myInitPos.transform.position = transform.position;
        myInitPos.transform.rotation = transform.rotation;
    }
    public void ResetCameraPos() {
        transform.position = myInitPos.transform.position;
        transform.rotation = myInitPos.transform.rotation;
    }
}
