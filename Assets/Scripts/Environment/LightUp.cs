using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightUp : MonoBehaviour
{

    private SpriteRenderer sprite;

    private Color emissiveColor;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        emissiveColor = sprite.material.GetColor("_EmissionColor");
        SwitchOff();
    }

    public void SwitchOn()
    {
        sprite.material.SetColor("_EmissionColor", emissiveColor);
    }

    public void SwitchOff()
    {
        sprite.material.SetColor("_EmissionColor", new Color(0, 0, 0, 0));
    }
}
