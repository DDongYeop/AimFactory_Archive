using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum State { JumpReady, Idle, Walk}

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _jumpspeed = 10f;

    [SerializeField] private float _speechfadeTime = .5f;
    [SerializeField] private SpriteRenderer _speechBuble;

    [SerializeField] private AudioSource _jumpAudioSource;
    [SerializeField] private AudioSource _walkAudioSource;

    private TargetManager _targetManager;
    private Clear _clear;
    private GameManager _gameManager;

    private Rigidbody2D _rigidbody2D;
    private CapsuleCollider2D _capsuleCollider2D;
    private SpriteRenderer _spriteRenderer;
    
    private Vector3 _pos;
    
    private State _state = State.Idle;


    #region Animator
    private Animator _animator;
    private int _isRunning = Animator.StringToHash("isRunning");
    private int _isJump    = Animator.StringToHash("isJump");
    private int _isFall    = Animator.StringToHash("isFall");
    #endregion


    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();

        _targetManager = GameObject.Find("TargetSpawnArea").GetComponent<TargetManager>();
        _clear = GameObject.Find("Clear Collider").GetComponent<Clear>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Start()
    {
        _speechBuble.DOFade(0f, 0);
    }

    private void Update()
    {
        FlipSprite();
        Isfall();

        if (_clear.isClearUIOn)
            return;

        JumpReady();

        if (_state == State.JumpReady)
            return;

        PlayerMove();
    }

    private void PlayerMove()
    {
        if (Mathf.Abs(_rigidbody2D.velocity.y) > Mathf.Epsilon)
            return;

        float x = Input.GetAxisRaw("Horizontal");
        float xPos = x * _speed * Time.deltaTime;
        _pos = new Vector3(xPos, 0, 0);
        transform.position += _pos;

        if (x != 0)
            _animator.SetBool(_isRunning, true);
        else
            _animator.SetBool(_isRunning, false);
    }

    private void FlipSprite()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            _spriteRenderer.flipX = true;
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            _spriteRenderer.flipX = false;
    }

    private void JumpReady()
    {
        if (_state != State.JumpReady)
        {
            if (Input.GetKeyDown(KeyCode.Space) && _capsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
            {
                _state = State.JumpReady;
                _animator.SetBool(_isRunning, false);
                _targetManager.StartSpawn();

                Sequence seq = DOTween.Sequence();
                seq.Append(_speechBuble.DOFade(1f, _speechfadeTime));
            }
        }
    }

    public void Jump()
    {
        _state = State.Idle;
        _targetManager.StopSpawn();
        _animator.SetBool(_isJump, true);
        _jumpAudioSource.Play();
        
        Sequence seq = DOTween.Sequence();
        seq.Append(_speechBuble.DOFade(0f, _speechfadeTime));
    }

    public void PlayerJump(int minusJumpspeed)
    {
        float jumpspeed = _jumpspeed - minusJumpspeed;
        _rigidbody2D.AddForce(Vector3.up * jumpspeed, ForceMode2D.Impulse);

    }

    public void JumpMove(int direction)
    {
        StartCoroutine(JumpMoveCo(direction));
    }

    IEnumerator JumpMoveCo(int direction)
    {
        while (true)
        {
            float x = direction * (_speed - 1) * Time.deltaTime;
            _pos = new Vector3(x, 0, 0);
            transform.position += _pos;

            if (Mathf.Abs(_rigidbody2D.velocity.y) <= Mathf.Epsilon)
                StopAllCoroutines();
            yield return null;
        }
    }

    private void Isfall()
    {
        if (!_capsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")) && _rigidbody2D.velocity.y < 0)
        {
            _animator.SetBool(_isFall, true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _animator.SetBool(_isFall, false);
        _animator.SetBool(_isJump, false);

        _gameManager.Save();
    }

    public void WalkSound()
    {
        _walkAudioSource.Play();
    }
}
