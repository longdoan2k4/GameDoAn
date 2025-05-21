using UnityEngine;

public class Player_Combat : MonoBehaviour
{
    public Transform attackPonit;
    public float weaponRange = 1;
    public float knockbackForce = 50;
    public float knockbackTime = 0.15f;
    public float stunTime = 0.5f;

    public LayerMask enemyLayer;
    public int damage = 1;

    public Animator anim;
    public float cooldown = 1;
    private float timer;

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }
    public void Attack()
    {
        if (timer <= 0)
        {
            anim.SetBool("isAttacking", true);


            timer = cooldown;
        }
    }

    public void DealDamage()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPonit.position, weaponRange, enemyLayer);

        if (enemies.Length > 0)
        {
            enemies[0].GetComponent<Enemy_Health>().ChangeHealth(-damage);
            enemies[0].GetComponent<Enemy_Knockback>().Knockback(transform, knockbackForce, knockbackTime,stunTime);
        }
    }

    public void FinishAttacking()
    {
        anim.SetBool("isAttacking", false);
    }

    public void OnDrawGizmosSelected()
    {
        if (attackPonit == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPonit.position, weaponRange);
    }

}
