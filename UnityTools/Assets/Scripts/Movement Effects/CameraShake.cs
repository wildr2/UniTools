using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraShake : ShakingObj
{
    private Dictionary<System.Enum, CamShakeParams> shake_defs
        = new Dictionary<System.Enum, CamShakeParams>();


    public void DefineShakeType(System.Enum type, CamShakeParams shake_params)
    {
        shake_defs[type] = shake_params;
    }

    public void Shake(CamShakeParams shake_params)
    {
        FreezeFrames(shake_params.freeze_frames);
        Shake(shake_params.shake_params);
    }
    public void Shake(System.Enum shake_type)
    {
        Shake(shake_defs[shake_type]);
    }
    public void FreezeFrames(int frames)
    {
        StartCoroutine(Freeze(frames));
    }


    protected override void Awake()
    {
        base.Awake();

        
    }
    private IEnumerator Freeze(float frames = 3)
    {
        TimeScaleManager.Instance.SetFactor(0);
        for (int i = 0; i < frames; ++i)
        {
            yield return null;
        }
        TimeScaleManager.Instance.SetFactor(1);
    }   
}

public class CamShakeParams
{
    public ShakeParams shake_params;
    public int freeze_frames = 0;
    
    public CamShakeParams()
    {
        shake_params = new ShakeParams(0, 0, 0);
    }
    public CamShakeParams(float duration, float intensity, float speed, int freeze_frames)
    {
        shake_params = new ShakeParams(duration, intensity, speed);
        this.freeze_frames = freeze_frames;
    }
}

