using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public BlackFade fader;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlayMusicTrack("landoffantasy");
        AudioManager.Instance.PlaySoundEffect("guitarattack1");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        fader.FadeToLevel("CharacterCreation");
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
