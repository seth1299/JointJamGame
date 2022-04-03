using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Linq;

public class SoundManager : MonoBehaviour
{
    public string audioPath = "../GameAudioV1/";

    public Sound[] sounds;
    public FileInfo[] soundFiles;
    private static SoundManager _instance;
    private static Dictionary<string, float> soundTimerDictionary;

    public static SoundManager instance { get { return _instance; } }

    void setSounds(Sound[] sounds, string audioPath, FileInfo[] soundFiles)
    {
        string [] paths = {"", "AsyncBellLoop/", "AwakeLoops/", "DreamState/", "Ending/", "Puzzles/"};
        DirectoryInfo[] dirs = null;


        bool temp = new DirectoryInfo("../GameAudioV1/").Exists;
        Debug.Log(temp.ToString());
        return;


        // Grab each path as dir.
        for (int i = 0; i < 5; i++) dirs[i] = new DirectoryInfo((audioPath + paths[i]));

        // Get all files at each dir.
        for (int i = 0; i < 5; i++) soundFiles.Concat(dirs[i] ?.GetFiles().ToArray());

        // Store into data structure.
        for (int i = 0; i < soundFiles.Length; i++)
        {
            Debug.Log(soundFiles[i].ToString());
            // sounds[i].name = soundFiles[i].FullName;
            // sounds[i].clip = soundFiles[i];

        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        setSounds(sounds, audioPath, soundFiles);

        soundTimerDictionary = new Dictionary<string, float>();

        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.isLoop;

            if (sound.hasCooldown)
            {
                Debug.Log(sound.name);
                soundTimerDictionary[sound.name] = 0f;
            }
        }
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
            // case "MainMenu": 
            // {
            //     Play("IntroDroneBuildup", 0, 0, false, false);
            //     break;
            // }
            // case "CourtRoom": 
            // {
            //     curPath += "AwakeLoops/";
            //     Play(curPath + "2-BarChimePulseLoop", );
            //     break;
            // }
            // case "CourtRoom": 
            // {
            //     Play("");
            //     break;
            // }
            default: break;
        }
    }

    public Sound getSoundByName (string name)
    {
        Sound sound = null;
    
        for (int i = 0; i < sounds.Length; i++) if (sounds[i].name == name) return sounds[i];
        return sound;
    }
    public void Play(string name, float volume, float pitch, bool isLoop, bool hasCooldown)
    {
        Sound sound = getSoundByName(name);

        if (sound == null)
        {
            Debug.LogError("Sound " + name + " Not Found!");
            return;
        }

        if (!CanPlaySound(sound)) return;

        sound.name = name;
        sound.volume = volume;
        sound.pitch = pitch;
        sound.isLoop = isLoop;
        sound.hasCooldown = hasCooldown;
        sound.source.Play();
    }

    public void Stop(string name)
    {
        Sound sound = getSoundByName(name);

        if (sound == null)
        {
            Debug.LogError("Sound " + name + " Not Found!");
            return;
        }

        sound.source.Stop();
    }

    private static bool CanPlaySound(Sound sound)
    {
        if (soundTimerDictionary.ContainsKey(sound.name))
        {
            float lastTimePlayed = soundTimerDictionary[sound.name];

            if (lastTimePlayed + sound.clip.length < Time.time)
            {
                soundTimerDictionary[sound.name] = Time.time;
                return true;
            }

            return false;
        }

        return true;
    }
}
