using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SymbolsCombinationsController : MonoBehaviour {

	const int _maxStack = 4;
	List<int> _stack = new List<int>();
	
	List<int[]> _combos4 = new List<int[]>();
	List<int[]> _combos3 = new List<int[]>();
	List<int[]> _combos2 = new List<int[]>();
	List<int[]> _combos1 = new List<int[]>();

	// Use this for initialization
	void Start () {
		_combos4.Add(new int[]{1, 2, 3, 4});
		_combos2.Add(new int[]{2, 2});
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
			if (!Try2Combo()){

			}
			else{
				Debug.Log("2x combo!!!");
				SoundController.instance.Play("voice", 1f, 1f);
			}
		}else{
			Debug.Log("4x combo!!!");
			SoundController.instance.Play("piano", 1f, 1f);
		}

		_stack.Clear();
	}

	public void AddToStack(int _id){

		SoundController.instance.PlayRandomButton();

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
		return true;
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

	bool TryOne(){
		return true;
	}
}
