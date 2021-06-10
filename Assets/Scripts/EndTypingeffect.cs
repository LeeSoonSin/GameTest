using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndTypingeffect : MonoBehaviour
{
    public Text[] tx;
    public GameObject grid;
    public Image image;
    private string[] m_text =
    {
        "�䱫�� óġ�ϰ� ���� �ٽ� ���⸦ ã�Ҵ�. \n",
        "�������� ��� ������� ���ƿ��� \n",
        "���� ��ħ�޻��� �����Դ�.\n",
        "        -The End- \n"
    };

    void Start()
    {
        StartCoroutine(_image());
        StartCoroutine(_typing());
        grid = GameObject.Find("Grid2");
        image = GameObject.Find("bg_n (2)").GetComponent<Image>();
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
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(0);
    }
    IEnumerator _image()
    {
        Color color = image.color;
        yield return new WaitForSeconds(1.5f);
        for (float f = 1.0f; f >= 0.0f; f -= 0.0001f)
        {
            color.a = f;
            image.color = color;
        }
    }
}