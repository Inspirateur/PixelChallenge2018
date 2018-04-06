using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterMovementData", menuName = "Character / Movement")]
public class CharacterMovementData : ScriptableObject
{
    public float Acceleration;
    public float MaxSpeed;
    public AnimationCurve AccelerationCurve;
}
