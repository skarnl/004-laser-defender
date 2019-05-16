using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using UnityEngine.UI;
 
public class AudioLoader : Singleton {
 
    [SerializeField]
    Text debugTextfield;

    string path;
    Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();
 
    List<string> audioFilenames = new List<string>();
 
    void Start ()
    {
        path = Application.persistentDataPath;
       
        Debug.Log("path = " + path);
     
        if (Directory.Exists(path))
        {
            DirectoryInfo info = new DirectoryInfo(path);
         
            foreach (FileInfo item in info.GetFiles("*.ogg"))
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

            UnityWebRequest AudioFiles = UnityWebRequestMultimedia.GetAudioClip("file://" + path + string.Format("/{0}", fileName),AudioType.OGGVORBIS);
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

        FindObjectOfType<Level>().LoadStartMenu();
    }

    public AudioClip GetAudioClipByName(string name) {
        if(audioClips.ContainsKey(name)) {
            return audioClips[name];
        } else {
            return null;
        }
    }
}
 