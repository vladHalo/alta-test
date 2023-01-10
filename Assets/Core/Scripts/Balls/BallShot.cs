using UnityEngine;

public class BallShot : Ball
{
    public override void Move()
    {
        rb.AddForce(Vector3.forward * speed, ForceMode.Force);
    }

    public override void Die()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out Block block))
        {
            var collider = GetComponent<SphereCollider>();
            var rb = GetComponent<Rigidbody>();

            collider.isTrigger = true;
            collider.radius *= transform.localScale.x + 1;
            rb.isKinematic = true;

            block.Break();
            StartCoroutine(GrowIn());
            return;
        }
        Jump(collision.gameObject.layer);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent(out Block block))
        {
            block.Break();
        }
    }
}
