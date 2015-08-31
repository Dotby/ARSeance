using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SymbolsCombinationsController : MonoBehaviour {

	const int _maxStack = 4;
	List<int> _stack = new List<int>();

	public Sprite[] runes;
	public Image runeSprite;
	
	List<int[]> _combos4 = new List<int[]>();
	List<int[]> _combos3 = new List<int[]>();
	List<int[]> _combos2 = new List<int[]>();
	List<int[]> _combos1 = new List<int[]>();

	public Animator runeObj;
	public Animator handsGhostObj;
	public GameObject ghostSpace;

	// Use this for initialization
	void Start () {
		ghostSpace.SetActive(false);
		_combos4.Add(new int[]{1, 2, 3, 4});
		_combos2.Add(new int[]{2, 2});
		_combos3.Add(new int[]{3, 2, 1});
		_combos1.Add(new int[]{6});
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void ClearTime(){
		_stack.Clear();
	}

	public void WaitAndTry(){
		Debug.Log("WAIT NEXT in 2 seconds...");
		Invoke("Try", 2f);
	}

	void Try(){
		if (!Try4Combo()){
			if (!Try3Combo()){
					if (!Try2Combo()){
						if (Try1Combo()){
							Debug.Log("1x combo!!!");
							SoundController.instance.Play("ghost", 1f, 1f);
						}
					}
					else{
						Debug.Log("2x combo!!!");
						handsGhostObj.Play("action");
						SoundController.instance.Play("voice", 1f, 1f);
					}
			}else{
				ghostSpace.SetActive(true);
				SoundController.instance.Play("singing", 1f, 1f);
				Debug.Log("3x combo!!!");

			}
		}
		else{
			Debug.Log("4x combo!!!");
			SoundController.instance.Play("piano", 1f, 1f);
		}

		_stack.Clear();
	}

	public void AddToStack(int _id){

		ghostSpace.SetActive(false);

		SoundController.instance.PlayRandomButton();
		runeSprite.sprite = runes[_id];
		runeObj.Play("runeUp");

		_stack.Add(_id);
		CancelInvoke("Try");

		if (_stack.Count > _maxStack){
			//TODO: shift list not clear!
			_stack.Clear();
		}

		string _stkDebug = "stack: ";
		foreach(int i in _stack){
			_stkDebug = _stkDebug + i.ToString() + " ";
		}
		
		Debug.Log(_stkDebug);
	}

	bool Try4Combo(){

		if (_stack.Count < 4) {return false;}
		bool found = false;

		for(int i = 0; i < _combos4.Count; i++){

			for (int d = 0; d < 4; d++){
				Debug.Log(_stack[d] + " == " + _combos4[i][d]);
				if (_stack[d] != _combos4[i][d]) {
					Debug.Log("BREAk");
					found = false;
				}
				else
				{
					found = true;
				}

			}
		}

		return found;
	}

	bool Try3Combo(){
		if (_stack.Count < 3) {return false;}
		bool found = false;
		
		for(int i = 0; i < _combos3.Count; i++){
			
			for (int d = 0; d < 2; d++){
				Debug.Log(_stack[d] + " == " + _combos3[i][d]);
				if (_stack[d] != _combos3[i][d]) {
					Debug.Log("BREAk");
					found = false;
				}
				else
				{
					found = true;
				}
				
			}
		}
		
		return found;
	}

	bool Try2Combo(){
		if (_stack.Count < 2) {return false;}
		bool found = false;
		
		for(int i = 0; i < _combos2.Count; i++){
			
			for (int d = 0; d < 2; d++){
				Debug.Log(_stack[d] + " == " + _combos2[i][d]);
				if (_stack[d] != _combos2[i][d]) {
					Debug.Log("BREAk");
					found = false;
				}
				else
				{
					found = true;
				}
				
			}
		}
		
		return found;
	}

	bool Try1Combo(){
			if (_stack.Count != 1) {return false;}
			bool found = false;
			
			for(int i = 0; i < _combos1.Count; i++){
				
					Debug.Log(_stack[0] + " == " + _combos1[i][0]);
					if (_stack[0] != _combos1[i][0]) {
						Debug.Log("BREAk");
						found = false;
					}
					else
					{
						found = true;
					}
			}
			
			return found;
	}
}
