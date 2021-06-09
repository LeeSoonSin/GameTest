using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public AudioClip clip;
    public void OnClickStart()
    {
        SoundManager.instance.SFXPlay("ButtonSfx", clip);
        SceneManager.LoadScene("Story");
    }
}
