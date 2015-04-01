using UnityEngine;
using System.Collections;
using System.IO;

public class ScreenShot : MonoBehaviour 
{
	public Camera targetCamera;
	public Texture2D screenShot;
	public Rect rect;

	// Use this for initialization
	void Start () 
	{
		//CaptureCamera(targetCamera,rect);
	}
	
	// Update is called once per frame
	void Update () 
	{
		CaptureCamera(targetCamera,rect);
	}

	public Texture2D CaptureCamera(Camera cam, Rect rect)
	{
		//process: CAM -> RenderTexture -> Texture2D (-> *.png)

		//capture image from cam
		RenderTexture renderTexture = new RenderTexture((int)rect.width,(int)rect.height,0);
		cam.targetTexture = renderTexture;
		camera.Render();

		//read the image
		RenderTexture.active = renderTexture;
		screenShot = new Texture2D((int)rect.width,(int)rect.height,TextureFormat.RGB24,false);
		screenShot.ReadPixels(rect,0,0); //read from RenderTexture.active
		screenShot.Apply();

		//reset camera
		camera.targetTexture = null;
		RenderTexture.active = null;
		GameObject.Destroy(renderTexture);

		/*
		//Generate PNG file
		byte[] bytes = screenShot.EncodeToPNG();
		string filename = Application.dataPath + "/BibaReporter01.png";
		System.IO.File.WriteAllBytes(filename,bytes);
		*/

		Debug.Log("-------ScreenShot!-------" );

		return screenShot;
	}
}