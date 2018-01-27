using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsManager {
    [Serializable]
    public class SaveItem {
        [SerializeField]
        public int lastOpenLevel;
        [SerializeField]
        public List<BestTime> bestTimes;

        public SaveItem() {
            lastOpenLevel = 0;
            bestTimes = new List<BestTime>();
        }
    }
    [Serializable]
    public class BestTime {
        [SerializeField]
        public int level;
        [SerializeField]
        public float time;

        public BestTime(int lev, float t){
            level = lev;
            time = t;
        }
    }
    private static SettingsManager instance = null;
    public static SettingsManager Instance {
        get {
            if (instance == null)
                instance = new SettingsManager();
            return instance;
        }
    }

    string savePath = Application.persistentDataPath + "/save.json";
    int lives;
    public int Lives { get { return lives; } }
    int lastOpenLevel;
    List<BestTime> bestTimes;
    public SettingsManager() {
        InitializeGame();
        Application.targetFrameRate = 60;
    }
    public void InitializeGame() {
        lives = 3;
        SaveItem item = new SaveItem();
        bestTimes = new List<BestTime>();
        try {
            item = JsonUtility.FromJson<SaveItem>(File.ReadAllText(savePath));
        } catch (Exception) {
            Debug.Log("save file parsing failed");
            SaveItems();
        }
        bestTimes = item.bestTimes;
        lastOpenLevel = item.lastOpenLevel;
        
    }
    private void SaveItems(){
        SaveItem item = new SaveItem();
        item.lastOpenLevel = lastOpenLevel;
        item.bestTimes = bestTimes;
        File.WriteAllText(savePath, JsonUtility.ToJson(item, true));
    }
    public bool PostTime(int level, float time) {
        foreach (var best in bestTimes) {
            if (best.level == level){
                if (time < best.time){
                    best.time = time;
                    SaveItems();
                    return true;
                }
                return false;
            }
        }
        bestTimes.Add(new BestTime(level, time));
        SaveItems();
        return true;
    }
    public float GetBestTime(int level) {
        foreach (var best in bestTimes) {
            if (best.level == level)
                return best.time;
        }
        return 0;
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
