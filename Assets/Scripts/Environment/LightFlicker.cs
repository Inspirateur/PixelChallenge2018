using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{

    private SpriteRenderer sprite;

    public LightFlickerData Data;

    private Color emissiveColor;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        emissiveColor = sprite.material.GetColor("_EmissionColor");
        SwitchOff();
    }

    public void forceOff()
    {
        CancelInvoke();
        SwitchOff();
    }

    private void SwitchOn()
    {
        sprite.material.SetColor("_EmissionColor", emissiveColor);
        //sprite.color = new Color(1, 1, 1, 1);
        if (Random.Range(0.0f, 1.0f) < Data.FlickerProbability)
        {
            Invoke("SwitchOff", Data.FlickerFrequency);
        }
        else
        {
            Invoke("SwitchOn", Data.FlickerFrequency);
        }
    }

    private void SwitchOff()
    {
        //sprite.color = new Color(0, 0, 0, 0);

        sprite.material.SetColor("_EmissionColor", new Color(0, 0, 0, 0));
        Invoke("SwitchOn", Data.OffTime);
    }
}
