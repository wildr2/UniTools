using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class IVector2
{
    public int x;
    public int y;

    public IVector2(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    public IVector2(Vector2 pos)
    {
        this.x = (int)pos.x;
        this.y = (int)pos.y;
    }
    public float Magnitude()
    {
        return Mathf.Sqrt(x * x + y * y);
    }
    public Vector2 AsVector2()
    {
        return new Vector2(x, y);
    }

    public static float Distance(IVector2 v1, IVector2 v2)
    {
        int dx = v2.x - v1.x;
        int dy = v2.y - v1.y;
        return Mathf.Sqrt(dx * dx + dy * dy);
    }
    public static int DistanceManhatten(IVector2 v1, IVector2 v2)
    {
        int dx = Mathf.Abs(v2.x - v1.x);
        int dy = Mathf.Abs(v2.y - v1.y);
        return dx + dy;
    }
    public static IVector2 operator +(IVector2 v1, IVector2 v2)
    {
        return new IVector2(v1.x + v2.x, v1.y + v2.y);
    }
    public static IVector2 operator -(IVector2 v1, IVector2 v2)
    {
        return new IVector2(v1.x - v2.x, v1.y - v2.y);
    }
    public static IVector2 operator *(IVector2 v1, int s)
    {
        return new IVector2(v1.x * s, v1.y * s);
    }
    public override bool Equals(object obj)
    {
        // If parameter is null return false
        if (obj == null)
        {
            return false;
        }

        // If parameter cannot be cast to IVector2 return false
        IVector2 v = obj as IVector2;
        if ((System.Object)v == null)
        {
            return false;
        }

        // Return true if the fields match:
        return (x == v.x) && (y == v.y);
    }
    public override int GetHashCode()
    {
        return x ^ y;
    }
    public override string ToString()
    {
        return "(" + x + ", " + y + ")";
    }
}