using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerBehaviour : MonoBehaviour
{
    public float mainVolume;
    public List<AudioSource> audioPlayers = new List<AudioSource>();
    public List<AudioClip> sounds = new List<AudioClip>();
    private Dictionary <string, AudioClip> soundDict = new Dictionary <string, AudioClip>();
    private AudioManagerBehaviour  self = null;

    public void Start()
    {
        if (self != null && self != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            self = this;

            foreach (AudioClip clip in sounds)
            {
                soundDict.Add(clip.ToString(), clip);
            }
            for (int i = 0; i < 10; i++)
            {

                this.gameObject.AddComponent<AudioSource>();
            }
            foreach (AudioSource audiosource in this.gameObject.GetComponents<AudioSource>())
            {
                this.audioPlayers.Add(audiosource);
            }
        }
    }

    //Sound Player für Single Sounds
    public void PlaySound(string name)
    {
        //Sucht angefragten Clip aus Soundliste heraus
        AudioClip requestedClip = soundDict[name + " (UnityEngine.AudioClip)"];
        //Check ob Sound in Soundliste vorhanden
        if (soundDict.ContainsKey(name+ " (UnityEngine.AudioClip)"))
        {
            foreach (AudioSource audioPlayer in audioPlayers)
            {
                //Prüft jede Audiosource auf verfügbarkeit
                if (audioPlayer.isPlaying == false)
                {
                    //Wenn frei wird der angefragte Ton EINMAL abgespielt
                    //ACHTUNG: das Attribut audioPlayer.clip wird durch  PlayOneShot() nicht initialisiert und bleib  null
                    audioPlayer.PlayOneShot(requestedClip, mainVolume); 
                    break;
                }
                else
                {
                    if (audioPlayer.clip == requestedClip)
                    {
                        //audioPlayer.Stop();
                        //audioPlayer.PlayOneShot(requestedClip);
                        break;
                    }
                }
            }
        }
        else
        {
        }
    }

    //SoundPlayer für loopende Sounds
    public void PlaySound(string name, bool repeat)
    {
        //Sucht angefragten Clip aus Soundliste heraus
        AudioClip requestedClip = soundDict[name + " (UnityEngine.AudioClip)"];
        //Check ob Sound in Soundliste vorhanden
        if (soundDict.ContainsKey(name + " (UnityEngine.AudioClip)"))
        {
            foreach (AudioSource audioPlayer in audioPlayers)
            {
                //Prüft jede Audiosource auf verfügbarkeit
                if (audioPlayer.isPlaying == false)
                {
                    //Wenn ja wird das Attribut clip von der Audiosource mit dem angefragten SoundClip initialisiert
                    audioPlayer.clip = requestedClip;
                    //Das Attribut lopp auf den übergebenen repeat Paramter gesetzt
                    audioPlayer.loop = repeat;
                    //Der Ton abgespielt
                    audioPlayer.Play();
                    break;
                }
                else
                {
                    //Wenn nicht verfügbar wird das loop Attribut auf den übergeben repeat Paramter gesetzt
                    if (audioPlayer.clip == requestedClip)
                    {
                        audioPlayer.loop = repeat;
                        break;
                    }
                }
            }
        }
    }
    public void StopSound(string name)
    {
        foreach (AudioSource audioPlayer in audioPlayers)
        {
            if (audioPlayer.isPlaying && audioPlayer.clip != null)
            { 
                if(audioPlayer.clip == soundDict[name + " (UnityEngine.AudioClip)"])
                {
                    Debug.Log("Trying to Stop " + audioPlayer.clip);
                    audioPlayer.loop = false;
                    audioPlayer.Stop();
                    audioPlayer.clip = null;
                }
                else
                {
                    Debug.Log("Kann Sound " + audioPlayer.clip + " nicht Stoppen");
                }
            }
        }
    }
    public void SetPitch(string name, float pitch)
    {
        //Sucht angefragten Clip aus Soundliste heraus
        AudioClip requestedClip = soundDict[name + " (UnityEngine.AudioClip)"];
        if (soundDict.ContainsKey(name + " (UnityEngine.AudioClip)"))
        {
            foreach (AudioSource audioPlayer in audioPlayers)
            {
                //Prüft jede Audiosource auf Aktivität
                if (audioPlayer.isPlaying && audioPlayer.clip != null)
                {
                    //Wenn Aktiv wird Übereinstimmung mit dem Angefragten Ton geprüft
                    if (audioPlayer.clip == requestedClip)
                    {
                        //Wenn ja wird der Pitch auf den übergebenen Pitch Paramater gesetzt
                        audioPlayer.pitch = pitch;
                        break;
                    }
                }
                else
                {
                    return;
                }
            }
        }
    }
}
