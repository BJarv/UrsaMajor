using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogController : MonoBehaviour {

    [SerializeField]
    private bool loggingEnabled = false;

    protected void Log(string message) {
        if (loggingEnabled) Debug.Log(message);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
