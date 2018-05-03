using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChangController : MonoBehaviour
{

    #region フィールド

    private Transform _transform;
    private Animator animator;

    private float moveSpeed;
    private Vector3 moveDirection;

    #endregion


    #region プロパティ

    private bool IsRunning { get { return moveDirection.magnitude != 0f; } }

    #endregion


    #region Unity メソッド

    /// <summary> 
    /// 初期化処理
    /// </summary>
    void Awake()
    {
        _transform = transform;
        animator = GetComponent<Animator>();

        moveSpeed = 5.0f;
        moveDirection = Vector3.zero;
    }

    /// <summary> 
    /// 更新処理
    /// </summary>
    void Update()
    {
        InputDirection();
        Move();
        Turn();
        Animate();
    }

    #endregion


    #region メソッド

    public void InputDirection()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector3(x, 0f, z);
    }

    private void Move()
    {
        _transform.position += moveDirection.normalized * moveSpeed * Time.deltaTime;
    }

    private void Turn()
    {
        if (!IsRunning) return;
        Quaternion targetRotation = Quaternion.LookRotation(moveDirection.normalized);
        _transform.rotation = Quaternion.RotateTowards(_transform.rotation, targetRotation, 360f * Time.deltaTime);
    }

    private void Animate()
    {
        animator.SetBool("isRunning", IsRunning);
    }

    #endregion

}