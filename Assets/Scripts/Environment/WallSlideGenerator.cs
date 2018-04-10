using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class WallSlideGenerator : WallGenerator
{
    protected override float ComputeHeight()
    {
        return 4f + (circle_nb - 1) * 4 - (float)height / 12f - 0.75F;
    }
}
