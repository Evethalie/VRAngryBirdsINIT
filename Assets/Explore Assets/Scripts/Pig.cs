using System;
using UnityEditor.Build.Content;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Pig : MonoBehaviour
{
    [Header("Death Threshold")]
    [Tooltip("Impact speed from a bird collision needed to kill the pig")]
    [SerializeField] private float birdHitThreshold = 3.0f;

    [Tooltip("Impact from ANY collision - like falling")]
    [SerializeField] private float fallDamageThreshold = 6.0f;

    [SerializeField] private GameObject deathEffectPrefab;

    private Boolean isDead;

    private void OnCollisionEnter(Collision collision)
    {
        if (isDead) { return;  }

        float impactSpeed = collision.relativeVelocity.magnitude;
        bool hitByBird = collision.gameObject.CompareTag("Bird");

        if (hitByBird && impactSpeed >= birdHitThreshold)
        {
            Debug.Log($"[Pig] {name} killed by bird hit at {impactSpeed}");
            Die();
        }
        else if (impactSpeed >= fallDamageThreshold)
        {
            Debug.Log($"[Pig] {name} killed by falling at {impactSpeed}");
            Die();
        }
    }

    private void Die()
    {
        isDead = true;

        if (deathEffectPrefab != null)
        {
            Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
        }

        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnPigDied();
        }

        Destroy(gameObject);
    }
}
