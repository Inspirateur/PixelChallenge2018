using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]

public class LaserGenerator : Neon {

    public int height = 3;
    public int circle_nb = 1;
    public int angle = 60;
    private int angle_save = -1;
    private int circle_nb_save = -1;



    // Use this for initialization
    void Start () {
        Vector3 pos = transform.position
                    + Quaternion.AngleAxis(angle, Vector3.forward) * new Vector3(ComputeHeight(), 0, 0);
        Quaternion rot = Quaternion.LookRotation(Vector3.forward, pos - transform.position);

        this.transform.localScale = new Vector3(1, height, 1);
        this.transform.rotation = rot;
        this.transform.position = pos;
        
        
        
        
        
    }
	
	// Update is called once per frame
	void Update () {

        
            Vector3 pos = new Vector3(0, 0, 0)
                        + Quaternion.AngleAxis(angle, Vector3.forward) * new Vector3(ComputeHeight(), 0, 0);
            Quaternion rot = Quaternion.LookRotation(Vector3.forward, transform.position);

            //this.transform.localScale = new Vector3(1, height, 1);
            this.transform.rotation = rot;
            this.transform.position = pos;
        this.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            angle_save = angle;
            circle_nb_save = circle_nb;

    }

    virtual protected float ComputeHeight()
    {
        return 2.7f + (circle_nb - 1) * 4 - (float)height / 12f;
    }
}
