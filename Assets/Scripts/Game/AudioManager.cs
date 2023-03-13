using UnityEngine.Audio;
using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour{

    public Sound[] sounds;
    private static List<Sound> staticSounds;
    private void Awake(){

        foreach (Sound s in sounds){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.loop = s.loop;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

            staticSounds.Add(s);
        }
    }
    	public void MuteMusic() {
			foreach (Sound s in staticSounds) {
				if (s.name == "Song") {
					s.source.volume = 0;
					return;
				}
			}
		}

		public void UnmuteMusic() {
			foreach (Sound s in staticSounds) {
				if (s.name == "Song") {
					s.source.volume = 0.65f;
					return;
				}
			}
		}
 
		public static void Play(string name) {
			foreach (Sound s in staticSounds) {
				if (s.name == name) {
					s.source.Play();
					return;
				}
			}
		}
       
		public static void Stop(string name) {
			foreach (Sound s in staticSounds) {
				if (s.name == name) {
					s.source.Stop();
					return;
				}
			}
		}


}

[System.Serializable]
public class Sound{

    public string name;
    public AudioClip clip;

    [Range(0f,1f)]
    public float volume;
    [Range(0.1f,3f)]
    public float pitch;

    public bool loop;

    public AudioSource source;
}
