// Date: 2/26/2023
// Developer: Theodore Calafatidis
//
// Description: Responsible for handling the camera behaviour

using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // The transform of the target the camera will follow
    private Transform _target;

    // The target position the camera will try to move at
    private Vector3 _targetPosition;


    [SerializeField]
    [Tooltip("A value from 0-1 that controls the camera movement smoothing.")]
    private float _cameraSmoothing = 0.1f;

    [Tooltip("The minimum bound (bottom left) the camera can move at.")]
    [SerializeField] private Vector2 _minBound;

    [Tooltip("The maximum bound (top right) the camera can move at.")]
    [SerializeField] private Vector2 _maxBound;

    private void Awake()
    {
        _target = GameObject.Find("Player").transform;
    }

    // While a common practise is to move the camera in LateUpdate(), since the player character
    // is moved inside the FixedUpdate, FixedUpdate is used to move the camera too to avoid camera jittering.
    private void FixedUpdate()
    {
        // The position the camera will try to move at defined by the _target's position
        _targetPosition = new Vector3(_target.position.x, _targetPosition.y, transform.position.z);

        // Clamp the position the camera will move at so it doesnt move outside the predefined bounds
        _targetPosition.x = Mathf.Clamp(_target.position.x, _minBound.x, _maxBound.x);
        _targetPosition.y = Mathf.Clamp(_target.position.y, _minBound.y, _maxBound.y);

        // Move the camera
        transform.position = Vector3.Lerp(transform.position, _targetPosition, _cameraSmoothing);
    }
}
