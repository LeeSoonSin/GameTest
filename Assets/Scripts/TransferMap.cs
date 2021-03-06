using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferMap : MonoBehaviour
{
    public string transferMapName; // 이동할 맵의 이름.
    private PlayerControl thePlayer;
    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerControl>(); // 다수의 객체 참조의 범위 차이다 겟컴포넌트하고.
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player2") // 닿은 것의 게임오브젝트 이름이 플레이어라면
        {
            if (GameManager.instance.MonsterCount[GameManager.instance.buildIndex -2] == 0 && GameManager.instance.isDoor == true)//Input.GetKeyDown(KeyCode.UpArrow)
            {
                StartCoroutine(UIManager.instance.SelectCard());
                //collision.gameObject.GetComponent<PlayerStat>().currentHP += 50;
                //UIManager.instance.SetHP(collision.gameObject.GetComponent<PlayerStat>().currentHP, collision.gameObject.GetComponent<PlayerStat>().HP);
            }
        }
    }

    public void NextStage()
    {
        if (!UIManager.instance.IsSelect)
        {
            thePlayer.currentMapName = transferMapName;
            SceneManager.LoadScene(transferMapName);
            GameManager.instance.isDoor = false;
            GameManager.instance.isRound = true;
            GameManager.instance.buildIndex += 1;
            UIManager.instance.IsSelect = false;
        }
    }
}
