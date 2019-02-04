﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piloti : MonoBehaviour {

	public List<Vector3> points = new List<Vector3>();
	public List<GameObject> gos = new List<GameObject>();
	public float distance = 0;
	public bool loop = false;
	public Module module;
	public NeighborCheck neighborCheck;
	public float width = 1;
	public List<Vector3> yNegative = new List<Vector3>();
	public static int piltoiGroups = 0;
	public static List<List<string>> groups = new List<List<string>>();
	
	// Use this for initialization
	void Start () {

		module = gameObject.GetComponent<Module>();
		neighborCheck = gameObject.GetComponent<NeighborCheck>();
		var renderer = module.meshGo.GetComponent<MeshRenderer>();
		renderer.material.color = Color.red;
		GroundPlan.instance.pilotis.Add(this);
		piltoiGroups++;
		neighborCheck.tags.Add("Piloti" + piltoiGroups);
		// module.visible = false;
		// var piloti = Resources.Load<GameObject>("Prefabs/Piloti");
		// var pos = transform.position;
		// var size = module.size;
		// pos.y = size.y;
		// piloti = Instantiate(piloti, pos, Quaternion.identity, transform);
		// var pilotiScript = piloti.GetComponent<Pilotis>();
		// // get the shortest side
		// pilotiScript.length = size.x - size.x * 0.1f;
		// pilotiScript.width = size.z - size.z * 0.1f;
		// var a = pilotiScript.length > pilotiScript.width ? pilotiScript.width : pilotiScript.length;	// get the shortet side
		// a *= 0.1f;
		// a = Mathf.Clamp(a, 0.1f, 0.75f);
		// pilotiScript.pilotiWidth = a;
		// pilotiScript.width -= a;
		// pilotiScript.length -= a;
		// var scale = pilotiScript.transform.localScale;
		// scale.y = size.y;
		var center = this.transform.position;
		var size = module.size;
		yNegative.Add(center + new Vector3(-size.x/2, -size.y/2, -size.z/2));
		yNegative.Add(center + new Vector3(-size.x/2, -size.y/2, size.z/2));
		yNegative.Add(center + new Vector3(size.x/2, -size.y/2, -size.z/2));
		yNegative.Add(center + new Vector3(size.x/2, -size.y/2, size.z/2));
		foreach(var p in yNegative){
			var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
			go.transform.position = p;
			go.transform.SetParent(transform);
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.black;
		foreach(var p in yNegative){
			Gizmos.DrawCube(p,Vector3.one * 0.1f);
		}
	}
	static bool init = false;

	public List<string> IsInList(string str, List<List<string>> listOfLists){
		int count = 0;
		foreach(var lst in listOfLists){
			if(lst.Contains(str)){
				return listOfLists[count];
				break;
			}
			count++;
		}
		return null;
	} 

	// Update is called once per frame
	void Update () {

		// module.visible = false;

		if(Input.GetKeyDown(KeyCode.Alpha3)){
			if(!init){
				init = true;
				var piltois = FindObjectsOfType<Piloti>();
				foreach(var piloti in piltois){
					var lst = IsInList(piloti.gameObject.name, groups);
					if(lst != null){
						foreach(var str in piloti.neighborCheck.groudIds)
						if(!lst.Contains(str)){
							lst.Add(str);
						}
					}else{
						bool b = false;
						foreach(var str in piloti.neighborCheck.groudIds){
							var subLst = IsInList(str, groups);
							if(subLst != null){
								subLst.Add(piloti.gameObject.name);
								b = true;
							}
						}
						if(!b){
							var g = new List<string>();
							g.Add(piloti.gameObject.name);
							g.AddRange(piloti.neighborCheck.groudIds);
							groups.Add(g);
						}
					}
				}
				int count = 0;
				foreach(var list in groups){
					var bigStr = count.ToString();
					foreach(var str in list){
						bigStr += " " + str;
					}
					Debug.Log(bigStr);
					count++;
				}
			}
		}

		// distance = 0;
		
		// if(points != null & points.Count > 0){
		// 	if(gos != null){
		// 		if(gos.Count < points.Count){
		// 			while(gos.Count < points.Count){
		// 				var go = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		// 				go.transform.SetParent(transform);
		// 				gos.Add(go);
		// 			}
		// 		}else if(gos.Count > points.Count){
		// 			while(gos.Count > points.Count){
		// 				var go = gos[gos.Count-1];
		// 				GameObject.Destroy(go);
		// 				gos.RemoveAt(gos.Count-1);
		// 			}
		// 		}
		// 		int index = 0;
		// 		foreach (var p in points)
		// 		{	
		// 			if(index < points.Count -1)
		// 				distance += Vector3.Distance(points[index], points[index +1]);
		// 			gos[index].transform.localPosition = p;
		// 			index++;
		// 		}
		// 		if(loop)
		// 			distance += Vector3.Distance(points[0], points[index +1]);
		// 		index = 0;
		// 		float divisor = distance / points.Count;
		// 		// foreach(var go in gos){
		// 		// 	float t = Mathf.InverseLerp(0,distance, divisor * index);
		// 		// 	if(index == 0){

		// 		// 	}else if(index == points.Count -1){

		// 		// 	}else{
		// 		// 		Vector3 newPoint = Vector3.Lerp(points[index],points[index+1],t);
		// 		// 		go.transform.position = newPoint;
		// 		// 	}
		// 		// 	index++;
		// 		// }
		// 	}
		// }
		// var size = module.size;
		// if(size.x > size.z){
		// 	width = size.z * 0.1f;
		// }else{
		// 	width = size.x * 0.1f;
		// }
		// foreach(var go in gos){
		// 	// var scale = go.transform.localScale;
		// 	// scale.x = width;
		// 	// scale.y = size.y /2;
		// 	// scale.z = width;
		// 	// go.transform.localScale = scale;
		// 	// var pos = go.transform.localPosition;
		// 	// pos.y = (size.y /2);
		// }
	}

	private void OnDestroy()
	{
		foreach(var go in gos){
			GameObject.Destroy(go);
		}	
	}
		
}
