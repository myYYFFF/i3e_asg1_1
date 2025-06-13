using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 1;

    public void SetDamage(int dmg)
    {
        damage = dmg;
    }

    private void OnCollisionEnter(Collision collision)
    {
 
        MobHealth mob = collision.collider.GetComponent<MobHealth>();
        if (mob != null)
        {
            mob.TakeDamage(damage);
        }

   
        DestructibleObject destructible = collision.collider.GetComponent<DestructibleObject>();
        if (destructible != null)
        {
            destructible.TakeDamage(damage);
        }

        Destroy(gameObject); 
    }
}
