using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [Header("Platform")]
    public Transform platform;
    public Vector2 minMaxWidth;

    [SerializeField] Transform finishPlatform;

    [Header("Ball")]
    public BallPlayer mainBall;

    [Header("FireWork")]
    public ParticleSystem firework;

    [Header("Restart")]
    public UI uI;
    public System.Action restartAction;
    public bool isDead;

    private void Start()
    {
        InitPlatform();
    }

    private void Update()
    {
        Controller();
    }

    void InitPlatform()
    {
        var width = Random.Range(minMaxWidth.x, minMaxWidth.y);
        platform.localScale = new Vector3(mainBall.transform.localScale.x, 1, width);
        platform.position = new Vector3(platform.position.x, platform.position.y, width / 2 - .5f);

        var finishPosition = width / 2 + finishPlatform.localScale.z / 2 + platform.position.z;
        finishPlatform.position = new Vector3(finishPlatform.position.x, finishPlatform.position.y, finishPosition);
    }

    [Button("Restart", EButtonEnableMode.Playmode)]
    public void Restart()
    {
        isDead = false;
        restartAction?.Invoke();
        InitPlatform();
        StopAllCoroutines();
    }

    void Controller()
    {
        if (isDead) return;

#if (UNITY_EDITOR)
        if (Input.GetMouseButtonDown(0))
        {
            mainBall.InstanceBallShot();
        }
        else if (Input.GetMouseButton(0))
        {
            mainBall.GrowOutIn(platform);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            mainBall.MoveGrowBall();
        }
#else
        if (Input.touchCount > 0)
        {
            switch (Input.GetTouch(0).phase)
            {
                case TouchPhase.Began:
                    mainBall.InstanceBallShot();
                    break;

                case TouchPhase.Stationary:
                    mainBall.GrowOutIn(platform);
                    break;

                case TouchPhase.Moved:
                    mainBall.GrowOutIn(platform);
                    break;

                case TouchPhase.Ended:
                    mainBall.MoveGrowBall();
                    break;
            }
        }
#endif
    }
}
