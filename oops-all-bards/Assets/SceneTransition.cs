using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    [SerializeField]
    BlackFade fader;
    [SerializeField]
    string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        fader = GameObject.Find("BlackFade").GetComponent<BlackFade>();
    }

    private void OnTriggerEnter(Collider other)
    {
        fader.FadeToLevel(sceneName);
    }
}
