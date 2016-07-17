﻿// From http://wiki.unity3d.com/index.php?title=AspectRatioEnforcer#AspectUtility.cs


using UnityEngine;

public class AspectUtility : MonoBehaviour
{
    public float _wantedAspectRatio = 1.3333333f;
    static float wantedAspectRatio;
    static float prev_aspect_ratio;
    static Camera[] cams;
    static Camera backgroundCam;

    void Awake()
    {
        cams = FindObjectsOfType<Camera>();
        //cam = GetComponent<Camera>();
        //if (!cam)
        //{
        //    cam = Camera.main;
        //}
        //if (!cam)
        //{
        //    Debug.LogError("No camera available");
        //    return;
        //}
        wantedAspectRatio = _wantedAspectRatio;
        UpdateCameras();
    }
    void Update()
    {
        UpdateCameras();
    }

    public static void UpdateCameras()
    {   
        float currentAspectRatio = (float)Screen.width / Screen.height;
        if (currentAspectRatio == prev_aspect_ratio) return;
        prev_aspect_ratio = currentAspectRatio;

        Debug.Log("here");

        // If the current aspect ratio is already approximately equal to the desired aspect ratio,
        // use a full-screen Rect (in case it was set to something else previously)
        if ((int)(currentAspectRatio * 100) / 100.0f == (int)(wantedAspectRatio * 100) / 100.0f)
        {
            foreach (Camera cam in cams) cam.rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
            if (backgroundCam)
            {
                Destroy(backgroundCam.gameObject);
            }
            return;
        }
        // Pillarbox
        if (currentAspectRatio > wantedAspectRatio)
        {
            float inset = 1.0f - wantedAspectRatio / currentAspectRatio;
            foreach (Camera cam in cams) cam.rect = new Rect(inset / 2, 0.0f, 1.0f - inset, 1.0f);
        }
        // Letterbox
        else
        {
            float inset = 1.0f - currentAspectRatio / wantedAspectRatio;
            foreach (Camera cam in cams) cam.rect = new Rect(0.0f, inset / 2, 1.0f, 1.0f - inset);
        }
        if (!backgroundCam)
        {
            // Make a new camera behind the normal camera which displays black; otherwise the unused space is undefined
            backgroundCam = new GameObject("BackgroundCam", typeof(Camera)).GetComponent<Camera>();
            backgroundCam.depth = int.MinValue;
            backgroundCam.clearFlags = CameraClearFlags.SolidColor;
            backgroundCam.backgroundColor = Color.black;
            backgroundCam.cullingMask = 0;
        }
    }

    public static int screenHeight
    {
        get
        {
            return (int)(Screen.height * cams[0].rect.height);
        }
    }

    public static int screenWidth
    {
        get
        {
            return (int)(Screen.width * cams[0].rect.width);
        }
    }

    public static int xOffset
    {
        get
        {
            return (int)(Screen.width * cams[0].rect.x);
        }
    }

    public static int yOffset
    {
        get
        {
            return (int)(Screen.height * cams[0].rect.y);
        }
    }

    public static Rect screenRect
    {
        get
        {
            return new Rect(cams[0].rect.x * Screen.width, cams[0].rect.y * Screen.height, cams[0].rect.width * Screen.width, cams[0].rect.height * Screen.height);
        }
    }

    public static Vector3 mousePosition
    {
        get
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.y -= (int)(cams[0].rect.y * Screen.height);
            mousePos.x -= (int)(cams[0].rect.x * Screen.width);
            return mousePos;
        }
    }

    public static Vector2 guiMousePosition
    {
        get
        {
            Vector2 mousePos = Event.current.mousePosition;
            mousePos.y = Mathf.Clamp(mousePos.y, cams[0].rect.y * Screen.height, cams[0].rect.y * Screen.height + cams[0].rect.height * Screen.height);
            mousePos.x = Mathf.Clamp(mousePos.x, cams[0].rect.x * Screen.width, cams[0].rect.x * Screen.width + cams[0].rect.width * Screen.width);
            return mousePos;
        }
    }
}
