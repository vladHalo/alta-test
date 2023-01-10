using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    [SerializeField] List<Item> items;
    [SerializeField] List<GameObject> listBlocks;

    GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.instance;
        gameManager.restartAction += Spawn;
        Spawn();
    }

    [Button("Spawn")]
    void Spawn()
    {
        for (int i = listBlocks.Count - 1; i >= 0; i--)
            Destroy(listBlocks[i]);
        listBlocks.Clear();

        for (int i = 0; i < items.Count; i++)
        {
            for (int j = 0; j < items[i].count; j++)
            {
                var item = Instantiate(items[i].prefab);
                item.name = $"{items[i].Name} {j}";

                var scale = Random.Range(1f, 3f);

                var positionY = scale + .3f;
                var positionX = Random.Range(-10, 10);
                var positionZ = Random.Range(40, gameManager.minMaxWidth.x - 30);

                item.transform.position = new Vector3(positionX, positionY, positionZ);
                item.transform.localScale = Vector3.one * scale;

                listBlocks.Add(item);
            }
        }
    }
}

[System.Serializable]
public class Item
{
    public string Name;
    public GameObject prefab;
    public int count;
}