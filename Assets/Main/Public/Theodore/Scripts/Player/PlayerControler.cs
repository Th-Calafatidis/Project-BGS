using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    private Rigidbody2D _rb2d;
    private Animator _animator;

    [SerializeField] private GameObject _gfxFront;
    [SerializeField] private GameObject _gfxBack;
    [SerializeField] private GameObject _gfxLeft;
    [SerializeField] private GameObject _gfxRight;
    private List<GameObject> _gfx = new List<GameObject>();

    private Vector2 _moveInput;

    [SerializeField] private float _speed;

    void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _rb2d.gravityScale = 0f;

        _animator = GetComponentInChildren<Animator>();

        //_gfxFront = transform.GetChild(0).gameObject;
        _gfx.Add(_gfxFront);

        //_gfxBack = transform.GetChild(1).gameObject;
        _gfx.Add(_gfxBack);

        //_gfxLeft = transform.GetChild(2).gameObject;
        _gfx.Add(_gfxLeft);

        //_gfxRight = transform.GetChild(3).gameObject;
        _gfx.Add(_gfxRight);

    }

    private void Update()
    {
        GatherInput();

        HandleAnimations();
    }

    private void FixedUpdate()
    {
        _rb2d.MovePosition(_rb2d.position + _moveInput * _speed * Time.fixedDeltaTime);
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
