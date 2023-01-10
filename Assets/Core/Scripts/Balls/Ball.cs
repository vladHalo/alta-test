using System.Collections;
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

    public abstract void Die();
    
    void OnFireWork()
    {
        var firework = GameManager.instance.firework;
        firework.transform.position = transform.position;
        firework.Play();
    }

    public void Jump(int layer)
    {
        if (layer == 3) rb.AddForce(new Vector3(0, height, 0), ForceMode.VelocityChange);
    }

    public IEnumerator GrowIn()
    {
        while (transform.localScale.x > 0)
        {
            transform.localScale -= new Vector3(.1f, .1f, .1f);
            yield return new WaitForEndOfFrame();
        }

        OnFireWork();
        Die();
    }
}
