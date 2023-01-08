using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]

public abstract class Ball : MonoBehaviour
{
    [Header("Move")]
    public float speed;
    public float height;
    [HideInInspector] public Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public abstract void Move();

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 3)
        {
            rb.AddForce(new Vector3(0, height, 0), ForceMode.VelocityChange);
        }
    }
}
