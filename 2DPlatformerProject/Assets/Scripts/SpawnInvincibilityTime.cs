using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class SpawnInvincibilityTime : MonoBehaviour
{
    [SerializeField]private Rigidbody2D r2d;
     private Vector3 vector;
    [SerializeField] private float jumpSpeed;
    IEnumerator Start()
    {
        vector = new Vector3(transform.right.x, transform.up.y, 0f);
        r2d.velocity = vector.normalized * jumpSpeed;
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
        yield return new WaitForSeconds(0.7f);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
    }
}
