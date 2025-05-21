using UnityEngine;

public class TitleWobble : MonoBehaviour
{
    public enum WobbleMode { SideToSideAndRotate, UpAndDown }
    public WobbleMode mode = WobbleMode.SideToSideAndRotate;

    public float moveAmplitude = 0.1f;
    public float moveFrequency = 1f;

    public float rotateAmplitude = 5f;
    public float rotateFrequency = 1f;

    public float phaseOffset = 0f;

    private Vector3 initialPosition;
    private Quaternion initialRotation;

    void Start()
    {
        initialPosition = transform.localPosition;
        initialRotation = transform.localRotation;
    }

    void Update()
    {
        float time = Time.time + phaseOffset;

        if (mode == WobbleMode.SideToSideAndRotate)
        {
            float offsetX = Mathf.Sin(time * moveFrequency) * moveAmplitude;
            float angleZ = Mathf.Sin(time * rotateFrequency) * rotateAmplitude;

            transform.localPosition = initialPosition + new Vector3(offsetX, 0f, 0f);
            transform.localRotation = initialRotation * Quaternion.Euler(0f, 0f, angleZ);
        }
        else if (mode == WobbleMode.UpAndDown)
        {
            float offsetY = Mathf.Sin(time * moveFrequency) * moveAmplitude;
            transform.localPosition = initialPosition + new Vector3(0f, offsetY, 0f);
            transform.localRotation = initialRotation;
        }
    }
}
