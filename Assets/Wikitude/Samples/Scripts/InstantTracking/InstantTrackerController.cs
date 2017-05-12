using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Wikitude;

public class InstantTrackerController : SampleController 
{
	public GameObject ButtonDock;
	public GameObject InitializationControls;
    public List<GameObject> Trees;

    public List<GameObject> Start_Trees;

	public Text HeightLabel;
	public Text ScaleLabel;

	public InstantTracker Tracker;
	
	public List<Button> Buttons;
	public List<GameObject> Models;

	public Image ActivityIndicator;

	public Color EnabledColor = new Color(0.2f, 0.75f, 0.2f, 0.8f);
	public Color DisabledColor = new Color(1.0f, 0.2f, 0.2f, 0.8f);

	private float _currentDeviceHeightAboveGround = 1.0f;

	private MoveController _moveController;
	public GridRenderer _gridRenderer;

	private HashSet<GameObject> _activeModels = new HashSet<GameObject>();
	private InstantTrackingState _currentState = InstantTrackingState.Initializing;
	private bool _isTracking = false;

	public HashSet<GameObject> ActiveModels {
		get { 
			return _activeModels;
		}
	}

	private void Awake() {
		Application.targetFrameRate = 60;

		_moveController = GetComponent<MoveController>();
		_gridRenderer = GetComponent<GridRenderer>();
        TreesState(false);
        Buttons[0].gameObject.SetActive(false);
    }

	#region UI Events
	public void OnInitializeButtonClicked() {
        TreesState(true);
        Tracker.SetState(InstantTrackingState.Tracking);
	}

	public void OnHeightValueChanged(float newHeightValue) {
		_currentDeviceHeightAboveGround = newHeightValue;
		HeightLabel.text = string.Format("{0:0.##} m", _currentDeviceHeightAboveGround);
		Tracker.DeviceHeightAboveGround = _currentDeviceHeightAboveGround;
	}

	public void OnBeginDrag (int modelIndex) {
		if (_isTracking) {
			// Create object
			GameObject modelPrefab = Models[modelIndex];
			Transform model = Instantiate(modelPrefab).transform;
			_activeModels.Add(model.gameObject);
			// Set model position at touch position
			var cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			Plane p = new Plane(Vector3.up, Vector3.zero);
			float enter;
			if (p.Raycast(cameraRay, out enter)) {
				model.position = cameraRay.GetPoint(enter);
			}

			// Set model orientation to face toward the camera
			Quaternion modelRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(-Camera.main.transform.forward, Vector3.up), Vector3.up);
			model.rotation = modelRotation;

			_moveController.SetMoveObject(model);
		}
	}

	public override void OnBackButtonClicked() {
		if (_currentState == InstantTrackingState.Initializing) {
			base.OnBackButtonClicked();
		} else {
            TreesState(false);
            Tracker.SetState(InstantTrackingState.Initializing);
		}
	}
	#endregion

	#region Tracker Events
	public void OnEnterFieldOfVision(string target) {
		SetSceneActive(true);
    }

	public void OnExitFieldOfVision(string target) {
		SetSceneActive(false);
    }

	private void SetSceneActive(bool active) {

        _gridRenderer.enabled = active;

        foreach (var button in Buttons) {
			button.interactable = active;
		}

		foreach (var model in _activeModels) {
			model.SetActive(active);
		}

		ActivityIndicator.color = active ? EnabledColor : DisabledColor;
		
		_gridRenderer.enabled = active;
		_isTracking = active;
	}

	public void OnStateChanged(InstantTrackingState newState) {
		Tracker.DeviceHeightAboveGround = _currentDeviceHeightAboveGround;
		_currentState = newState;
		if (newState == InstantTrackingState.Tracking) {
			InitializationControls.SetActive(false);
            Buttons[0].gameObject.SetActive(true);
            ButtonDock.SetActive(true);
            TreesState(true);
        } else {
			foreach (var model in _activeModels) {
				Destroy(model);
			}
			_activeModels.Clear();

            InitializationControls.SetActive(true);
            Buttons[0].gameObject.SetActive(false);
            ButtonDock.SetActive(false);
		}
		_gridRenderer.enabled = true;
	}
    #endregion

    // sets the tree active states
    private void TreesState(bool state) {
        foreach (var tree in Trees) {
            tree.SetActive(state);
        }
    }
}
