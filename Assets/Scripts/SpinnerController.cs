using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(DoTweenPositionHelper))]
public class SpinnerController : MonoBehaviour {
    [SerializeField]
    float BladeMoveSpeed = 15f;
    [SerializeField]
    float BladeRotateSpeed = 15f;
    [SerializeField]
    private PathTool wayPoints;
    [SerializeField]
    int wayPointCount = 0;
    private Transform MovetoPos;
    bool move = false;

    void Start() {
		Debug.Log("wayPoints.nodes: " + wayPoints.nodes.Count); 
        transform.position = wayPoints.nodes[0].position;
        onMoveTo();
    }
	void Update(){
		transform.Rotate(0,0,BladeRotateSpeed);
	}

    public void onMoveTo() {
        move = true;
        wayPointCount++;
        if (wayPointCount >= wayPoints.nodes.Count) wayPointCount = 0;
        MovetoPos = wayPoints.nodes[wayPointCount];
        this.GetComponent<DoTweenPositionHelper>().onTweenTo(MovetoPos);
    }
}
