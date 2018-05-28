using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void ShowPopUp(string title,string dialogmessage){
		
		ObliusNativePopup popup = new ObliusNativePopup (title, dialogmessage);
		popup.AddAction ("OK", () => {Debug.Log("ok pressed");});
		popup.AddDismissListener (() => {Debug.Log("dismiss listener");});
		popup.Show ();

}

    public static string getDurationString(float seconds)
    {

        int hours = (int)seconds / 3600;
        int minutes = (int)(seconds % 3600) / 60;
        seconds =(int) seconds % 60;

        if (hours != 0)
        {

            return hours + ":" + minutes + ":" + seconds;
        }
        else
        {
            return minutes + ":" + seconds;

        }
    }

    public  static string FloatToTime (float toConvert, string format){
		switch (format){
		case "00.0":
			return string.Format("{0:00}:{1:0}", 
				Mathf.Floor(toConvert) % 60,//seconds
				Mathf.Floor((toConvert*10) % 10));//miliseconds
			break;
		case "#0.0":
			return string.Format("{0:#0}:{1:0}", 
				Mathf.Floor(toConvert) % 60,//seconds
				Mathf.Floor((toConvert*10) % 10));//miliseconds
			break;
		case "00.00":
			return string.Format("{0:00}:{1:00}", 
				Mathf.Floor(toConvert) % 60,//seconds
				Mathf.Floor((toConvert*100) % 100));//miliseconds
			break;
		case "00.000":
			return string.Format("{0:00}:{1:000}", 
				Mathf.Floor(toConvert) % 60,//seconds
				Mathf.Floor((toConvert*1000) % 1000));//miliseconds
			break;
		case "#00.000":
			return string.Format("{0:#00}:{1:000}", 
				Mathf.Floor(toConvert) % 60,//seconds
				Mathf.Floor((toConvert*1000) % 1000));//miliseconds
			break;
		case "#0:00":
			return string.Format("{0:#0}:{1:00}",
				Mathf.Floor(toConvert / 60),//minutes
				Mathf.Floor(toConvert) % 60);//seconds
			break;
            case "custom":
                return string.Format("%02d:%02d:%02d", (int)toConvert / 3600, (int)(toConvert % 3600) / 60, (int)toConvert % 60);


                break;
            case "#00:00":
			return string.Format("{0:#00}:{1:00}", 
				Mathf.Floor(toConvert / 60),//minutes
				Mathf.Floor(toConvert) % 60);//seconds
			break;
		case "0:00.0":
			return string.Format("{0:0}:{1:00}.{2:0}",
				Mathf.Floor(toConvert / 60),//minutes
				Mathf.Floor(toConvert) % 60,//seconds
				Mathf.Floor((toConvert*10) % 10));//miliseconds
			break;
		case "#0:00.0":
			return string.Format("{0:#0}:{1:00}.{2:0}",
				Mathf.Floor(toConvert / 60),//minutes
				Mathf.Floor(toConvert) % 60,//seconds
				Mathf.Floor((toConvert*10) % 10));//miliseconds
			break;
		case "0:00.00":
			return string.Format("{0:0}:{1:00}.{2:00}",
				Mathf.Floor(toConvert / 60),//minutes
				Mathf.Floor(toConvert) % 60,//seconds
				Mathf.Floor((toConvert*100) % 100));//miliseconds
			break;
		case "#0:00.00":
			return string.Format("{0:#0}:{1:00}.{2:00}",
				Mathf.Floor(toConvert / 60),//minutes
				Mathf.Floor(toConvert) % 60,//seconds
				Mathf.Floor((toConvert*100) % 100));//miliseconds
			break;
		case "0:00.000":
			return string.Format("{0:0}:{1:00}.{2:000}",
				Mathf.Floor(toConvert / 60),//minutes
				Mathf.Floor(toConvert) % 60,//seconds
				Mathf.Floor((toConvert*1000) % 1000));//miliseconds
			break;
		case "#0:00.000":
			return string.Format("{0:#0}:{1:00}.{2:000}",
				Mathf.Floor(toConvert / 60),//minutes
				Mathf.Floor(toConvert) % 60,//seconds
				Mathf.Floor((toConvert*1000) % 1000));//miliseconds
			break;
		}
		return "error";
	}
}
