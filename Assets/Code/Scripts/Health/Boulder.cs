using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoschingMachine
{
    [SelectionBase]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Boulder : MonoBehaviour
    {
        [SerializeField] float damageThreshold;
        [SerializeField] float damageScaling;

        new Rigidbody2D rigidbody;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.TryGetComponent(out IHasHealth health))
            {
                Vector2 momentum = collision.relativeVelocity * (collision.rigidbody.mass + rigidbody.mass);
                float damage = (momentum.magnitude - damageThreshold) * damageScaling;
                if (damage > 0.0f)
                {
                    print($"Damage: {damage}");
                    health.Damage(new DamageArgs(gameObject, damage));
                }
            }
        }
    }
}
