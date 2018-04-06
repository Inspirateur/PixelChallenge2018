﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CircleGenerator : MonoBehaviour {

    public GameObject Object;
    public int ObjectNbr = 10;
    public float ObjectDistance = 3;

    private int objectNbr = -1;
    private float objectDistance = -1;

	// Update is called once per frame
	void Update () {
		if (ObjectDistance != objectDistance || ObjectNbr != objectNbr)
        {
            LinkedList<Transform> children = new LinkedList<Transform>();
            foreach (Transform child in transform)
            {
                children.AddLast(child);
            }
            foreach (Transform child in children)
            {
                DestroyImmediate(child.gameObject);
            }

            for (int i = 0; i < ObjectNbr; i++)
            {
                Vector3 pos =  transform.position 
                    + Quaternion.AngleAxis(i * 360.0f / ObjectNbr, Vector3.forward) * new Vector3(ObjectDistance, 0, 0);
                Quaternion rot = Quaternion.LookRotation(Vector3.forward, pos - transform.position);
                Instantiate(Object, pos, rot, transform);
            }

            objectNbr = ObjectNbr;
            objectDistance = ObjectDistance;
        }
	}
}