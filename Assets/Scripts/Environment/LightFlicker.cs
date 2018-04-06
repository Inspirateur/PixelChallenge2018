using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour {

    public SpriteRenderer LightSprite;

    public LightFlickerData Data;

    private void Start()
    {
        SwitchOn();
    }

    private void SwitchOn()
    {
        LightSprite.color = new Color(1, 1, 1, 1);
        if (Random.Range(0.0f,1.0f) <= Data.FlickerProbability)
        {
            Invoke("SwitchOff", Data.FlickerFrequency);
        } else
        {
            Invoke("SwitchOn", Data.FlickerFrequency);
        }
    }

    private void SwitchOff() {
        LightSprite.color = new Color(0, 0, 0, 0);
        Invoke("SwitchOn", Data.OffTime);
    }
}
