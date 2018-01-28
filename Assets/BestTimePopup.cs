using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestTimePopup : MonoBehaviour {
    public Text[] texts;
    public GameObject nextPage;
    public GameObject prevPage;

    public int page = 0;

    void Start() {
        nextPage.SetActive(true);
        prevPage.SetActive(false);
        for (int i = 0; i < texts.Length; ++i) {
            texts[i].text = "" + (page*4 + i + 1) + ". " + GetMinSec(SettingsManager.Instance.GetBestTime(page*4 + i + 1));
        }
    }

    public void RefreshPage() {
        page = page == 0?1:0;
        nextPage.SetActive(page == 0);
        prevPage.SetActive(page != 0);
        for (int i = 0; i < texts.Length; ++i) {
            texts[i].text = "" + (page*4 + i + 1) + ". " + GetMinSec(SettingsManager.Instance.GetBestTime(page*4 + i + 1));
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
