using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Linq;
using UnityEngine.Networking;

public class SoundManager : MonoBehaviour
{
    private string audioPath = "GameAudioV1/";

    private AudioClip[] clips;
    private AudioSource globalSource;
    private List<AudioSource> runningLoops;
    private static SoundManager _instance;
    private static Dictionary<string, float> soundTimerDictionary;

    public static SoundManager instance { get { return _instance; } }

    void setSounds(string audioPath)
    {
        string [] paths = {"", "AsyncBellLoop/", "AwakeLoops/", "DreamState/", "Ending/", "Puzzles/"};

        clips = Resources.LoadAll(audioPath, typeof(AudioClip)).Cast<AudioClip>().ToArray();
    }

    private void Awake()
    {
        setSounds(audioPath);
        runningLoops = new List<AudioSource>();

        soundTimerDictionary = new Dictionary<string, float>();
        globalSource = gameObject.AddComponent<AudioSource>();
    }

    public static string curScene;
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        curScene = scene.name;
    }

    private void Start()
    {
        string curPath = audioPath;

        switch (curScene) 
        {
            case "MainMenu": 
            {
                Play("IntroDroneBuildup", 1, 0, false, false);
                break;
            }
            case "CourtRoom": 
            {
                Play("2-BarChimePulseLoop", 1, 0, true, false);
                Play("4-BarDronePulseLoop", 1, 0, true, false);
                Play("4-BarStringPulseLoop", 1, 0, true, false);
                Play("4-BarStringPulseLoop-Pitched1", 1, 0, true, false);
                Play("4-BarStringPulseLoop-Pitched2", 1, 0, true, false);
                break;
            }
            // case "CourtRoom": 
            // {
            //     Play("");
            //     break;
            // }
            default: break;
        }
    }

    public AudioClip getSoundByName (string name)
    {
        return clips.Where(x=>x.name == name).First();
    }
    public void Play(string name, float volume, float pitch, bool isLoop, bool hasCooldown)
    {
        AudioClip clip = clips.Where(x=>x.name == name).First();

        if(isLoop)
        {
            var newSource = gameObject.AddComponent<AudioSource>();
            newSource.volume = volume;
            newSource.loop = true;
            newSource.clip = clip;
            newSource.Play();
            runningLoops.Add(newSource);
            return;
        }

        globalSource.PlayOneShot(clip, volume);
    }

    public void Stop(string name) // for loops
    {
        foreach(var l in runningLoops){
            if(l.clip.name == name){
                l.Stop();
                runningLoops.RemoveAt(runningLoops.IndexOf(l));
                Destroy(l);
            }
        }
    }
}