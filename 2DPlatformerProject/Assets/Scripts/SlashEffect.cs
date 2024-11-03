using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashEffect : MonoBehaviour
{
    private void DestroyAnimatorEvent()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            EnemyHealthControl enemy = collider.GetComponent<EnemyHealthControl>();
            enemy.OnHit();
        }
    }
}
