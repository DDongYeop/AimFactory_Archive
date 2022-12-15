using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static Define;

public class Target : MonoBehaviour
{
    [SerializeField] float destoryTime = 6f;
    public float time = 0;
    public bool isJumpReady = false;
    
    private TargetManager _targetManager;

    private Player _player;

    private Transform _transform;
    private SpriteRenderer _spriteRenderer;

    private bool _isDestroy = false;

    private void Awake()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _targetManager = GameObject.Find("TargetSpawnArea").GetComponent<TargetManager>();

        _transform = GetComponent<Transform>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _transform.localScale = Vector3.zero;
    }

    private void Update()
    {
        time += Time.deltaTime;

        TargetClick();

        if (transform.localScale.x > 1.44f)
            _spriteRenderer.color = new Color(244f/255f, 244f/255f, 144f/255f, 1);
        else
            _spriteRenderer.color = Color.white;
    }

    public void TargetClick()
    {
        isJumpReady = true;
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 touchPos = MainCam.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hitInformation = Physics2D.Raycast(touchPos, Camera.main.transform.forward);
            GameObject touchedObject = hitInformation.transform.gameObject;
            if (isJumpReady)
            {
                if (touchedObject == gameObject)
                {
                    int jumppos;
                    if (transform.localScale.x < 1.44f)
                        jumppos = 7;
                    else
                        jumppos = 0;

                    _player.PlayerJump(jumppos);
                    TargetScale();
                    _targetManager.ParticleSpawn(gameObject.transform.position, jumppos);
                    _targetManager.AllTargetDestroy();

                    if (Input.GetKey(KeyCode.D))
                        _player.JumpMove(1);
                    else if (Input.GetKey(KeyCode.A))
                        _player.JumpMove(-1);

                    _player.Jump();
                }
                isJumpReady = false;
            }
        }
    }

    public void TargetScale()
    {
        _isDestroy = false;
        Sequence seq = DOTween.Sequence();

        seq.Append(transform.DOScale(0, 0));
        seq.Append(transform.DOScale(1.5f, destoryTime));
        seq.AppendCallback(TargetSmall);
    }

    public void TargetSmall()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOScale(0, 0.2f));
        seq.AppendCallback(TargetDestroy);
    }

    private void TargetDestroy()
    {
        if (_isDestroy == false)
        {
            PoolManager.instance.Push(gameObject);
            _targetManager.spawnTargetNum--;
            _isDestroy = true;
        }
    }
}
