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
    private float deathTimer = 1.0f;
    private float deathTime;
    private float continueTimer;
    public Text livesText;
    private bool showContinue;
    public Text timerText;
    public GameObject deathParticlePrefab;
    private GameObject deathParticle;
    void Start() {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        livesText.text = "Lives: " + SettingsManager.Instance.Lives;
        gameDeath = false;
        gameComplete = false;
        showContinue = false;
    }

    public void WinGame() {
        gameComplete = true;
    }

    public void KillPlayer() {
        SettingsManager.Instance.AddDeath();
        deathParticle = Instantiate(deathParticlePrefab, ball.transform.position, Quaternion.identity);
        ball.SetActive(false);
        if (SettingsManager.Instance.Lives <= 0) {
            ShowContinue();
        } else {
            gameDeath = true;
        }
    }
    void ShowGameOver() {
        SceneManager.LoadScene("MainMenu");
    }

    void ShowContinue() {
        livesText.text = "Lives: " + SettingsManager.Instance.Lives;
        showContinue = true;
        continuePopup.SetActive(true);
        continueTimer = 10;
    }
    public void ContinueClicked(){
        SettingsManager.Instance.ContinueGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void Update() {
        if (showContinue) {
            continueTimer-=Time.deltaTime;
            timerText.text = "" + Mathf.Ceil(continueTimer);
            if (continueTimer <= 0) {
                continuePopup.SetActive(false);
                ShowGameOver();
            }
        } else if (ball != null) {
            if (gameComplete) {
                ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            } else if (gameDeath) {
                ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                deathTime+=Time.deltaTime;
                if (deathTime > deathTimer){
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
        }
    }
}