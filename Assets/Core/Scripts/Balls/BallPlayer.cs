using UnityEngine;

public class BallPlayer : Ball
{
    [SerializeField] Transform startPosition;

    [Header("Shot")]
    [SerializeField] GameObject prefabBallShot;
    [HideInInspector] public BallShot growBallShot;
    [SerializeField] float speedGrow;

    GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.instance;
        gameManager.restartAction += Restart;
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
        Destroy(growBallShot.gameObject, 10);
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
            StartCoroutine(GrowIn());
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

    public override void Die()
    {
        gameManager.isDead = true;
        rb.isKinematic = true;
        gameObject.SetActive(false);

        Debug.Log("Lose");
        gameManager.uI.OnMenu(0);
    }

    public void Win()
    {
        rb.isKinematic = true;
        gameObject.SetActive(false);

        gameManager.uI.OnMenu(1);
    }

    public void Restart()
    {
        transform.position = startPosition.position;
        transform.localScale = startPosition.localScale;
        rb.isKinematic = false;
        gameObject.SetActive(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out Block block)) StartCoroutine(GrowIn());
        Jump(collision.gameObject.layer);
    }
}