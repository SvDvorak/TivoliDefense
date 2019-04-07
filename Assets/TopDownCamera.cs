using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCamera : MonoBehaviour
{
    public float HeightMin;
    public float HeightMax;

    public float HeightMinPitch;
    public float HeightMaxPitch;

    public float CameraZoomSpeed;

    public float CameraPanSpeed;
    public float CameraZoomSpeedModifier;
    public float CameraZoomSpeedModifierPower;


    private Vector3 _targetPosition;
    private float _targetAngle = 45f;
    private float _targetPitch = 45f;
    private Resolution _resolution;
    private Vector3 _currentVelocity = Vector3.zero;

    private Vector2 _mousePosDelta = Vector2.zero;
    private Vector3 _mousePos;
    private float _angleVelocity;
    private float _pitchVelocity;

    // Start is called before the first frame update
    void Start()
    {
        _targetPosition = transform.position;
        _resolution = Screen.currentResolution;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetDelta = Vector3.zero;

        _mousePosDelta = Input.mousePosition - _mousePos;
        _mousePos = Input.mousePosition;

        var zoomFactor = (transform.position.y - HeightMin) / (HeightMax - HeightMin);
        var speedFactor = (zoomFactor + 1) * CameraZoomSpeedModifier;
        speedFactor = Mathf.Pow(speedFactor, CameraZoomSpeedModifierPower);

        if (Input.GetMouseButton(2))
        {

            var right = Camera.main.transform.right * _mousePosDelta.x;
            var forward = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z) * _mousePosDelta.y;

            targetDelta += (-right + -forward) * speedFactor * 0.02f;
            Debug.Log("Mouse 3 down");
        }
        else
        {
            var horizontalLimit = _resolution.width * 0.05;
            var verticalLimit = _resolution.height * 0.05;

            if (_mousePos.x < verticalLimit)
            {
                targetDelta += Camera.main.transform.right * -CameraPanSpeed * speedFactor;
            }
            else if (_mousePos.x > _resolution.width - horizontalLimit)
            {
                targetDelta += Camera.main.transform.right * CameraPanSpeed * speedFactor;
            }

            if (_mousePos.y < verticalLimit)
            {
                var cameraForward = Camera.main.transform.forward;
                cameraForward.y = 0;
                cameraForward = cameraForward.normalized;

                targetDelta += cameraForward * -CameraPanSpeed * speedFactor;
            }
            else if (_mousePos.y > _resolution.height - verticalLimit)
            {
                var cameraForward = Camera.main.transform.forward;
                cameraForward.y = 0;
                cameraForward = cameraForward.normalized;

                targetDelta += cameraForward * CameraPanSpeed * speedFactor;
            }
        }

        _targetPosition += targetDelta;

        var scroll = Input.mouseScrollDelta * (CameraZoomSpeed * zoomFactor + 1f);
        _targetPosition.y = _targetPosition.y + scroll.y;

        if (_targetPosition.y > HeightMax) _targetPosition.y = HeightMax;
        if (_targetPosition.y < HeightMin) _targetPosition.y = HeightMin;



        if (Input.GetKey(KeyCode.Q))
        {

            RaycastHit hitInfo;

            var groundHit = Physics.Raycast(transform.position, transform.forward, out hitInfo, Mathf.Infinity, LayerMask.GetMask("Ground"));

            var rotationVector = hitInfo.point - _targetPosition;
            //Debug.DrawLine(Camera.main.transform.position, Camera.main.transform.position + rotationVector, Color.yellow, 4);
            var grej = Quaternion.AngleAxis(2, Vector3.up) * rotationVector;

            _targetPosition = _targetPosition + rotationVector - grej;
            //Camera.main.transform.position = _targetPosition;

            _targetPosition.y = Camera.main.transform.position.y;
            _targetAngle += 2f;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            RaycastHit hitInfo;

            var groundHit = Physics.Raycast(transform.position, transform.forward, out hitInfo);
            var rotationVector = hitInfo.point - _targetPosition;
            //Debug.DrawLine(Camera.main.transform.position, Camera.main.transform.position + rotationVector, Color.yellow, 4);
            var grej = Quaternion.AngleAxis(-2, Vector3.up) * rotationVector;

            _targetPosition = _targetPosition + rotationVector - grej;
            //Camera.main.transform.position = _targetPosition;
            _targetPosition.y = Camera.main.transform.position.y;

            _targetAngle -= 2f;
        }


        _targetPitch = zoomFactor * (HeightMaxPitch - HeightMinPitch) + HeightMinPitch;

        transform.position = Vector3.SmoothDamp(transform.position, _targetPosition, ref _currentVelocity, 0.1f);
        transform.rotation = Quaternion.Euler(
            Mathf.SmoothDampAngle(transform.rotation.eulerAngles.x, _targetPitch, ref _pitchVelocity, 0.01f),
            Mathf.SmoothDampAngle(transform.rotation.eulerAngles.y, _targetAngle, ref _angleVelocity, 0.01f),
            transform.rotation.eulerAngles.z);
    }
}
