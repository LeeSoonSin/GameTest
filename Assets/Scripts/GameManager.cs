using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int[] MonsterCount = { 1, 5, 7, 9, 11 };
    public bool isDoor = false;
    public bool isRound = false; // 1�������� 2�������� 3������������ �̰��� ���� ������.
    public int RoundNumber = 1;

    public int buildIndex;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        buildIndex = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        if (MonsterCount[buildIndex- RoundNumber] == 0) // isround �迭�����
        {
            isDoor = true;
        }
        else
        {
            isDoor = false;
        }
    }
}
