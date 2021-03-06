﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using deVoid.Utils;

public class TestScript : MonoBehaviour {

	public Module module;
	public int count = 0;
	public int limit = 50;
	public Modulor leModulor;
	public List<Module> modules = new List<Module>();

	private void OnEnable()
	{
		Signals.Get<Module.ModuleStart>().AddListener(onModuleStart);
	}

	private void OnDisable()
	{
		Signals.Get<Module.ModuleStart>().RemoveListener(onModuleStart);
	}

	public void onModuleStart(Module _module)
	{	
		modules.Add(_module);
		if(count > limit)
			return;

		var rand = Random.value;
		if(Random.value < 0.5f){
			_module.divAxis = Module.axis.x;	
		}else{
			_module.divAxis = Module.axis.y;	
		}
		_module.divs = 2;
		Debug.Log(module.gameObject.name);
		Debug.Log(module.size);
		Debug.Log(module.transform.localPosition);
		count++;
	}

	public IEnumerator Refresh(){
		module.divs = 0;
		count = 0;
		yield return new WaitForSeconds(0.1f);
		var rand = Random.value;
		if(Random.value < 0.5f){
			module.divAxis = Module.axis.x;	
		}else{
			module.divAxis = Module.axis.y;	
		}
		module.divs = 2;
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetKeyDown(KeyCode.Space)){
			StartCoroutine(Refresh());
		}
		// int i = Random.Range(0,modules.Count -1);
		// if(i > 0 | i < modules.Count){
		// 	modules[i].divs = Random.Range(0,3);
		// }
	}
}
