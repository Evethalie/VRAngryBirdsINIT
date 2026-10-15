using UnityEngine;

public class SpeedBird : Bird
{
    [SerializeField] private float dashSpeed = 30f;
    [SerializeField] private GameObject dashEffect;

    // override means "use this instead of the version in Bird".
    protected override void Ability()
    {
        // .normalized keeps the direction we're already flying,
        // and throws away the speed. Then we set our own speed.
        body.linearVelocity = body.linearVelocity.normalized * dashSpeed;

        if (dashEffect != null)
        {
            Instantiate(dashEffect, transform.position, Quaternion.identity);
        }
    }
}
