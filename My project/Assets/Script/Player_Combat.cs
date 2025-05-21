using UnityEngine;

public class Player_Combat : MonoBehaviour
{
    public Transform attackPonit;
    public LayerMask enemyLayer;

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
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPonit.position, StartsManager.Instance.weaponRange, enemyLayer);
        
        if (enemies.Length > 0)
        {
            enemies[0].GetComponent<Enemy_Health>().ChangeHealth(-StartsManager.Instance.damage);
            enemies[0].GetComponent<Enemy_Knockback>().Knockback(transform, StartsManager.Instance.knockbackForce, StartsManager.Instance.knockbackTime,StartsManager.Instance.stunTime);
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
        Gizmos.DrawWireSphere(attackPonit.position, StartsManager.Instance.weaponRange);
    }

}
