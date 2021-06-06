using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferMap : MonoBehaviour
{
    public string transferMapName; // 이동할 맵의 이름.
    private PlayerControl thePlayer;
    // Start is called before the first frame update
    GameManager gameManager;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        thePlayer = FindObjectOfType<PlayerControl>(); // 다수의 객체 참조의 범위 차이다 겟컴포넌트하고.
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player") // 닿은 것의 게임오브젝트 이름이 플레이어라면
        {
            if (gameManager.MonsterCount[gameManager.buildIndex -1] == 0 && gameManager.isDoor == true)//Input.GetKeyDown(KeyCode.UpArrow)
            {
                thePlayer.currentMapName = transferMapName;
                SceneManager.LoadScene(transferMapName);
                gameManager.isDoor = false;
                gameManager.isRound = true;
                gameManager.RoundNumber += 1;
            }
        }
    }
}
