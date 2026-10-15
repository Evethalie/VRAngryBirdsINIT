using UnityEngine;

public class BombBird : Bird
{
    [SerializeField] private float radius     = 3f;
    [SerializeField] private float force      = 700f;
    [SerializeField] private float upwardBias = 1f;
    [SerializeField] private GameObject blastEffect;

    protected override void Ability()
    {
        if (blastEffect != null)
        {
            GameObject effect = Instantiate(blastEffect, transform.position, Quaternion.identity);
            Destroy(effect, 3f);
        }

        // OverlapSphere gives you every collider inside a sphere.
        Collider[] caught = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider hit in caught)
        {
            // attachedRigidbody finds the Rigidbody a collider belongs to,
            // which might be on a parent. Safer than GetComponent.
            Rigidbody hitBody = hit.attachedRigidbody;

            if (hitBody != null)
            {
                hitBody.AddExplosionForce(force, transform.position, radius, upwardBias);
            }
        }

        Destroy(gameObject);
    }
}
