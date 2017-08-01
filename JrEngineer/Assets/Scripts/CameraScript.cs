using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    public Transform Target;
    private float _distanceBetweenBall;
	public float FollowDistance;
    public float Velocity;
    // Update is called once per frame
   
	void Update () {
        transform.LookAt(Target);
        _distanceBetweenBall = Vector3.Magnitude(Target.transform.position - transform.position);

        if (_distanceBetweenBall > FollowDistance)
        {
            transform.Translate(new Vector3(0,0,Velocity) *Time.deltaTime);
        }

    }
}
