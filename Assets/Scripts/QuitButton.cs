using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : MonoBehaviour
{
    public AudioClip clip;
    public void OnClickExit()
    {
        SoundManager.instance.SFXPlay("ButtonSfx", clip);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
        Debug.Log("게임 종료");
#endif 
    }
}
