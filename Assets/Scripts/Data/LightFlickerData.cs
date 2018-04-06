using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LightFlickerData", menuName = "Environment / LightFlicker")]
public class LightFlickerData : ScriptableObject {

    public float FlickerProbability = 1;
    public float FlickerFrequency = 1;

    // Must be less than FlickerFrequency
    public float OffTime = 0.1f;
}
