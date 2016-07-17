using UnityEngine;
using System.Collections;

/// <summary>
/// Keeps the rotation of an object constant relative to its parent and the main camera.
/// The local rotation on Awake is taken as the rotation to fix.
/// </summary>
public class FixedRotation : MonoBehaviour
{
    private Quaternion r;

    private void Awake()
    {
        r = transform.localRotation;
    }
    private void Update()
    {
        transform.rotation = r * Camera.main.transform.rotation;
    }
}
