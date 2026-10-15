using UnityEngine;

public class SplitBird : Bird
{
    [Tooltip("A plain Bird prefab. Do NOT use a SplitBird here.")]
    [SerializeField] private GameObject splitPrefab;

    [SerializeField] private int   splitCount = 3;
    [SerializeField] private float spreadAngle = 15f;

    protected override void Ability()
    {
        if (splitPrefab == null) { return; }

        Vector3 velocity = body.linearVelocity;

        for (int i = 0; i < splitCount; i++)
        {
            // Spread them evenly around the current direction.
            // i=0 goes left, i=1 straight on, i=2 right.
            float angle = (i - (splitCount - 1) * 0.5f) * spreadAngle;

            // Quaternion.AngleAxis builds a rotation of `angle` degrees
            // around the up axis, then we apply it to our velocity.
            Vector3 spread = Quaternion.AngleAxis(angle, Vector3.up) * velocity;

            GameObject copy = Instantiate(splitPrefab, transform.position, transform.rotation);
            copy.GetComponent<Rigidbody>().linearVelocity = spread;
        }

        Destroy(gameObject);
    }
}
