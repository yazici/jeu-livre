using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <inheritdoc />
/// MouseLook rotates the transform based on the mouse delta.
/// Minimum and Maximum values can be used to constrain the possible rotation
/// To make an FPS style character:
/// - Create a capsule.
/// - Add the MouseLook script to the capsule.
///   -&gt; Set the mouse look to use LookX. (You want to only turn character but not tilt it)
/// - Add FPSInputController script to the capsule
///   -&gt; A CharacterMotor and a CharacterController component will be automatically added.
/// - Create a camera. Make the camera a child of the capsule. Reset it's transform.
/// - Add a MouseLook script to the camera.
///   -&gt; Set the mouse look to use LookY. (You want the camera to tilt up and down like a head. The character already turns.)
[AddComponentMenu("Camera-Control/Mouse Look")]
public class MouseLook : MonoBehaviour
{
    public enum RotationAxes
    {
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2
    }

    public RotationAxes m_Axes = RotationAxes.MouseXAndY;
    public float m_SensitivityX = 15f;
    public float m_SensitivityY = 15f;

    // Mouse rotation input
    private float m_RotationX;
    private float m_RotationY;

    // Used to calculate the rotation of this object
    private Quaternion m_OriginalRotation;

    // Minimum angle you can look up
    public float m_MinimumY = -60;
    public float m_MaximumY = 60;

    // Number of frames to be averaged, used for smoothing mouse look
    private int m_FramesCounter;

    // Array of rotations to be averaged
    private Queue<float> m_RotArrayX;
    private Queue<float> m_RotArrayY;

    private static bool _isMouseLooking;
    private static bool _shouldStopSfx;


    private void Start()
    {
        m_FramesCounter = GameManager.m_Instance.m_MainSettings.m_FrameCounterCameraLag;
        m_RotArrayX = new Queue<float>(m_FramesCounter);
        m_RotArrayY = new Queue<float>(m_FramesCounter);

        // Make the rigid body not change rotation
        var rb = GetComponent<Rigidbody>();
        if (rb) rb.freezeRotation = true;
        m_OriginalRotation = transform.localRotation;
    }

    private void Update()
    {
        if (Cursor.lockState == CursorLockMode.None || GameManager.m_Instance.m_CinematicMode) return;

        float rotAverageX = 0;
        float rotAverageY = 0;

        if (m_Axes == RotationAxes.MouseXAndY || (m_Axes == RotationAxes.MouseX))
        {
            // Mouse/Camera Movement Smoothing:     
            // Average rotationX for smooth mouse look
            m_RotationX += Input.GetAxis("Mouse X") * m_SensitivityX;

            // Add the current rotation to the array, at the last position
            m_RotArrayX.Enqueue(m_RotationX);

            // Reached max number of steps?  Remove the oldest rotation from the array
            if (m_RotArrayX.Count >= m_FramesCounter) m_RotArrayX.Dequeue();

            // Add all of these rotations together
            rotAverageX = m_RotArrayX.Average();
        }

        if (m_Axes == RotationAxes.MouseXAndY || (m_Axes == RotationAxes.MouseY))
        {
            m_RotationY += Input.GetAxis("Mouse Y") * m_SensitivityY;
            m_RotArrayY.Enqueue(m_RotationY);

            if (m_RotArrayY.Count >= m_FramesCounter) m_RotArrayY.Dequeue();

            rotAverageY = m_RotArrayY.Average();
        }


        // Set Rotation Limits for vertical    
        if ((rotAverageY >= -360) && (rotAverageY <= 360))
        {
            rotAverageY = Mathf.Clamp(rotAverageY, m_MinimumY, m_MaximumY);
        }
        else if (rotAverageY < -360)
        {
            rotAverageY = Mathf.Clamp(rotAverageY + 360, m_MinimumY, m_MaximumY);
        }
        else
        {
            rotAverageY = Mathf.Clamp(rotAverageY - 360, m_MinimumY, m_MaximumY);
        }

        // Apply and rotate this object
        Quaternion xQuaternion = Quaternion.AngleAxis(rotAverageX, Vector3.up);
        Quaternion yQuaternion = Quaternion.AngleAxis(rotAverageY, Vector3.left);

        transform.localRotation = m_OriginalRotation * xQuaternion * yQuaternion;

        // Start mouse looking
        if (Math.Abs(Input.GetAxis("Mouse X")) > 0.2f || Math.Abs(Input.GetAxis("Mouse Y")) > 0.2f)
        {
            if (_isMouseLooking) return;

            _isMouseLooking = true;
            StopCoroutine("StopSfxAfterDelay");
            AudioManager.m_Instance.PlaySFX("DroneRotation");
        }
        // Stop mouse looking
        else
        {
            if (!_isMouseLooking) return;

            _isMouseLooking = false;
            StopCoroutine("StopSfxAfterDelay");
            StartCoroutine(StopSfxAfterDelay());
        }
    }

    private IEnumerator StopSfxAfterDelay()
    {
        yield return new WaitForSeconds(0.2f);
        if (!_isMouseLooking) AudioManager.m_Instance.StopSFX("DroneRotation");
    }
}