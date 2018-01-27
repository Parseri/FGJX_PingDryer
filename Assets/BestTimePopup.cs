using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestTimePopup : MonoBehaviour {
    public Text[] texts;

    void Start() {
        for (int i = 0; i < texts.Length; ++i) {
            texts[i].text = "" + (i + 1) + ". " + GetMinSec(SettingsManager.Instance.GetBestTime(i + 1));
        }
    }
    public string GetMinSec(float time) {
        string retVal = "";
        int minutes = (int)(time / 60f);
        if (minutes > 0) {
            if (minutes < 10)
                retVal += "0";
            retVal += minutes + ":";
        } else retVal += "00:";
        float seconds = time - minutes * 60;
        retVal += seconds.ToString("00.00");
        return retVal;
    }

    public void OnBackClicked() {
        gameObject.SetActive(false);
    }
}
