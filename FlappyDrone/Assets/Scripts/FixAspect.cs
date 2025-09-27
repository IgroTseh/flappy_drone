using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FixAspect : MonoBehaviour {
    void Start()
    {
        Camera cam = GetComponent<Camera>();
        float targetAspect = 9f / 16f; // портретное соотношение

        float windowAspect = (float)Screen.width / (float)Screen.height;
        float scaleHeight = windowAspect / targetAspect;

        if (scaleHeight < 1.0f)
        {
            // добавляем черные полосы сверху и снизу
            Rect rect = cam.rect;

            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;

            cam.rect = rect;
        }
        else
        {
            // добавляем черные полосы слева и справа
            float scaleWidth = 1.0f / scaleHeight;

            Rect rect = cam.rect;

            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;

            cam.rect = rect;
        }
    }
}
