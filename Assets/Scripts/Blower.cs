using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blower : MonoBehaviour {
    public GameObject ball;
    public Plane plane;
    public float maxDistance;
    public float maxForce;
    float transitionTimer;
    public ParticleSystem particles;
    public AudioSource blowLoop;
    public AudioSource[] blowStart;
    private float blowTimer = -1;
    private bool soundFadeOut = false;
    public float startSoundCooldown = -1;
    float transitionTime = 0.1f;
    float scale = 1f;
    TransitionMode mode = TransitionMode.Static;
    enum TransitionMode {
        Static,
        TransitionIn,
        TransitionOut
    }
    void Start() {
        plane = new Plane(Vector3.forward, Vector3.zero);
        GetComponent<SpriteRenderer>().enabled = false;
        scale = 0.5f;
        transform.localScale = new Vector3(scale, scale, 1);
    }
    void Update() {
        if (SystemManager.Instance.PopupsVisible()) return;
        if (mode != TransitionMode.Static) {
            transitionTimer += Time.deltaTime;
            if (mode == TransitionMode.TransitionIn) {
                GetComponent<SpriteRenderer>().enabled = true;
                scale = Mathf.Lerp(scale, 1f, transitionTimer / transitionTime);
                transform.localScale = new Vector3(scale, scale, 1);
                if (transitionTimer >= transitionTime)
                    mode = TransitionMode.Static;
            } else {
                scale = Mathf.Lerp(scale, 0.5f, transitionTimer / transitionTime);
                transform.localScale = new Vector3(scale, scale, 1);
                if (transitionTimer >= transitionTime) {
                    mode = TransitionMode.Static;
                    GetComponent<SpriteRenderer>().enabled = false;
                }
            }
        }
        if (startSoundCooldown >= 0) {
            startSoundCooldown += Time.deltaTime;
            if (startSoundCooldown >= 0.5f)
                startSoundCooldown = -1;
        }

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)) {
            if (!particles.isPlaying)
                particles.Play();
            if (Input.GetMouseButtonDown(0)) {
                if (startSoundCooldown < 0) {
                    blowStart[UnityEngine.Random.Range(0, blowStart.Length)].Play();
                    blowTimer = 0;
                    soundFadeOut = false;
                }
                blowLoop.Play();
            }

            blowTimer += Time.deltaTime;
            blowLoop.volume = Mathf.Lerp(blowLoop.volume, 1.0f, blowTimer / 0.5f);

            if (blowTimer >= 0.5f)
                blowTimer = -1;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float rayDistance;
            if (plane.Raycast(ray, out rayDistance)) {
                Vector3 diff = ball.transform.position - ray.GetPoint(rayDistance);
                var hit = Physics2D.Raycast(ray.GetPoint(rayDistance), diff, 1000f);
                if (mode != TransitionMode.TransitionIn) {
                    mode = TransitionMode.TransitionIn;
                    transitionTimer = 0;
                }
                transform.position = ray.GetPoint(rayDistance);
                transform.rotation = Quaternion.identity;
                transform.Rotate(0, 0, Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg - 90);
                if (hit.collider != null && hit.collider.tag == "Ball" || hit.collider.tag == "BlowThrough") {
                    var mag = diff.magnitude;
                    if (mag != 0) {
                        ball.GetComponent<Rigidbody2D>().AddForce(diff.normalized * Mathf.Clamp(maxDistance / mag, 0, maxForce));
                    }
                }
            }
        } else if (Input.GetMouseButtonUp(0)) {
            particles.Stop();
            transitionTimer = 0;
            mode = TransitionMode.TransitionOut;
            soundFadeOut = true;
            blowTimer = 0;
        }
        if (soundFadeOut) {
            blowTimer += Time.deltaTime;
            blowLoop.volume = Mathf.Lerp(blowLoop.volume, 0f, blowTimer / 0.5f);

            if (blowTimer >= 0.5f) {
                soundFadeOut = false;
                blowTimer = -1;
            }
        }
    }
}

