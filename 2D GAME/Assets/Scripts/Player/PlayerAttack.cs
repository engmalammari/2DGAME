using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<EnemyAI>())
        {
            var enemy = other.GetComponent<EnemyAI>();
            enemy.TakeDamage(50);
        }
       
    }
}
