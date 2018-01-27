using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLogic : MonoBehaviour {
    public Transform ballTransform;
    public Transform doomPiece;
    void Start() {
        if (ballTransform == null)
            ballTransform = GameObject.FindGameObjectWithTag("Ball").transform;
    }

    void Update() {
        if (ballTransform.position.x > transform.position.x)
            transform.position = new Vector3(ballTransform.position.x, transform.position.y, transform.position.z);
        var pos = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height*0.5f, 0));
        doomPiece.position = new Vector3(pos.x, doomPiece.position.y, doomPiece.position.z);
    }
}
