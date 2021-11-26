using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightFlicker : MonoBehaviour 
{
	public string pattern = "mmamammmmammamamaaamammma"; //Classic Quake flicker pattern

	int char_index = 0;

	float due;

	public float flicker_speed; //The lower the faster.
	public float light_intensity;

	Light light;

	Dictionary<char, float> lookup_table = new Dictionary<char, float>()
	{
		{'a', 0},
		{'b', 1 * 0.03846153846f},
		{'c', 2 * 0.03846153846f},
		{'d', 3 * 0.03846153846f},
		{'e', 4 * 0.03846153846f},
		{'f', 5 * 0.03846153846f},
		{'g', 6 * 0.03846153846f},
		{'h', 7 * 0.03846153846f},
		{'i', 8 * 0.03846153846f},
		{'j', 9 * 0.03846153846f},
		{'k', 10 * 0.03846153846f},
		{'l', 11 * 0.03846153846f},
		{'m', 12 * 0.03846153846f},
		{'n', 13 * 0.03846153846f},
		{'o', 14 * 0.03846153846f},
		{'p', 15 * 0.03846153846f},
		{'q', 16 * 0.03846153846f},
		{'r', 17 * 0.03846153846f},
		{'s', 18 * 0.03846153846f},
		{'t', 19 * 0.03846153846f},
		{'u', 20 * 0.03846153846f},
		{'v', 21 * 0.03846153846f},
		{'w', 22 * 0.03846153846f},
		{'x', 23 * 0.03846153846f},
		{'y', 24 * 0.03846153846f},
		{'z', 0.9615384615f},
	};

	// Use this for initialization
	void Start () 
	{
		light = gameObject.GetComponent<Light>();
	}
	
	// Update is called once per frame
	
	void Update () 
	{
		if(due < 0.0f)
		{
			due = flicker_speed;
			changeLightColor(pattern[char_index]);

			char_index++;
			if(char_index >= pattern.Length)
			{
				char_index = 0;
			}

			return;
		}

		due -= Time.deltaTime;
	}

	void changeLightColor(char intensity)
	{
		light.intensity = light_intensity * lookup_table[intensity];
	}
}
