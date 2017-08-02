using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceManager : MonoBehaviour {
    public float MaxDistance;
    public float MinDistance;
    private float _distance;
    public bool isInRange;
    public static DistanceManager Instance;
    RaycastHit hit;
    void Awake() {
        Instance = this;
    }
    void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out hit)) {
            if (hit.collider.CompareTag("Ground")) {
                _distance = hit.distance;
            }           
        }
        isInRange= _distance <= MaxDistance && _distance >=MinDistance ? true : false;
    }

}
