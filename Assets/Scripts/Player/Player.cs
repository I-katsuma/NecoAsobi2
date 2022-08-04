using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // public変数
    public float speed;
    public float jumpSpeed;
    public float jumpHeight;
    public float jumpLimitTime;
    public float gravity;
    public GroundCheck ground; // 地面
    public GroundCheck head; // あたま
    public AnimationCurve dashCurve;
    public AnimationCurve jumpCurve;

    // private変数
    private Animator anim = null;
    private Rigidbody2D rb2D = null;
    private bool isGround = false;
    private bool isHead = false;
    private bool isJump = false;
    private float jumpPos = 0.0f;
    private float jumpTime = 0.0f; // 滞空時間
    private float dashTime = 0.0f;
    private float beforeKey = 0.0f;

    private void Awake()
    {
        speed = ParamsSO.Entity.speed;
        jumpSpeed = ParamsSO.Entity.jumpSpeed;
        jumpHeight = ParamsSO.Entity.jumpHeight;
        jumpLimitTime = ParamsSO.Entity.jumpLimitTime;
        gravity = ParamsSO.Entity.gravity;
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        isGround = ground.IsGround();
        isHead = ground.IsGround();

        float horizontalKey = Input.GetAxis("Horizontal");
        float xSpeed = 0.0f;
        float ySpeed = GetYSpeed();

        if (horizontalKey > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            anim.SetBool("run", true);
            dashTime += Time.deltaTime;
            xSpeed = speed;
        }
        else if (horizontalKey < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            anim.SetBool("run", true);
            dashTime += Time.deltaTime;
            xSpeed = -speed;
        }
        else
        {
            anim.SetBool("run", false);
            dashTime = 0.0f;
            xSpeed = 0.0f;
        }

        // 前回の入力からダッシュの反転を判断して速度を変える
        if (horizontalKey > 0 && beforeKey < 0)
        {
            dashTime = 0.0f;
        }
        else if (horizontalKey < 0 && beforeKey > 0)
        {
            dashTime = 0.0f;
        }
        beforeKey = horizontalKey;

        // アニメーションカーブを速度に適応
        xSpeed *= dashCurve.Evaluate(dashTime); // 指定された時間の値を返す

        anim.SetBool("jump", isJump);
        anim.SetBool("ground", isGround);
        rb2D.velocity = new Vector2(xSpeed, ySpeed);
    }

    /// <summary>
    /// Y成分で必要な計算をし、速度を返す
    /// </summary>
    /// <returns>Y軸の速さ</returns>
    private float GetYSpeed()
    {
        float verticalKey = Input.GetAxis("Vertical");
        float ySpeed = -gravity;

        if (isGround)
        {
            if (verticalKey > 0)
            {
                ySpeed = jumpSpeed;
                jumpPos = transform.position.y; // ジャンプした高さを保存
                isJump = true;
                jumpTime = 0.0f;
            }
            else
            {
                isJump = false;
            }
        }
        else if (isJump)
        {
            // 上方向キーを押しているか
            bool pushUpKey = verticalKey > 0;

            // 現在の高さが飛べる高さより下か
            bool canHeight = jumpPos + jumpHeight > transform.position.y;

            // ジャンプ時間が長くなりすぎてないか
            bool canTime = jumpLimitTime > jumpTime;

            if (pushUpKey && canHeight && canTime && !isHead)
            {
                // 上方向キー かつ 飛べる高さより↓ かつ ジャンプの時間がオーバーしていない かつ 頭ぶつけてない
                ySpeed = jumpSpeed;
                jumpTime += Time.deltaTime;
            }
            else
            {
                isJump = false;
                jumpTime = 0.0f;
            }
        }

        if (isJump)
        {
            ySpeed *= jumpCurve.Evaluate(jumpTime);
        }
        return ySpeed;
    }
}
