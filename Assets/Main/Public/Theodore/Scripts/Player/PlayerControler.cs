// Date: 2/25/2023
// Developer: Theodore Calafatidis
//
// Description: Responsible for moving the player, activating the appropriate player sprites and handling player animations.

using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    private Rigidbody2D _rb2d;
    private Animator _animator;

    private List<GameObject> _gfx = new List<GameObject>();

    private Vector2 _moveInput;

    [SerializeField] private GameObject _gfxFront;
    [SerializeField] private GameObject _gfxBack;
    [SerializeField] private GameObject _gfxLeft;
    [SerializeField] private GameObject _gfxRight;
    [SerializeField] private float _speed;

    void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _rb2d.gravityScale = 0f;

        _animator = GetComponentInChildren<Animator>();

        _gfx.Add(_gfxFront);
        _gfx.Add(_gfxBack);
        _gfx.Add(_gfxLeft);
        _gfx.Add(_gfxRight);

    }

    private void Update()
    {
        GatherInput();

        HandleAnimations();
    }

    private void FixedUpdate()
    {
        _rb2d.MovePosition(_rb2d.position + _moveInput.normalized * _speed * Time.fixedDeltaTime);
    }

    void GatherInput()
    {
        _moveInput.x = Input.GetAxisRaw("Horizontal");
        _moveInput.y = Input.GetAxisRaw("Vertical");
    }

    void HandleAnimations()
    {
        if (_moveInput.x > 0 && _moveInput.y == 0)
        {
            DeactivateGfx();
            _gfxRight.SetActive(true);

            _animator.SetInteger("State", 1);
        }

        if (_moveInput.x < 0 && _moveInput.y == 0)
        {
            DeactivateGfx();
            _gfxLeft.SetActive(true);

            _animator.SetInteger("State", 1);
        }

        if (_moveInput.y > 0 && _moveInput.x == 0)
        {
            DeactivateGfx();
            _gfxBack.SetActive(true);

            _animator.SetInteger("State", 1);
        }

        if (_moveInput.y < 0 && _moveInput.x == 0)
        {
            DeactivateGfx();
            _gfxFront.SetActive(true);

            _animator.SetInteger("State", 1);
        }

        if (_moveInput.x == 0 && _moveInput.y == 0)
        {
            _animator.SetInteger("State", 0);
        }
    }

    private void DeactivateGfx()
    {
        foreach (GameObject obj in _gfx)
        {
            obj.SetActive(false);
        }
    }
}
