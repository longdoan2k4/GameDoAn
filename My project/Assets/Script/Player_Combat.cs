using UnityEngine;

public class Player_Combat : MonoBehaviour
{
    public Transform attackPonit;
    public float weaponRange ;
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

            Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPonit.position, weaponRange, enemyLayer);

            if (enemies.Length > 0)
            {
                enemies[0].GetComponent<Enemy_Health>().ChangeHealth(-damage);
            }

            timer = cooldown;
        }
    }
    public void FinishAttacking()
    {
        anim.SetBool("isAttacking", false);
    }
}
