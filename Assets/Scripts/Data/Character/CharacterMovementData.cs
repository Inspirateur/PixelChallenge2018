using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterMovementData", menuName = "Character / Movement")]
public class CharacterMovementData : ScriptableObject
{
    public float Acceleration = 0.1f;
    public float MaxSpeed = 2f;

    // The instantaneous jump acceleration
    public float JumpImpulseAcceleration = 40;

    // Time during which helding down jump button highten the jump
    public float JumpInputTime = 0.25f;

    public float JumpVelocityLoss = 0.1f;
}
