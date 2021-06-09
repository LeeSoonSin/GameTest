using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferMap : MonoBehaviour
{
    public string transferMapName; // �̵��� ���� �̸�.
    private PlayerControl thePlayer;
    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerControl>(); // �ټ��� ��ü ������ ���� ���̴� ��������Ʈ�ϰ�.
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player2") // ���� ���� ���ӿ�����Ʈ �̸��� �÷��̾���
        {
            if (GameManager.instance.MonsterCount[GameManager.instance.buildIndex -2] == 0 && GameManager.instance.isDoor == true)//Input.GetKeyDown(KeyCode.UpArrow)
            {
                thePlayer.currentMapName = transferMapName;
                SceneManager.LoadScene(transferMapName);
                GameManager.instance.isDoor = false;
                GameManager.instance.isRound = true;
                GameManager.instance.buildIndex += 1;
            }
        }
    }
}
