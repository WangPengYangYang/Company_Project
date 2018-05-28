using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LikeUsButton : MonoBehaviour
{

	public Text descriptionText;
	public float animationVelocity;
    Vector3 initialScale;
    private void Start()
    {
    }
    // Use this for initialization
    void OnEnable ()
	{
		if(initialScale==Vector3.zero) initialScale = descriptionText.rectTransform.localScale;

        StartCoroutine(textAnimation ());

	}
	
	// Update is called once per frame
	void Update ()
	{
	
		if (FBLikeToUnlock.instance.Liked () && gameObject.activeInHierarchy) {
			gameObject.SetActive (false);
		}

	}





	IEnumerator textAnimation ()
	{

		while (true) {

			Vector3 targetScale = new Vector3 (initialScale.x - 0.1f, initialScale.y - 0.1f, initialScale.z - 0.1f);

			while (descriptionText.rectTransform.localScale != targetScale) {
				descriptionText.rectTransform.localScale = Vector3.MoveTowards (descriptionText.rectTransform.localScale, targetScale, animationVelocity * Time.deltaTime);
				yield return new WaitForEndOfFrame ();
			}


			while (descriptionText.rectTransform.localScale != initialScale) {
				descriptionText.rectTransform.localScale = Vector3.MoveTowards (descriptionText.rectTransform.localScale, initialScale, animationVelocity * Time.deltaTime);
				yield return new WaitForEndOfFrame ();
			}


			yield return new WaitForEndOfFrame ();
		}


	}




}
