using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferMap : MonoBehaviour
{
    public string transferMapName; // �̵��� ���� �̸�.
    private PlayerControl thePlayer;
    // Start is called before the first frame update
    GameManager gameManager;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        thePlayer = FindObjectOfType<PlayerControl>(); // �ټ��� ��ü ������ ���� ���̴� ��������Ʈ�ϰ�.
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player") // ���� ���� ���ӿ�����Ʈ �̸��� �÷��̾���
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
