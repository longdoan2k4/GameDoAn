using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class player : MonoBehaviour
{
    public float movespeed = 5f;
    public float dashBoots;
    public float dashTime;
    private float _dashTime;
    bool isDashing = false;
    public GameObject ghostEffect;
    public float ghostDelaySeconds;
    private Coroutine dashEffectCoroutine;

    private Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer characterSR;

    public Player_Combat player_Combat;

    public Vector3 moveInput;

    private bool isKnockedBack;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("CharStab"))
        {
            player_Combat.Attack();
        }
    }

    private void FixedUpdate()
    {
        if (isKnockedBack == false)
        {
            moveInput.x = Input.GetAxis("Horizontal");
            moveInput.y = Input.GetAxis("Vertical");
            transform.position += moveInput * movespeed * Time.deltaTime;

            animator.SetFloat("Speed", moveInput.sqrMagnitude);
        }

        if (Input.GetKeyDown(KeyCode.Space) && _dashTime <= 0 && isDashing == false)
        {
            movespeed += dashBoots;
            _dashTime = dashTime;
            isDashing = true;
            StartDashEffect();
        }

        // Điều chỉnh lại tốc độ khi dừng dash
        if (_dashTime <= 0 && isDashing == true)
        {
            movespeed = 5f; // Đặt lại tốc độ gốc
            isDashing = false;
            StopDashEffect();
        }
        else
        {
            _dashTime -= Time.deltaTime;
        }

        if (moveInput.x != 0)
        {
            if (moveInput.x > 0)
            {
                transform.localScale = new Vector3(-1, 1, 0);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 0);
            }
        }


    }

    void StopDashEffect()
    {
        if (dashEffectCoroutine != null)
        {
            StopCoroutine(dashEffectCoroutine);
            dashEffectCoroutine = null; // Reset biến sau khi dừng coroutine
        }
    }

    void StartDashEffect()
    {
        if (dashEffectCoroutine != null)
        {
            StopCoroutine(dashEffectCoroutine);
        }
        dashEffectCoroutine = StartCoroutine(DashEffectCoroutine()); // Gọi hàm đúng tên
    }

    IEnumerator DashEffectCoroutine()
    {
        while (isDashing)
        {
            // Tạo ghost tại vị trí nhân vật
            GameObject ghost = Instantiate(ghostEffect, transform.position, Quaternion.identity);

            // Cập nhật sprite của ghost từ nhân vật
            SpriteRenderer ghostSR = ghost.GetComponent<SpriteRenderer>();
            if (ghostSR != null)
            {
                ghostSR.sprite = characterSR.sprite; // Lấy sprite hiện tại của nhân vật
            }

            // 📌 Đảm bảo ghost quay đầu đúng hướng nhân vật
            ghost.transform.localScale = transform.localScale;

            // Xóa ghost sau 0.5s để tránh tràn bộ nhớ
            Destroy(ghost, 0.5f);

            yield return new WaitForSeconds(ghostDelaySeconds);
        }
    }

    public void KnockBack(Transform enemy, float force, float stunTime)
    {
        isKnockedBack = true;
        Vector2 direction = (transform.position - enemy.position).normalized;
        rb.linearVelocity = direction * force;
        StartCoroutine(KnockbackCouter(stunTime));
    }
    
    IEnumerator KnockbackCouter(float stunTime)
    {
        yield return new WaitForSeconds(stunTime);
        rb.linearVelocity = Vector2.zero;
        isKnockedBack = false;
    }

}
