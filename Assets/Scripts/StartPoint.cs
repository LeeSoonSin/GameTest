using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    public string startPoint;// ���� �̵��Ҷ� �÷��̾ ���۵� ��ġ.
    private Move thePlayer;
    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<Move>();
        if(startPoint == thePlayer.currentMapName)
        {
            thePlayer.transform.position = this.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}