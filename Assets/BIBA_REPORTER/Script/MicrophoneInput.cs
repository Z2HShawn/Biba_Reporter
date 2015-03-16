using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class MicrophoneInput : MonoBehaviour {
	public float sensitivity = 100;
	public float loudness = 0;

	void Start() 
	{
		/*
		if ( iPhone.generation == iPhoneGeneration.iPodTouch5Gen )
		{
			sensitivity = sensitivity * 1.5f;
		};
		*/
		audio.clip = Microphone.Start(null, true, 3, 44100);
		audio.loop = true; // Set the AudioClip to loop
		audio.mute = true; // Mute the sound, we don't want the player to hear it
		while (!(Microphone.GetPosition(null) > 0)){} // Wait until the recording has started
		audio.Play(); // Play the audio source!
	}
	
	void Update()
	{
		loudness = GetAveragedVolume() * sensitivity;
	}
	
	float GetAveragedVolume()
	{ 
		float[] data = new float[256];
		float a = 0;
		audio.GetOutputData(data,0);
		foreach(float s in data)
		{
			a += Mathf.Abs(s);
		}
		return a/256;
	}

	public void StopMicrophone()
	{
		Microphone.End(null);
	}
}