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
        GetComponent<SpriteRenderer>().enabled = false;
    }
    void Update() {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)) {
            GetComponent<SpriteRenderer>().enabled = true;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float rayDistance;
            if (plane.Raycast(ray, out rayDistance)) {
                Vector3 diff = ball.transform.position - ray.GetPoint(rayDistance);
                transform.position = ray.GetPoint(rayDistance);
                transform.rotation = Quaternion.identity;
                transform.Rotate(0,0,Mathf.Atan2(diff.y, diff.x)*Mathf.Rad2Deg-90);
                var mag = diff.magnitude;
                if (mag != 0) {
                    ball.GetComponent<Rigidbody2D>().AddForce(diff.normalized * Mathf.Clamp(maxDistance / mag, 0, maxForce));
                    Debug.Log("diff: " + diff + ", mag: " + mag + ", force: " + (diff / mag));
                }
            }
        }
        else
            GetComponent<SpriteRenderer>().enabled = false;
    }
}
