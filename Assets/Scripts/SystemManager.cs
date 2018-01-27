using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemManager : MonoBehaviour
{
    private static SystemManager instance = null;
    public static SystemManager Instance { get { return instance; } }
    bool gameComplete = false;
    public bool GameComplete { get { return gameComplete; } }
	bool gameDeath = false;
    public bool GameDeath { get { return gameDeath; } }
    void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void WinGame()
    {
        gameComplete = true;
    }

	public void KillPlayer(){
		gameDeath = true;
	}

    void Update()
    {
        if (gameComplete && Time.timeScale > 0)
        {
            Time.timeScale *= 0.9f;
			if (Time.timeScale <= 0.05f)
				Time.timeScale = 0;
        }

		if (gameDeath && Time.timeScale > 0)
        {
            Time.timeScale *= 0.5f;
			if (Time.timeScale <= 0.05f)
				Time.timeScale = 0;
        }
    }
}
