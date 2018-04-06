using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "JumpData", menuName = "Character / Jump")]
public class JumpData : ScriptableObject {

    // The instantaneous jump acceleration
    public float JumpImpulseAcceleration = 40;

    // Time during which helding down jump button highten the jump
    public float JumpInputTime = 0.25f;
}
