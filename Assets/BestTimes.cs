using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BestTimes : MonoBehaviour {
    public GameObject bestScores;

    public void OnBestClicked() {
		bestScores.SetActive(true);
    }

}
