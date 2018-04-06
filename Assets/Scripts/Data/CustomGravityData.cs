using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CustomGravityData", menuName = "Environment / Gravity")]
public class CustomGravityData : ScriptableObject
{
    public float Force = 9.81f;
    public Vector2 Center = Vector2.zero;
    public bool IsAttraction = false;
    public bool IsRotatedTowardsCenter = false;
}
