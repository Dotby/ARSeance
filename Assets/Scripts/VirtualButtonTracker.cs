using UnityEngine;
using Vuforia;

public class VirtualButtonTracker : MonoBehaviour, IVirtualButtonEventHandler
{

	public ParticleSystem candleFire;
	public float startTime = 0f;
	Color normalColor;
	/// <summary>
	/// Called when the scene is loaded
	/// </summary>
	void Start() {

		normalColor = candleFire.startColor;
		
		VirtualButtonBehaviour[] vbs = GetComponentsInChildren<VirtualButtonBehaviour>();
		for (int i = 0; i < vbs.Length; ++i) {
			Debug.Log("Register vbutton.");
			// Register with the virtual buttons TrackableBehaviour
			vbs[i].RegisterEventHandler(this);
		}
	}

	/// <summary>
	/// Called when the virtual button has just been pressed:
	/// </summary>
	public void OnButtonPressed( VirtualButtonAbstractBehaviour vb) {
	//	if (pViewer == null) {return;}
		switch(vb.VirtualButtonName) {
		case "b1":
			break;
		case "b2":
			break;
		case "b10":
			SoundController.instance.Play("ghost", 0.8f, 1f);
			break;
		default:
			//throw new UnityException("Button not supported: " + vb.VirtualButtonName);
			break;
		}

		Debug.Log(vb.VirtualButtonName);
		candleFire.startColor = Color.red;;
	}

	/// <summary>
	/// Called when the virtual button has just been released:
	/// </summary>
	public void OnButtonReleased(VirtualButtonAbstractBehaviour vb) {

		candleFire.startColor = normalColor;

		if (vb.VirtualButtonName == "center"){
			//pViewer.VButton(1);
			Debug.Log("CENTER PUSH: " + (Time.time - startTime));
			if (Time.time - startTime >= 3f)
			{
				Application.LoadLevel("Scene");
			}
		}
	}
}