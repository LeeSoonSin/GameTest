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
        "��ȭ�ο� û����(������)�� ��� �� ��. \n",
        "���� ��Ű�� ��ȣ���� ����ȣ�� \n",
        "�꿡 ��ȭ�� ���� ���� �˾������� �ȴ�.\n",
        "���� �����ִ� �������� \n",
        "����� �䱫�� ���� ������� �ְ�, ���� �����Ǿ���ȴ�. \n",
        "���� �������� �� �� �� ������ ��ȣ���� \n",
        "���� �����߸��� �ֹ��� �䱫�� ó���Ϸ� ����. \n"
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
