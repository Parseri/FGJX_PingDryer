using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBlinker : MonoBehaviour {
    public GameObject eyes;
    float eyesOpen = -1;
    float nextBlink = 0;
    void Start() {
        nextBlink = UnityEngine.Random.value * 15;
        eyes.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        nextBlink -= Time.deltaTime;
        if (nextBlink <= 0) {
			nextBlink = 5 + UnityEngine.Random.value * 15;
            eyes.SetActive(true);
            eyesOpen = 0;
        }
        if (eyesOpen >= 0) {
            eyesOpen += Time.deltaTime;
            if (eyesOpen >= 0.75f) {
                eyes.SetActive(false);
                eyesOpen = -1;
            }
        }
    }
}
