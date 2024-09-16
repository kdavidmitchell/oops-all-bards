using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private AudioSource source;

    [SerializeField]
    private AudioMixerGroup musicMixerGroup;

    [SerializeField]
    private AudioMixerGroup sfxMixerGroup;

    [SerializeField]
    private List<SoundClip> musicTracks;

    [SerializeField]
    private List<SoundClip> sfxClips;

    private SoundClip trackPlaying = null;
    private SoundClip trackFading;
    private SoundClip sfxPlaying;

    private static AudioManager _instance;
    public static AudioManager Instance => _instance;

    // Singleton pattern
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        // set up the default values of each SoundClip based on what is set in the editor
        foreach (var track in this.musicTracks)
        {
            track.audioSource = this.gameObject.AddComponent<AudioSource>();
            track.audioSource.clip = track.clip;
            track.audioSource.volume = track.volume;
            track.audioSource.pitch = Random.Range(track.pitchLower, track.pitchUpper);
            track.audioSource.loop = track.loop;
            track.audioSource.outputAudioMixerGroup = this.musicMixerGroup;
            track.audioSource.Stop();
        }

        foreach (var clip in this.sfxClips)
        {
            clip.audioSource = this.gameObject.AddComponent<AudioSource>();
            clip.audioSource.clip = clip.clip;
            clip.audioSource.volume = clip.volume;
            clip.audioSource.pitch = Random.Range(clip.pitchLower, clip.pitchUpper);
            clip.audioSource.loop = clip.loop;
            clip.audioSource.outputAudioMixerGroup = this.sfxMixerGroup;
            clip.audioSource.Stop();
        }
    }

    // is there a cleaner way to do this? When a new scene is loaded add click sounds to every button
    // can't add onclick outside of runtime because AudioManager only exists in the StartMenu scene
    /**void ButtonSound()
    {
        PlaySoundEffect("click");
    }
    private void Update()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        List<GameObject> buttons = new List<GameObject>();
        buttons.AddRange((GameObject[])Resources.FindObjectsOfTypeAll(typeof(Button)));
        foreach(GameObject button in buttons)
        {
            button.GetComponent<Button>().onClick.AddListener(ButtonSound);
        }
    }**/

    // find the requested track and play it, stop the current song if one is already playing
    public int PlayMusicTrack(string title)
    {
        var track = this.musicTracks.Find(track => track.title == title);

        if (null == track)
        {
            Debug.Log("Sound track not found: " + title);
            return 1;
        }

        track.audioSource.Play();

        if (null != this.trackPlaying)
        {
            //Debug.Log("stopping: " + trackPlaying.clip.name);
            this.trackPlaying.audioSource.Stop();
        }

        this.trackPlaying = track;
        Debug.Log("playing: " + trackPlaying.clip.name);

        return 0; 
    }

    // stop playing the requested track
    public void StopMusicTrack(string title)
    {
        var track = this.musicTracks.Find(track => track.title == title);

        if (null == track)
        {
            Debug.Log("Sound track not found: " + title);
            return;
        }
        this.trackPlaying = null;
        track.audioSource.Stop();
    }

    // play the requested sound effect track. If no specific pitch or volume or given, play at the default values set on Awake
    public void PlaySoundEffect(string title, float pitch, float volume)
    {
        var track = this.sfxClips.Find(track => track.title == title);

        if (null == track)
        {
            Debug.Log("Sound track not found: " + title);
            return;
        }
        track.audioSource.volume = volume;
        track.audioSource.pitch = pitch;
        track.audioSource.PlayOneShot(track.clip);
    }
    public void PlaySoundEffect(string title)
    {
        var track = this.sfxClips.Find(track => track.title == title);

        if (null == track)
        {
            Debug.Log("Sound track not found: " + title);
            return;
        }
        track.audioSource.pitch = Random.Range(track.pitchLower, track.pitchUpper);
        track.audioSource.volume = track.volume;
        track.audioSource.PlayOneShot(track.clip);
    }

    // stop the requested sound effect track
    public void StopSoundEffect(string title)
    {
        var track = this.sfxClips.Find(track => track.title == title);

        if (null == track)
        {
            Debug.Log("Sound track not found: " + title);
            return;
        }

        track.audioSource.Stop();
    }

    public static void PlaySFX(string title)
    {
        Instance.PlaySoundEffect(title);
    }
    public static void StopSFX(string title)
    {
        Instance.StopSoundEffect(title);
    }
    public static void PlayMusic(string title)
    {
        Instance.PlayMusicTrack(title);
    }
    public static void StopMusic(string title)
    {
        Instance.StopMusicTrack(title);
    }
}

[System.Serializable]
public class SoundClip
{
    public AudioClip clip;

    [HideInInspector]
    public AudioSource audioSource;

    public string title;

    [Range(0f, 1f)]
    public float volume = 1.0f;

    [Range(0.1f, 3f)]
    public float pitchLower = 1.0f;
    [Range(0.1f, 3f)]
    public float pitchUpper = 1.0f;

    public bool loop = true;
}