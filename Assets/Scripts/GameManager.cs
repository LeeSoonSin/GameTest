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
    public bool isRound = false; // 1스테이지 2스테이지 3스테이지마다 이것을 통해 변경함.
    public int RoundNumber;
    public int buildIndex;
    //public GameObject potal;

    public bool[] SelectedCard = new bool[9]; //고른 카드
    public bool Dead = false;
    public bool BossDead = false;

    private void Awake()
    {
        _instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 9; i++)
        {
            SelectedCard[i] = false;
        }
        RoundNumber = 2;
        DontDestroyOnLoad(this.gameObject);
        buildIndex = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        if (MonsterCount[buildIndex - RoundNumber] == 0) // isround 배열만들기
        {
            isDoor = true;
            //potal.gameObject.SetActive(true);
        }
        else
        {
            isDoor = false;
            //potal.gameObject.SetActive(false);
        }
        if(Dead)
        {
            Destroy(gameObject);
        }
        if(BossDead && SceneManager.GetActiveScene().buildIndex == 8)
        {
            Destroy(gameObject);
        }
    }
}
