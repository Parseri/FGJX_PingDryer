using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsManager {
    private static SettingsManager instance = null;
    public static SettingsManager Instance {
        get {
            if (instance == null)
                instance = new SettingsManager();
            return instance;
        }
    }
    int lives;
    public int Lives { get { return lives; } }
    public SettingsManager() {
        InitializeGame();
        Application.targetFrameRate = 60;
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
        InitializeGame();
    }
    public void ContinueGame() {
        lives = 3;
    }
}
