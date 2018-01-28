using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public AudioSource enterHole;
    public AudioSource[] deathAudio;
    public AudioSource[] heavyHitAudio;
    public AudioSource[] lightHitAudio;
    bool enterPlayed = false;
    void Start() {
        GetComponent<SpriteRenderer>().enabled = true;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Goal" && !SystemManager.Instance.GameComplete && !enterPlayed) {
            enterPlayed = true;
            SystemManager.Instance.WinGame();
            enterHole.Play();
        }
        if (other.tag == "KillSwitch" && !SystemManager.Instance.GameDeath) {
            Kill();
        }

    }
    void OnCollisionEnter2D(Collision2D coll) {
        if (!enterPlayed) {
            if (gameObject.GetComponent<Rigidbody2D>().velocity.magnitude > 18f) {
                heavyHitAudio[UnityEngine.Random.Range(0, heavyHitAudio.Length)].Play();
            } else
                lightHitAudio[UnityEngine.Random.Range(0, lightHitAudio.Length)].Play();
        }
    }
    void Kill() {
        int random = UnityEngine.Random.Range(0, deathAudio.Length);
        deathAudio[random].Play();
        SystemManager.Instance.KillPlayer();
        GetComponent<Collider2D>().enabled = false;
    }

    void Update() {

        if (GetComponent<Collider2D>().enabled && transform.position.x <= Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height * 0.5f)).x)
            Kill();
    }
}
