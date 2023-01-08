using UnityEngine;

public class BallShot : Ball
{
    public override void Move()
    {
        rb.AddForce(Vector3.forward * speed, ForceMode.Force);
    }
}
