using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BoschingMachine
{
    [SelectionBase]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class PlayerAvatar : MonoBehaviour, IHasHealth
    {
        [SerializeField] InputActionAsset inputAsset;

        [Space]
        [SerializeField] PlayerMovement movement;
        [SerializeField] Health health;

        new Rigidbody2D rigidbody;

        InputActionMap playerMap;
        InputAction move;

        public Health Health => health;

        public void Damage(DamageArgs args) => health.Damage(gameObject, args);
        public void Die(DamageArgs args) => health.Die(gameObject, args);

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();

            inputAsset.Enable();

            playerMap = inputAsset.FindActionMap("Player");
            move = playerMap.FindAction("move");
        }

        private void FixedUpdate()
        {
            movement.Move(rigidbody, move.ReadValue<Vector2>());
        }
    }
}
