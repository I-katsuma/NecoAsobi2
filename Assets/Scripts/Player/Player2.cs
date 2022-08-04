using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//[RequireComponent(typeof(Rigidbody2D))]
public class Player2 : MonoBehaviour
{

    //public static Player2 instance;

    // public
    public GroundCheck ground;
    public GroundCheck head;

    // private
    private bool isGround;
    private bool isHead;
    private bool isRun;

    //private bool isJump;

    [SerializeField]
    private float _moveForce = 5;

    [SerializeField]
    private float _jumpForce = 5;
    //private float _verticalVelocity;

    [SerializeField] private Rigidbody2D _rigidbody2D;
    private Animator _anim = null;
    private GameInputs _gameInputs;
    //private InputAction _moveAction, _JumpAction;
    private Vector2 _moveInputValue;
    private bool isOnJump;

    [SerializeField]
    private float jumpPos; // ジャンプした高さを保持

    private enum actionNum
    {
        stop = 0,
        run = 1,
        jump = 2,
        fall = 3,
    }

    
    private void Awake()
    {
        if(_gameInputs != null)
            _gameInputs.Disable();
        
    }
    

    private void Start()
    {

        //_rigidbody2D = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _gameInputs = new GameInputs(); // InputActionインスタンス生成
        
        
        //var _gameInputs = GetComponent<PlayerInput>();
        
        //var actionMap = _gameInputs.currentActionMap;

        //_moveAction = actionMap["OnMove"];
        //_JumpAction = actionMap["OnJump"];


       /* 
        // Actionイベント登録
        _gameInputs.Player2.Move.started += OnMove;
        _gameInputs.Player2.Move.performed += OnMove;
        _gameInputs.Player2.Move.canceled += OnMove;
        _gameInputs.Player2.Jump.performed += OnJump;
        */
        // InputActionを機能させるため、有効化する必要がある
        _gameInputs.Enable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        // Moveアクション入力取得
        _moveInputValue = context.ReadValue<Vector2>();
        Debug.Log(_moveInputValue.x);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (isGround)
        {
            // ジャンプする力
            _rigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
        else
        {
            return;
        }
    }

    private float GetXspeed()
    {
        float xSpeed = 0.0f;

        if (_moveInputValue.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            //_anim.SetBool("run", true);
            isRun = true;
            xSpeed = _moveForce;
        }
        else if (_moveInputValue.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            //_anim.SetBool("run", true);
            isRun = true;
            xSpeed = -_moveForce;
        }
        else
        {
            //_anim.SetBool("run", false);
            isRun = false;
            xSpeed = 0.0f;
        }

        return xSpeed;
    }

    private void SetAnimation()
    {
        _anim.SetBool("run", isRun);
        _anim.SetBool("jump", isOnJump);
        _anim.SetBool("ground", isGround);
    }

    private void FixedUpdate()
    {
        isGround = ground.IsGround();
        isHead = ground.IsGround();
        jumpPos = _rigidbody2D.velocity.y;

        // Jump
        if (!isGround && jumpPos < 10f && jumpPos > 0)
        {
            // 上昇中
            isOnJump = true;
        }
        else if (jumpPos <= 0)
        {
            isOnJump = false;
        }

        // Move
        _rigidbody2D.velocity = new Vector2(GetXspeed(), jumpPos);

        SetAnimation();
    }
}
