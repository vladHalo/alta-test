using NaughtyAttributes;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("Platform")]
    [SerializeField] Transform platform;
    [SerializeField] Vector2 minMaxWidth;

    [SerializeField] Transform finishPlatform;

    [Header("Ball")]
    public BallPlayer mainBall;

    private void Start()
    {
        InitPlatform();
    }

    private void Update()
    {
        Controller();
    }

    [Button("Restart")]
    void InitPlatform()
    {
        var width = Random.Range(minMaxWidth.x, minMaxWidth.y);
        platform.localScale = new Vector3(platform.localScale.x, platform.localScale.y, width);
        platform.position = new Vector3(platform.position.x, platform.position.y, width / 2 - .5f);

        var finishPosition = width / 2 + finishPlatform.localScale.z / 2 + platform.position.z;
        finishPlatform.position = new Vector3(finishPlatform.position.x, finishPlatform.position.y, finishPosition);
    }

    void Controller()
    {
        if (mainBall == null) return;

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
                    mainBall.GrowOutIn();
                    break;

                case TouchPhase.Moved:
                    mainBall.GrowOutIn();
                    break;

                case TouchPhase.Ended:
                    mainBall.growBallShot.Move();
                    break;
            }
        }
#endif
    }
}
