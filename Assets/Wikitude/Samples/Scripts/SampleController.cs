using UnityEngine;
#if UNITY_5_3_OR_NEWER
using UnityEngine.SceneManagement;
#endif

public class SampleController : MonoBehaviour 
{
	public virtual void OnBackButtonClicked() {
#if UNITY_5_3_OR_NEWER
		SceneManager.LoadScene("Main Menu");
#else
		Application.LoadLevel("Main Menu");
#endif
	}

	// URLResource events
	public virtual void OnFinishLoading() {
		Debug.Log("URL Resource loaded successfully.");
	}

	public virtual void OnErrorLoading(int errorCode, string errorMessage) {
		Debug.LogError("Error loading URL Resource!\nErrorCode: " + errorCode + "\nErrorMessage: " + errorMessage);
	}

	// Tracker events
	public virtual void OnTargetsLoaded() {
		Debug.Log("Targets loaded successfully.");
	}

	public virtual void OnErrorLoadingTargets(int errorCode, string errorMessage) {
		Debug.LogError("Error loading targets!\nErrorCode: " + errorCode + "\nErrorMessage: " + errorMessage);
	}

	protected virtual void Update() {
		// Also handles the back button on Android
		if (Input.GetKeyDown(KeyCode.Escape)) { 
			OnBackButtonClicked();
		}
	}
}
