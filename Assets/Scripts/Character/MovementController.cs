using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {
    bool onDash;

	void Start () {
        onDash = false;
	}
	
	void Update () {
        if (!onDash) {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                onDash = true;
            }
        }
    }
}
