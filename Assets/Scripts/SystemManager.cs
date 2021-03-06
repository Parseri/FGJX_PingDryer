﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SystemManager : MonoBehaviour {
    private static SystemManager instance = null;
    public static SystemManager Instance { get { return instance; } }
    bool gameComplete = false;
    public bool GameComplete { get { return gameComplete; } }
    bool gameDeath = false;
    public bool GameDeath { get { return gameDeath; } }
    public GameObject ball;
    public GameObject continuePopup;
    public AudioSource completePopupAudio;
    public Text speedRunTime;
    public Text bestRunTime;
    private float deathTimer = 1.0f;
    private float deathTime;
    private float continueTimer;
    public Text livesText;
    private bool showContinue;
    private float startTime = -1;
    public Text timerText;
    public GameObject deathParticlePrefab;
    private GameObject deathParticle;
    private float gameCompleteTimer = -1;
    private bool newHighScore;
    public GameObject highScoreStamp;
    private float gameEndScale = 1f;

    public GameObject completePopup;
    private bool stopTimer = false;
    private bool timeStored = false;
    private float finalTime;
    void Start() {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        if (ball == null)
            ball = GameObject.FindGameObjectWithTag("Ball");
        livesText.text = "X " + SettingsManager.Instance.Lives;
        gameDeath = false;
        gameComplete = false;
        showContinue = false;
        startTime = -1;
        bestRunTime.text = "Best: " + GetMinSec(SettingsManager.Instance.GetBestTime(SceneManager.GetActiveScene().buildIndex));
    }

    public void WinGame() {
        gameComplete = true;
        stopTimer = true;
    }

    public void KillPlayer() {
        stopTimer = true;
        SettingsManager.Instance.AddDeath();
        deathParticle = Instantiate(deathParticlePrefab, ball.transform.position, Quaternion.identity);
        ball.GetComponent<SpriteRenderer>().enabled = false;
        if (SettingsManager.Instance.Lives <= 0) {
            gameDeath = true;
            ShowContinue();
        } else {
            gameDeath = true;
        }
    }
    void ShowGameOver() {
        SceneManager.LoadScene("MainMenu");
    }

    void ShowContinue() {
        livesText.text = "X " + SettingsManager.Instance.Lives;
        showContinue = true;
        continuePopup.SetActive(true);
        continueTimer = 10;
    }
    public void ContinueClicked() {
        SettingsManager.Instance.ContinueGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ExitClicked() {
        SettingsManager.Instance.ContinueGame();
        SceneManager.LoadScene("MainMenu");
    }
    public void RestartClicked() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void NextLevelClicked() {
        if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        else
            SceneManager.LoadScene("MainMenu");
        completePopup.SetActive(false);
    }
    void ShowLevelCompletePopup() {
        completePopup.SetActive(true);
        completePopupAudio.Play();
        if (newHighScore) {
            highScoreStamp.SetActive(true);
        }
    }

    public bool PopupsVisible() {
        return completePopup.activeSelf || continuePopup.activeSelf;
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
    void Update() {
        if (startTime < 0)
            startTime = Time.realtimeSinceStartup;
        else if (!stopTimer) {

            finalTime = Time.realtimeSinceStartup - startTime;
            speedRunTime.text = "Time: " + GetMinSec(finalTime);
        } else if (!timeStored) {
            timeStored = true;
            if (!gameDeath)
                newHighScore = SettingsManager.Instance.PostTime(SceneManager.GetActiveScene().buildIndex, finalTime);
        }
        if (gameCompleteTimer >= 0) {
            gameCompleteTimer += Time.deltaTime;
            if (gameCompleteTimer <= 0.5f) {
                gameEndScale = Mathf.Lerp(gameEndScale, 0, gameCompleteTimer / 0.5f);
                ball.transform.localScale = new Vector3(gameEndScale, gameEndScale, 1);
            } else {
                gameCompleteTimer = -1;
                ShowLevelCompletePopup();
            }
        } else if (showContinue) {
            continueTimer -= Time.deltaTime;
            timerText.text = "" + Mathf.Ceil(continueTimer);
            if (continueTimer <= 0) {
                continuePopup.SetActive(false);
                ShowGameOver();
            }
        } else if (ball != null) {
            if (gameComplete) {
                ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                gameCompleteTimer = 0;
                gameComplete = false;
            } else if (gameDeath) {
                ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                deathTime += Time.deltaTime;
                if (deathTime > deathTimer) {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
        }
    }
}