using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Game
{
    public class Instance : MonoBehaviour
    {
        [RuntimeInitializeOnLoadMethod]
        static void Initialize()
        {
            Screen.SetResolution(1920, 1080, false);

            Camera camera = Camera.main;
            float orthographicSize = camera.orthographicSize;
            float aspectRatio = Screen.width * 1.0f / Screen.height;
            float cameraHeight = orthographicSize * 2;
            float cameraWidth = cameraHeight * aspectRatio;
            Debug.Log("camera size in unit=" + cameraWidth + "," + cameraHeight);
        }

    }
}
