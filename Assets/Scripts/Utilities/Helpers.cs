using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helpers
{
    public static float GetDiffAngle2D(Vector3 forward, Vector3 vectorToTarget)
    {
        Vector2 currentDirection = new Vector2(forward.x, forward.z).normalized;
        Vector2 targetDirection = new Vector2(vectorToTarget.x, vectorToTarget.z).normalized;

        float diffAngle = Vector2.Angle(currentDirection, targetDirection);

        // For some reason, this angle is absolute. Do some algebra magic to get negative angle
        Vector3 cross = Vector3.Cross(forward, vectorToTarget);
        if (cross.y < 0)
        {
            diffAngle *= -1;
        }
        return diffAngle;
    }

    public static void DrawDebugLine(Vector3 from, Vector3 to)
    {
        Color color = new Color(0, 0, 1.0f);
        Debug.DrawLine(from, to, color);
    }

    public static ModuleType StringToModuleType(string typeString)
    {
        return (ModuleType)System.Enum.Parse(typeof(ModuleType), typeString);
    }
}
