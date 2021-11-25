using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using System;
using System.IO;

[Serializable]
public class Light_import
{
    public string name;
    public Vector3 world_pos;
    public float intensity;
}

[CustomEditor(typeof(LightImporter))]
public class LightImporterEditor : Editor 
{
	public string directory = "./LightData/";
	public string[] files;

	public override void OnInspectorGUI()
    {
        if(GUILayout.Button("Import")) 
        {
      		Import();
        }
    }

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

            Light_import new_light = JsonUtility.FromJson<Light_import>(contents);

            GameObject new_light_go = new GameObject(new_light.name);
            new_light_go.transform.position = new_light.world_pos;

            Light new_light_component = new_light_go.AddComponent<Light>();

  			new_light_component.intensity = new_light.intensity * 0.01f;
		}
	}
}
