using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleExplosion : MonoBehaviour {

    public CircleGenerator Circle;
    public CircleGenerator Explosions;

    private void Start()
    {
        Explosions.gameObject.SetActive(false);
    }

    public void Explode()
    {
        Explosions.gameObject.SetActive(true);
        Circle.gameObject.SetActive(false);
    }
}
