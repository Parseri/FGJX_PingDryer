using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    void Start() {
        GetComponent<SpriteRenderer>().enabled = true;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Goal" && !SystemManager.Instance.GameComplete)
            SystemManager.Instance.WinGame();
        if (other.tag == "KillSwitch" && !SystemManager.Instance.GameDeath) {
            Kill();
        }

    }

    void Kill() {
        SystemManager.Instance.KillPlayer();
    }
    
    void Update() {
        if (transform.position.x <= Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height * 0.5f)).x)
            Kill();
    }
}
