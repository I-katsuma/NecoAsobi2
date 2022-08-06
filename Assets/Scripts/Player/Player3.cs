using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class Player3 : MonoBehaviour
{
    // public
    public GroundCheck ground;
    public GroundCheck head;

    //[SerializeField] LayerMask groundLayer;
    public float _jumpForce = 3;
    public float _moveForce = 5;

    // private
    private int stateNum;
    private bool isGround;
    private bool isHead;
    private bool isRun;
    private bool isOnJump;
    private float jumpPos;
    private Vector2 _moveInputValue;

    private Rigidbody2D _rigidbody2d;
    private Animator _anim;

    void Start()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        /*
        _moveAction = actionMap["Move"];
        _jumpAction = actionMap["Jump"];
        actionMap["Move"].performed += OnMove;
        actionMap["jump"].performed += OnJump;
        */
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log("OnMove");
        _moveInputValue = context.ReadValue<Vector2>();

        if (_moveInputValue.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            isRun = true;
        }
        else if (_moveInputValue.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            isRun = true;
        }
        else
        {
            isRun = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (isGround)
        {
            _rigidbody2d.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            Debug.Log("OnJump");
        }
    }

    public void OnChangeMode(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            stateNum++;
            if(stateNum > 2 || stateNum < 0)
            {
                stateNum = 0;
            }
            Debug.Log(stateNum);
        }

        switch (stateNum)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
        }
    }

    private void SetAnimation()
    {
        _anim.SetBool("run", isRun);
        _anim.SetBool("jump", isOnJump);
        _anim.SetBool("ground", isGround);
    }

    void Update()
    {
        //Debug.Log(stateNum);

        isGround = ground.IsGround();
        isHead = ground.IsGround();
        jumpPos = _rigidbody2d.velocity.y;

        // Animation jump
        if (!isGround && jumpPos < 10f && jumpPos > 0)
        {
            // 上昇中
            isOnJump = true;
        }
        else if (jumpPos <= 0)
        {
            isOnJump = false;
        }

        SetAnimation();

        // Move
        _rigidbody2d.velocity = new Vector2(_moveInputValue.x * _moveForce, jumpPos);
    }
}
