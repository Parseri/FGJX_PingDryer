using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blower : MonoBehaviour {
    public GameObject ball;
    public Plane plane;
    public float maxDistance;
    public float maxForce;
    void Start() {
        plane = new Plane(Vector3.forward, Vector3.zero);
    }
    void Update() {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float rayDistance;
            if (plane.Raycast(ray, out rayDistance)) {
                Vector3 diff = ball.transform.position - ray.GetPoint(rayDistance);
                var mag = diff.magnitude;
                if (mag != 0) {
                    ball.GetComponent<Rigidbody2D>().AddForce(diff.normalized * Mathf.Clamp(maxDistance / mag, 0, maxForce));
                    Debug.Log("diff: " + diff + ", mag: " + mag + ", force: " + (diff / mag));
                }
            }
        }
    }
}
