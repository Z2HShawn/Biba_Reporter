using UnityEngine;
using System.Collections;
using System.IO;


namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("BibaReporter")]
	
	public class Reporter_ScreenShot : FsmStateAction
	{

		[Tooltip("Just show the captured image")]
		public Texture2D screenShotPreview;

		[RequiredField]
		[Tooltip("The one capturing image")]
		public Camera targetCamera;

		[RequiredField]
		[Tooltip("The range of screenshot")]
		public Rect rect;

		[RequiredField]
		[Tooltip("A  FSM Texture which store the screenshot")]
		public FsmTexture StoreTexture;


		public override void Reset()
		{
			StoreTexture = null;
		}
		
		public override void OnEnter()
		{
			StoreTexture.Value = CaptureCamera(targetCamera,rect);

			Finish();
		}
				
		public override void OnUpdate()
		{

		}
		
		public override void OnExit ()
		{
		
		}

		public Texture2D CaptureCamera(Camera cam, Rect rect)
		{
			//process: CAM -> RenderTexture -> Texture2D (-> *.png)
			
			//capture image from cam
			RenderTexture renderTexture = new RenderTexture((int)rect.width,(int)rect.height,0);
			cam.targetTexture = renderTexture;
			targetCamera.Render();
			
			//read the image
			RenderTexture.active = renderTexture;
			screenShotPreview = new Texture2D((int)rect.width,(int)rect.height,TextureFormat.RGB24,false);
			screenShotPreview.ReadPixels(rect,0,0); //read from RenderTexture.active
			screenShotPreview.Apply();
			
			//reset camera
			targetCamera.targetTexture = null;
			RenderTexture.active = null;
			GameObject.Destroy(renderTexture);
			
			/*
			//Generate PNG file
			byte[] bytes = screenShot.EncodeToPNG();
			string filename = Application.dataPath + "/BibaReporter01.png";
			System.IO.File.WriteAllBytes(filename,bytes);
			*/
			
			//Debug.Log("-------ScreenShot!-------" );
			
			return screenShotPreview;
		}
	}
}



