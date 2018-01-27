using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLogic : MonoBehaviour {
    public Transform ballTransform;
    void Start() {

    }

    void Update() {
        if (ballTransform.position.x > transform.position.x)
            transform.position = new Vector3(ballTransform.position.x, transform.position.y, transform.position.z);
    }
}
