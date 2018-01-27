using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour {
    private static SettingsManager instance = null;
    public static SettingsManager Instance { get { return instance; } }
    int lives;
    public int Lives { get { return lives; } }

    public void StartGame(){
        SceneManager.LoadScene(1);
    }
    public void InitializeGame() {
        lives = 3;
    }
    public void AddDeath() {
        lives--;
    }
    public void AddExtraLife() {
        lives++;
    }
    void Start() {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
		DontDestroyOnLoad(this);
        InitializeGame();
    }
	public void ContinueGame(){
		lives = 3;
	}
    void Update() {

    }
}
