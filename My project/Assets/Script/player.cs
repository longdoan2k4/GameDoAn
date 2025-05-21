using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;

public class player : MonoBehaviour
{
    public int facingDirection = 1;
    private Rigidbody2D rb;
    public Animator anim;

    public Player_Combat player_Combat;

    private bool isKnockedBack;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        anim = GetComponentInChildren<Animator>();
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
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            if (horizontal > 0 && transform.localScale.x < 0 || horizontal < 0 && transform.localScale.x > 0)
            {
                Flip();
            }

            anim.SetFloat("horizontal", Mathf.Abs(horizontal));
            anim.SetFloat("vertical", Mathf.Abs(vertical));

            rb.linearVelocity = new Vector2(horizontal, vertical) * StartsManager.Instance.speed;
        }

    }

    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
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
