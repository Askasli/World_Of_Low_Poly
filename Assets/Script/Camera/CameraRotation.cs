
using UnityEngine;
using Zenject;

public class CameraRotation : ICameraRotation
{
    private Transform playerTransform;
    private ÑameraShake _shakeSettings;

    private Vector3 targetOffset;
    private Vector3 position;

    private float Speed = 2f;

    private float xRot;
    private float yRot;

    private float smoothRotation;
    private Quaternion currentRotation;
    private Quaternion targetRotation;


    [Inject]
    private void Construct(Player player)
    {
        playerTransform = player.transform;
        _shakeSettings = new ÑameraShake();
    }

    public void RotateCamera(Vector3 camPose, float mouseX, float mouseY)
    {
        xRot += mouseY * Speed;
        yRot += mouseX * Speed;

        xRot = ClampAngle(xRot, -60f, 20);

        if (smoothRotation < 1)
            smoothRotation += Time.deltaTime / 2f;

        targetRotation = Quaternion.Euler(-xRot, yRot, 0f);
        targetOffset = Vector3.Lerp(targetOffset, camPose, Time.deltaTime);

        currentRotation = Quaternion.Slerp(currentRotation, targetRotation, smoothRotation);
        position = playerTransform.position + new Vector3(0, 0.4f, 0) - (currentRotation * targetOffset);


        Camera.main.transform.rotation = currentRotation;
        Camera.main.transform.position = position;

        /*
        Vector3 rotationAmount = Random.insideUnitSphere * _shakeSettings.ShakeMagnitude;
        rotationAmount.z = 0f;
        Camera.main.transform.rotation = currentRotation * Quaternion.Euler(rotationAmount);
        */
    }


    private static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
        {
            angle += 360;
        }
        if (angle > 360)
        {
            angle -= 360;
        }

        return Mathf.Clamp(angle, min, max);
    }
}