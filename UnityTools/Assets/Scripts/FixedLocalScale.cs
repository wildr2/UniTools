using UnityEngine;
using System.Collections;

/// <summary>
/// Keeps the local scale of an object constant relative to the main camera.
/// </summary>
public class FixedLocalScale : MonoBehaviour
{
    public float factor = 0.75f; // between 0 and 1

    private Vector3 scale;
    private float orig_ortho;

    private void Awake()
    {
        scale = transform.localScale;
        orig_ortho = Camera.main.orthographicSize;
        factor = Mathf.Clamp01(factor);
    }
    private void Update()
    {
        transform.localScale = (1f - factor) * scale + factor * scale * (Camera.main.orthographicSize / orig_ortho);
    }
}
