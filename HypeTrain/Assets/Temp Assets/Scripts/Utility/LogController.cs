using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogController : MonoBehaviour {

    //Flag for whether or not log messages for the given script should be output.
    [SerializeField] private bool loggingEnabled = false;

    //Basic message print, which appends "[SCRIPT_NAME.cs]" for easier trackdowns
    protected void Log(string message) {
        if (loggingEnabled) Debug.Log(message + " [" + GetComponent<MonoBehaviour>().GetType().Name + ".cs]");
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
