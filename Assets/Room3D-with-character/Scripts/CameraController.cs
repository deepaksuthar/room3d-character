using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform CinemachineCameraTarget; 
    public float mouseSensitivity = 100f; 
    public float CameraAngleOverride = 0f; 
    public float TopClamp = 80f; 
    public float BottomClamp = -80f; 
    public bool LockCameraPosition = false; 

    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;

    void Start()
    {
        _cinemachineTargetYaw = CinemachineCameraTarget.eulerAngles.y;
        _cinemachineTargetPitch = CinemachineCameraTarget.eulerAngles.x;
    }

    void LateUpdate()
    {
        CameraRotation();
    }

    private void CameraRotation()
    {
        if (!LockCameraPosition)
        {

            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity ;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            _cinemachineTargetYaw += mouseX;
            _cinemachineTargetPitch -= mouseY; 

            _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

            CinemachineCameraTarget.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride, _cinemachineTargetYaw, 0.0f);
        }
        else
        {
            CinemachineCameraTarget.localRotation = Quaternion.identity;

        }
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f) angle += 360f;
        if (angle > 360f) angle -= 360f;
        return Mathf.Clamp(angle, min, max);
    }
}
