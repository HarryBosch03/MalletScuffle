using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoschingMachine
{
    [System.Serializable]
    public class Health
    {
        [SerializeField] int maxHealth;
        [SerializeField] int currentHealth;
        [SerializeField] bool destroyOnDeath;

        public void Damage (GameObject gameObject, DamageArgs args)
        {
            currentHealth -= (int)args.damage;

            if (currentHealth <= 0)
            {
                Die(gameObject, args);
            }
        }

        public void Die (GameObject gameObject, DamageArgs args)
        {
            if (destroyOnDeath) Object.Destroy(gameObject);
            else gameObject.SetActive(false);
        }
    }

    public struct DamageArgs
    {
        public GameObject damager;
        public float damage;

        public DamageArgs(GameObject damager, float damage)
        {
            this.damager = damager;
            this.damage = damage;
        }
    }

    public interface IHasHealth
    {
        Health Health { get; }

        void Damage(DamageArgs args);
        void Die(DamageArgs args);
    }
}
