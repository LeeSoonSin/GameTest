using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class typingeffect : MonoBehaviour
{
    public Text[] tx;
    public GameObject grid;
    private string[] m_text =
    {
        "평화로운 청구국(靑丘國)의 어느 산 속. \n",
        "산을 지키는 수호신인 구미호는 \n",
        "산에 변화가 생긴 것을 알아차리게 된다.\n",
        "산을 지켜주던 나무들은 \n",
        "사악한 요괴의 힘에 물들어져 있고, 산은 오염되어버렸다. \n",
        "산이 망가지는 걸 볼 수 없었던 수호신은 \n",
        "산을 망가뜨리는 주범인 요괴를 처리하러 간다. \n"
    };
    
    void Start()
    {
        StartCoroutine(_typing());
        grid = GameObject.Find("Grid");
    }

IEnumerator _typing()
    {
        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < tx.Length; i++)
        {
            for (int j = 0; j < m_text[i].Length; j++)
            {
                tx[i].text = m_text[i].Substring(0, j);
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(0.5f);
        }
        grid.SetActive(false);
        SceneManager.LoadScene("InGame");
    }
}
