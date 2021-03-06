﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
public static class LeModular {

	public static List<float> redSeries = new List<float>(){
		0.6f,
		0.9f,
		1.5f,
		2.4f,
		3.9f,
		6.3f,
		10.2f,
		16.5f,
		26.7f,
		43.2f,
		69.8f,
		113.0f,
		182.9f,
		295.9f,
		478.8f,
		774.7f,
		1253.5f,
		2028.2f,
		3981.6f,
		5309.8f,
		8591.4f,
		13901.3f,
		22492.7f,
		36394.0f,
		58886.7f,
		95280.7f
	};

	public static List<float> blueSeries = new List<float>(){
		1.1f,
		1.8f,
		3.0f,
		4.8f,
		7.8f,
		12.6f,
		20.4f,
		33.0f,
		53.4f,
		86.3f,
		139.7f,
		226.0f,
		365.8f,
		591.8f,
		957.6f,
		1549.4f,
		2506.9f,
		4056.3f,
		6563.3f,
		10619.6f,
		17182.9f,
		27802.5f,
		44985.5f,
		72788.0f,
		117773.5f
	};

	public static float GetClosest(float value){
		
		int index = redSeries.BinarySearch(value);
    	if (0 <= index){
        Debug.Log(string.Format("Found value {0} at list[{1}]", value, index));
		}
		else
		{
			index = ~index;
			if (0 < index){
				//  Debug.Log(string.Format("list[{0}] = {1}", index - 1, redSeries[index - 1]));
			}
			//  Debug.Log(string.Format("list[{0}] = {1}", index, redSeries[index]));
		}
		return redSeries[index];
	}

	public static List<float> Divisions(float distance){
		float threshold = 0.6f;
		//find combination that will fall within a threshold of the distance
		List<float> possibilities = new List<float>();
		foreach (var i in redSeries)
		{
			if((i + threshold) < distance){
				possibilities.Add(i);
				// Debug.Log(distance + " possibility " + i);
			}	
		}

		return possibilities;
	}


	static  public List<Color> colours = new List<Color>();
	
	static public void Init(){
		
		Xml2CSharp.Root root = ReadXML.Deserialize<Xml2CSharp.Root>(Application.streamingAssetsPath + "/colourpalette.xml");
		Regex rx = new Regex(@"(\d+)", RegexOptions.Compiled | RegexOptions.IgnoreCase); // regex to capture groups of numbers
		foreach(var div in root.Div){
			MatchCollection matches = rx.Matches(div.Style);
			if(matches.Count != 3){
				Debug.LogError("Regex matches count is outside of range, either the regex failed or the string doesn't contain 3 groups of numbers");
			}
			byte r = byte.Parse(matches[0].ToString());
			byte g = byte.Parse(matches[1].ToString());
			byte b = byte.Parse(matches[2].ToString());
			byte a = byte.Parse("255");
			Color colour = new Color32(r,g,b,a);
			colours.Add(colour);
		}

		Debug.Log(colours);
	}
}

