using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform _target;
    private Vector3 _targetPosition;

    [SerializeField] private float _cameraSmoothing = 0.1f;
    [SerializeField] private Vector2 _minBound;
    [SerializeField] private Vector2 _maxBound;

    private void Awake()
    {
        _target = GameObject.Find("Player").transform;
    }

    private void FixedUpdate()
    {
        _targetPosition = new Vector3(_target.position.x, _targetPosition.y, transform.position.z);

        _targetPosition.x = Mathf.Clamp(_target.position.x, _minBound.x, _maxBound.x);
        _targetPosition.y = Mathf.Clamp(_target.position.y, _minBound.y, _maxBound.y);

        transform.position = Vector3.Lerp(transform.position, _targetPosition, _cameraSmoothing);
    }
}
