using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class JoystickControl : MonoBehaviour {

	public GazeInputModule pointer;
	public GameObject model;
	float rot = 0f;

	public AudioClip enterSnd;
	public AudioClip clickSnd;

	public Vector3 _pos;

	private bool connected = false;

	void Awake()
	{
		StartCoroutine(CheckForControllers());
	}

	// Use this for initialization
	void Start () {
		if(pointer == null){
			pointer = GameObject.Find("EventSystem").GetComponent<GazeInputModule>();
		}
	}

	IEnumerator CheckForControllers()
	{
		while(true)
		{
			var controllers = Input.GetJoystickNames();
			if (!connected && controllers.Length > 0)
			{
				connected = true;
				Debug.Log("Connected");
			}
			else if (connected && controllers.Length == 0)
			{
				connected = false;
				Debug.Log("Disconnected");
			}
			yield return new WaitForSeconds(1f);
		}
	}

	public void PointerEnter(int vec){
		if (vec == 0){
			TranslateHOR(0.1f);
		}

		if (vec == 1){
			TranslateHOR(-0.1f);
		}

		if (vec == 2){
			TranslateVER(0.1f);
		}

		if (vec == 3){
			TranslateVER(-0.1f);
		}
	}

	public void PointerClick(){
		//AudioSource.PlayClipAtPoint(clickSnd, Vector3.zero);
	}

	public void TranslateVER(float power){
		model.transform.Translate(0f, 0f, power);
	}

	public void TranslateHOR(float power){
		model.transform.Translate(0f, power, 0f);
	}
	
	public void ClickBut(int but){
		PointerClick();
		Debug.Log("Click button: " + but);
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log("Hor: " + Input.GetAxis("Horizontal"));
		Debug.Log("Ver: " + Input.GetAxis("Vertical"));
		Debug.Log("But0: " + Input.GetButton("Submit"));
		Debug.Log("But1: " + Input.GetButton("Cancel"));
		Debug.Log("But2: " + Input.GetButton("Fire3"));
		Debug.Log("But3: " + Input.GetButton("Jump"));

		if (Input.GetButton("Fire3")){
			pointer.EmulateClick();
		}

		if (model != null){

			//Vector3 vec = model.transform.localRotation.eulerAngles;
			model.transform.Translate(0f, Input.GetAxis("Horizontal") * 1f, 0f);
			model.transform.Translate(0f, 0f, Input.GetAxis("Vertical") * 1f);
			Debug.Log("VorldSpce: " + model.transform.localPosition);
		}

	}
}
