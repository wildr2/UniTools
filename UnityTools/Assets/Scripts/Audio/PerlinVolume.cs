using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class PerlinVolume : MonoBehaviour
{
    private AudioSource source;
    private float normal_volume = 1;
    public float speed = 1;
    public AnimationCurve curve;


    private void Awake()
    {
        source = GetComponent<AudioSource>();
        normal_volume = source.volume;
        StartCoroutine(Routine());
    }
    private IEnumerator Routine()
    {
        Vector2 p = Tools.RandomDirection2D() * Random.value * 100f;

        while (true)
        {
            p.x += Time.deltaTime * speed;
            source.volume = curve.Evaluate(Mathf.PerlinNoise(p.x, p.y)) * normal_volume;
            yield return null;
        }
    }
}
