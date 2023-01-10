using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider))]

public class WinZone : MonoBehaviour
{
    bool isOpen;
    Animator animator;

    private void Start()
    {
        GameManager.instance.restartAction += Restart;
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out BallPlayer ballPlayer)) return;

        if (!isOpen)
        {
            animator.Play(Str.open);
            isOpen = true;
            return;
        }

        Debug.Log("Win");
        ballPlayer.Win();
    }

    void Restart()
    {
        isOpen = false;
        animator.Play(Str.idle);
    }
}
