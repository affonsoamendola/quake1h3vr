using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.IO;
/*
[Serializable]
public class Light_exp
{
    public string name;
    public Vector3 world_pos;
    public float intensity;
}
*/

public class LightExport : MonoBehaviour
{
    public List<Light_exp> lights;

    public string path = "./LightExp/";

    void Start()
    {
        lights = new List<Light_exp>();

        for(int i = 0; i < gameObject.transform.childCount; i++)
        {
            GameObject current_light = gameObject.transform.GetChild(i).gameObject;

            Light_exp out_light = new Light_exp();

            out_light.name = current_light.name;
            out_light.world_pos = current_light.transform.position;
            out_light.intensity = current_light.GetComponent<Light>().intensity;

            if(!File.Exists(path + out_light.name + ".json"))
            {
                using(StreamWriter sw = File.CreateText(path + out_light.name + ".json"))
                {
                    sw.Write(JsonUtility.ToJson(out_light));
                }
            }
        }
    }
}
