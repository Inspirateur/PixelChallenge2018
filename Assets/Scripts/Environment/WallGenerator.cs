using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class WallGenerator : Neon {
    public GameObject Object;
    public float length = 3;
    public float height = 3;
    public int circle_nb = 1;
    private float length_save = -1;
    private float height_save = -1;
    private int circle_nb_save = -1;
    // Use this for initialization

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (length_save != length || height_save != height || circle_nb_save != circle_nb)
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

        
            for (int i = (int)Object.transform.rotation.eulerAngles.z + (int)-length; i < (int)Object.transform.rotation.eulerAngles.z + length; i++)
            {
                Vector3 pos = transform.position
                    + Quaternion.AngleAxis(i , Vector3.forward) * new Vector3(3+(circle_nb-1)*4-height/12, 0, 0);
                Quaternion rot = Quaternion.LookRotation(Vector3.forward, pos - transform.position);
                GameObject obj = Instantiate(Object, pos, rot, transform);
                obj.transform.localScale = new Vector3(1, height, 1);
            }
            length_save = length;
            height_save = height;
            circle_nb_save = circle_nb;
        }
    }
}
