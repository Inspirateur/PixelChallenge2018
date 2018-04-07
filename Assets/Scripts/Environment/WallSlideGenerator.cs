using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class WallSlideGenerator : Neon
{
    public GameObject Object;
    [Range(1, 50)]
    public int length = 3;
    [Range(1, 5)]
    public int height = 3;
    public int circle_nb = 1;
    [Range (0,360)]
    public int angle = 60;

    private float length_save = -1;
    private float height_save = -1;
    private int circle_nb_save = -1;
    private int angle_save = -1;

    // Use this for initialization

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (length_save != length || height_save != height || circle_nb_save != circle_nb || angle_save != angle)
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


            for (int i = angle + (int)-length; i < angle + length; i++)
            {
                Vector3 pos = transform.position
                    + Quaternion.AngleAxis(i, Vector3.forward) * new Vector3(3 + (circle_nb - 1) * 4 - (float)height / 12f -0.75F, 0, 0);
                Quaternion rot = Quaternion.LookRotation(Vector3.forward, pos - transform.position);
                GameObject obj = Instantiate(Object, pos, rot, transform);
                obj.transform.localScale = new Vector3(1, height, 1);
            }
            length_save = length;
            height_save = height;
            circle_nb_save = circle_nb;
            angle_save = angle;
        }
    }
}
