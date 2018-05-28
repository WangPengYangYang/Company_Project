using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.IO;

public class NativeShare : MonoBehaviour
{

	public static Texture2D screenshotTex;


	public static IEnumerator ShareScreenshotWithText (string text)
	{
		yield return new WaitForEndOfFrame ();
		TakeScreenshot ();
		SaveScreenshotAsFile ();

		while (!screenshotSaved ()) {
			yield return null;
		}

		Share (text, getPath (), "");
	}

	public static string getPath ()
	{
		// where we want to save the image
		return Application.persistentDataPath + "/stackScreenshot.png";
	}

	public static void TakeScreenshot ()
	{
		//takes the screenshot, but doesn't save a file. It's stored as a Texture2D instead
		screenshotTex = new Texture2D (Screen.width, Screen.height, TextureFormat.RGB24, false);
		screenshotTex.ReadPixels (new Rect (0, 0, Screen.width, Screen.height), 0, 0);
		screenshotTex.Apply ();
	}

	public static void SaveScreenshotAsFile ()
	{
		//saves a PNG file to the path specified above
		byte[] bytes = screenshotTex.EncodeToPNG ();
		File.WriteAllBytes (getPath (), bytes);
	}

	public static bool screenshotSaved ()
	{
		//it's not enough to just check that the file exists, since it doesn't mean it's finished saving
		//we have to check if it can actually be opened
		Texture2D image;
		image = new Texture2D (Screen.width, Screen.height);
		bool imageLoadSuccess = image.LoadImage (System.IO.File.ReadAllBytes (getPath ()));
		Destroy (image);
		return imageLoadSuccess;
	}


	public static void Share (string shareText, string imagePath, string url, string subject = "")
	{
#if UNITY_ANDROID
		AndroidJavaClass intentClass = new AndroidJavaClass ("android.content.Intent");
		AndroidJavaObject intentObject = new AndroidJavaObject ("android.content.Intent");
		
		intentObject.Call<AndroidJavaObject> ("setAction", intentClass.GetStatic<string> ("ACTION_SEND"));
		AndroidJavaClass uriClass = new AndroidJavaClass ("android.net.Uri");
		AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject> ("parse", "file://" + imagePath);
		intentObject.Call<AndroidJavaObject> ("putExtra", intentClass.GetStatic<string> ("EXTRA_STREAM"), uriObject);
		intentObject.Call<AndroidJavaObject> ("setType", "image/png");
		
		intentObject.Call<AndroidJavaObject> ("putExtra", intentClass.GetStatic<string> ("EXTRA_TEXT"), shareText);
		
		AndroidJavaClass unity = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
		AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject> ("currentActivity");
		
		AndroidJavaObject jChooser = intentClass.CallStatic<AndroidJavaObject> ("createChooser", intentObject, subject);
		currentActivity.Call ("startActivity", jChooser);
#elif UNITY_IOS
		CallSocialShareAdvanced(shareText, subject, url, imagePath);
#else
		Debug.Log("No sharing set up for this platform.");
#endif
	}

	#if UNITY_IOS
	public struct ConfigStruct
	{
		public string title;
		public string message;
	}

	[DllImport ("__Internal")] private static extern void showAlertMessage(ref ConfigStruct conf);
	
	public struct SocialSharingStruct
	{
		public string text;
		public string url;
		public string image;
		public string subject;
	}
	
	[DllImport ("__Internal")] private static extern void showSocialSharing(ref SocialSharingStruct conf);
	
	public static void CallSocialShare(string title, string message)
	{
		ConfigStruct conf = new ConfigStruct();
		conf.title  = title;
		conf.message = message;
		showAlertMessage(ref conf);
	}

	public static void CallSocialShareAdvanced(string defaultTxt, string subject, string url, string img)
	{
		SocialSharingStruct conf = new SocialSharingStruct();
		conf.text = defaultTxt; 
		conf.url = url;
		conf.image = img;
		conf.subject = subject;
		
		showSocialSharing(ref conf);
	}
#endif
}
