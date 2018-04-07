using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightUp : MonoBehaviour
{

    private SpriteRenderer sprite;

    private Color emissiveColor;
    private bool on = false;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        emissiveColor = sprite.material.GetColor("_EmissionColor");
        SwitchOff();
    }

    private void Update()
    {
        if (on)
        {
            SwitchOn();
        }
        else
        {
            SwitchOff();
        }
    }

    public void SwitchOn()
    {
        on = true;
        emissiveColor = sprite.material.GetColor("_Color");
        sprite.material.SetColor("_EmissionColor", 0.7f * emissiveColor);
    }

    public void SwitchOff()
    {
        on = false;
        emissiveColor = sprite.material.GetColor("_Color");
        sprite.material.SetColor("_EmissionColor", 0.2f * emissiveColor);
    }
}
