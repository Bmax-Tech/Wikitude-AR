using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoController : MonoBehaviour {

    private InstantTrackerController trackerScript;
    private GameObject ButtonParent;

	// Use this for initialization
	void Start () {
        trackerScript = GameObject.Find("Controller").gameObject.GetComponent<InstantTrackerController>();
        ButtonParent = GameObject.Find("Button Parent");

        trackerScript._gridRenderer.enabled = false;
        ButtonParent.SetActive(false);
	}

    void OnEnable() {
        trackerScript._gridRenderer.enabled = false;
        ButtonParent.SetActive(false);
    }

    void OnDisable() {
        ButtonParent.SetActive(true);
    }
}
