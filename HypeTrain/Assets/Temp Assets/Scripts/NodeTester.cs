﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NodeTester : MonoBehaviour {
	public bool test = false;
	// Use this for initialization
	void Start () {
	
	}


	// Update is called once per frame
	void Update () {
		if (test) {
			test = false;
			List<Collider2D> hits = GetComponent<path_node>().neighbors();
			foreach(Collider2D hit in hits) {
				Debug.Log (hit.name);
			}
		}
	}
}