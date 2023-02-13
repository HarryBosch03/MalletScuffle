using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;

namespace BoschingMachine
{
    [System.Serializable]
    public sealed class PlayerMovement
    {
        [SerializeField] float moveForce;
        [SerializeField] float torque;

        [Space]
        [SerializeField] MovementFX fx;

        public void Move(Rigidbody2D rigidbody, Vector2 direction)
        {
            float angle = Mathf.Sin(-direction.x) * Mathf.Rad2Deg * (1.0f + Mathf.Max(0.0f, -direction.y));
            rigidbody.AddTorque((angle - rigidbody.rotation) * torque);

            Vector2 up = rigidbody.transform.up;
            float throttle = Mathf.Max(Vector2.Dot(up, direction), 0.0f);
            Vector2 force = up * throttle * moveForce;

            rigidbody.AddForce(force);

            fx.Apply(rigidbody, force, throttle);
        }
    }

    [System.Serializable]
    public class MovementFX
    {
        [SerializeField] ParticleSystem thruster;
        [SerializeField] float pps;

        [Space]
        [SerializeField] CinemachineImpulseSource impulseSource;
        [SerializeField] float shakeThreshold;
        [SerializeField] float shakeScale;

        Vector2 lastForce;
        Vector2 lastVelocity;

        public void Apply (Rigidbody2D rigidbody, Vector2 force, float throttle)
        {
            var emmision = thruster.emission;
            emmision.rateOverTime = pps * throttle;

            Vector2 vDiff = rigidbody.velocity - lastVelocity;
            Vector2 fDiff = (lastForce / rigidbody.mass * Time.deltaTime) - vDiff;
            if (fDiff.magnitude > shakeThreshold)
            {
                fDiff -= fDiff.normalized * shakeThreshold;
                fDiff *= shakeScale;
                impulseSource.GenerateImpulse(fDiff);
            }

            lastForce = force + (Vector2)Physics.gravity * rigidbody.mass;
            lastVelocity = rigidbody.velocity;
        }
    }
}
