using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CamManager : MonoBehaviour 
{
	public WebCamTexture 	webCamTex;
	public WebCamDevice[]  	webCamDevice;
	//public MeshRenderer 	meshRender;
	public RawImage 		rawImage;

	// Use this for initialization
	void Start () 
	{
		InitialCam();
	}
	
	// Update is called once per frame
	void Update () 
	{
		 
	}

	void InitialCam()
	{
		webCamDevice 	= WebCamTexture.devices; //get cam device
		webCamTex 		= new WebCamTexture(webCamDevice[0].name,500,500,24); 
		webCamTex.Play();

		rawImage = GetComponent<RawImage>();
		rawImage.texture = webCamTex;
		//meshRender 		= GetComponent<MeshRenderer>();
		//meshRender.material.mainTexture = webCamTex;
	}

	public void EnableTex()
	{
		//meshRender.enabled = true;
		webCamTex.Play();
	}

	public void DisableTex()
	{
		//meshRender.enabled = false;
		webCamTex.Stop();
	}
}
