using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;

    public static GameManager instance { get { return _instance; } }


    public int[] MonsterCount = { 1, 5, 7, 9, 11 };
    public bool isDoor = false;
    public bool isRound = false; // 1�������� 2�������� 3������������ �̰��� ���� ������.
    public int RoundNumber;
    public int buildIndex;
    //public GameObject potal;

    private void Awake()
    {
        _instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        RoundNumber = 2;
        DontDestroyOnLoad(this.gameObject);
        buildIndex = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        if (MonsterCount[buildIndex - RoundNumber] == 0) // isround �迭�����
        {
            isDoor = true;
            //potal.gameObject.SetActive(true);
        }
        else
        {
            isDoor = false;
            //potal.gameObject.SetActive(false);
        }
    }
}
