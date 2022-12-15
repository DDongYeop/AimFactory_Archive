using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterMODE : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Player _player;

    private float speed = 7.5f;
    private bool _isMaster = false;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            _isMaster = !_isMaster;
        }

        MaterMode();
    }

    private void MaterMode()
    {
        _player.enabled = !_isMaster;

        if (_isMaster)
        {
            _rigidbody.gravityScale = 0;

            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");

            transform.position += new Vector3(x, y, 0);
        }
        else
            _rigidbody.gravityScale = 3.5f;
    }
}
