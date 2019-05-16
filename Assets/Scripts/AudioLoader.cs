using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using TMPro;
 
public class AudioLoader : Singleton {
 
    [SerializeField]
    TextMeshProUGUI debugTextfield;
    [SerializeField]
    TextMeshProUGUI loadingTextfield;

    string path;
    Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();

    bool loading = true;
    bool handledKeyPress = false;
 
    List<string> audioFilenames = new List<string>();

    string EXTENSION = ".ogg";
    AudioType AUDIO_TYPE = AudioType.OGGVORBIS;
 
    void Start ()
    {
        path = Application.persistentDataPath;
        
        Debug.Log("path = " + path);
        debugTextfield.text = path;
     
        if (Directory.Exists(path))
        {
            DirectoryInfo info = new DirectoryInfo(path);
         
            foreach (FileInfo item in info.GetFiles("*" + EXTENSION))
            {
                Debug.Log(item.Name + " file found!");

                audioFilenames.Add(item.Name);
            }
         
        }
        StartCoroutine(LoadAudioFile());
    }
 
    IEnumerator LoadAudioFile()
    {
        for (int i = 0; i <audioFilenames.Count; i++)
        {
            string fileName = audioFilenames[i];

            UnityWebRequest AudioFiles = UnityWebRequestMultimedia.GetAudioClip("file://" + path + string.Format("/{0}", fileName), AUDIO_TYPE);
            yield return AudioFiles.SendWebRequest();
            if(AudioFiles.isNetworkError)
            {
                Debug.Log(AudioFiles.error);
                Debug.Log(path + string.Format("/{0} FAILED TO LOAD", fileName));
            }
            else
            {
                AudioClip clip = DownloadHandlerAudioClip.GetContent(AudioFiles);
                clip.name = fileName;
                audioClips.Add(clip.name, clip);
                
                Debug.Log(path + string.Format("/{0} LOADED", fileName));
            }
        }

        loadingTextfield.text = "Done loading. Press a key to continue";
        loading = false;
    }

    void Update() {
        if (!loading && !handledKeyPress && Input.anyKey) {
            handledKeyPress = true;
            FindObjectOfType<Level>().LoadStartMenu();
        }
    }

    public AudioClip GetAudioClipByName(string name) {
        //we need to add the extension, since we want to be able to change that
        name += EXTENSION;

        Debug.Log("Requested: " + name);

        if(audioClips.ContainsKey(name)) {
            return audioClips[name];
        } else {
            Debug.Log(name + " - not found");

            return null;
        }
    }
}
 