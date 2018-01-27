using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{


    void Start()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Goal" && !SystemManager.Instance.GameComplete)
           	SystemManager.Instance.WinGame();
		if (other.tag == "KillSwitch" && !SystemManager.Instance.GameDeath)
           	SystemManager.Instance.KillPlayer();

    }
    void Update()
    {

    }
}
