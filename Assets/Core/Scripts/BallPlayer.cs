using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class BallPlayer : MonoBehaviour
{
    [SerializeField] float speedMove;
    [SerializeField] float speedRotation;
    [SerializeField] float height;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rb.AddTorque(Vector3.right * speedRotation);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 3)
        {
            rb.AddForce(new Vector3(0, height, 0), ForceMode.VelocityChange);
        }
    }
}
