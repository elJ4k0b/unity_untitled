using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Drawer
{
    public static void DrawCircle(LineRenderer line, float xradius, float yradius)
    {
        int segments = 50;
        float x;
        float y;
        float z;

        float angle = 20f;

        line.positionCount = segments + 1;
        line.useWorldSpace = false;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * xradius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * yradius;

            line.SetPosition(i, new Vector3(x, y, 0));

            angle += (360f / segments);
        }
    }
}
