using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.IO;

[Serializable]
public class Light_exp
{
    public string name;
    public Vector3 world_pos;
    public float intensity;
}

[ExecuteInEditMode]
public class LightImporter : MonoBehaviour 
{
	public string directory = "./LightData/";
	public string[] files;

	void Import()
	{
		files = System.IO.Directory.GetFiles(directory);

		foreach(string file in files)
		{	
            string contents;
			
			using(StreamReader sr = File.OpenText(file))
            {
            	contents = sr.ReadToEnd();
            }

            Light_exp new_light = JsonUtility.FromJson<Light_exp>(contents);

            GameObject new_light_go = new GameObject(new_light.name);
            new_light_go.transform.SetParent(this.transform);
            new_light_go.transform.position = new_light.world_pos;

            Light new_light_component = new_light_go.AddComponent<Light>();

  			new_light_component.intensity = new_light.intensity;
		}
	}
}
