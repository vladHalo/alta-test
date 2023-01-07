using NaughtyAttributes;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] Transform platform;
    [SerializeField] Vector2 minMaxWidth;

    [SerializeField] Transform finishPlatform;

    private void Start()
    {
        InitGame();
    }

    [Button("Restart")]
    void InitGame()
    {
        var width = Random.Range(minMaxWidth.x, minMaxWidth.y);
        platform.localScale = new Vector3(platform.localScale.x, platform.localScale.y, width);
        platform.position = new Vector3(platform.position.x, platform.position.y, width / 2 - .5f);

        var finishPosition = width / 2 + finishPlatform.localScale.z / 2 + platform.position.z;
        finishPlatform.position = new Vector3(finishPlatform.position.x, finishPlatform.position.y, finishPosition);
    }
}
