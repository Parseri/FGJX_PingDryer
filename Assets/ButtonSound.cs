using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour {
	void Start(){
		DontDestroyOnLoad(this);
	}
	public void OnButtonClicked(){
		GetComponent<AudioSource>().Play();
	}
}
