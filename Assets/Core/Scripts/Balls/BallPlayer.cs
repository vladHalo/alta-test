using UnityEngine;

public class BallPlayer : Ball
{
    [Header("Shot")]
    [SerializeField] GameObject prefabBallShot;
    [HideInInspector] public BallShot growBallShot;
    [SerializeField] float speedGrow;

    GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.instance;
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.localScale.x / 2);
    }

    void FixedUpdate()
    {
        Move();
    }

    public override void Move()
    {
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speed);
    }

    public void InstanceBallShot()
    {
        growBallShot = Instantiate(prefabBallShot, ShotStartPosition(prefabBallShot.transform), Quaternion.identity).GetComponent<BallShot>();
        growBallShot.transform.localScale = Vector3.zero;
    }

    Vector3 ShotStartPosition(Transform ball)
    {
        var startPosition = transform.localScale.x / 2 + ball.localScale.x / 2 + transform.position.z;
        return new Vector3(transform.position.x, transform.position.y, startPosition);
    }

    public void GrowOutIn(Transform platform)
    {
        if (transform.localScale.x <= .1f)
        {
            platform.localScale = Vector3.zero;
            MoveGrowBall();
            Die();
            return;
        }

        transform.localScale -= Vector3.one * speedGrow;
        growBallShot.transform.localScale += Vector3.one * speedGrow;
        growBallShot.transform.position = ShotStartPosition(growBallShot.transform);
        platform.localScale = new Vector3(transform.localScale.x, platform.localScale.y, platform.localScale.z);
    }

    public void MoveGrowBall()
    {
        if (growBallShot == null) return;
        growBallShot.Move();
        growBallShot = null;
    }

    void Die()
    {
        gameManager.mainBall = null;
        Destroy(gameObject);

        Debug.Log("Lose");
    }
}