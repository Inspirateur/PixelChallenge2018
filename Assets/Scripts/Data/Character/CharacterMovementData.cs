using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterMovementData", menuName = "Character / Movement")]
public class CharacterMovementData : ScriptableObject
{
    public float Acceleration = 0.1f;
    public float MaxSpeed = 2f;

    // The instantaneous jump acceleration
    public float JumpImpulseAcceleration = 10;

    // The jump button duration
    public float JumpButtonDuration = 0.5f;

    public AnimationCurve JumpVelocityLossCurve;

    // The slide duration
    public float SlideMaxDuration = 1f;

    public AnimationCurve AccelerationFactorOverSpeed;
}
