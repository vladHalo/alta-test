using UnityEngine;

[RequireComponent(typeof(BoxCollider))]

public class WinZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out BallPlayer ballPlayer))
        {
            Debug.Log("Win");
            Destroy(other.gameObject);
        }
    }
}
