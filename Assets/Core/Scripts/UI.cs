using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] Text scoreText;
    public GameObject[] menus;

    int score;
    public int Score
    {
        get { return score; }
        set
        {
            scoreText.text = $"{value}";
            score = value;
        }
    }

    public void OnMenu(int index)
    {
        StartCoroutine(On(index));
    }

    public void OffMenu()
    {
        menus[0].SetActive(false);
        menus[1].SetActive(false);
    }

    IEnumerator On(int index)
    {
        yield return new WaitForSeconds(.5f);
        if (index == 1) Score += 10;
        menus[index].SetActive(true);
    }
}
