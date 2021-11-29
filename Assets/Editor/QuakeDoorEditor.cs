using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(QuakeDoor))]
public class QuakeDoorEditor : Editor 
{
	QuakeDoor _target;

	void OnEnable()
	{
		_target = (QuakeDoor)target;
	}

	public override void OnInspectorGUI()
    {
		DrawDefaultInspector ();

        if(GUILayout.Button("Open")) 
        {
      		_target.Open();
        }

        if(GUILayout.Button("Close")) 
        {
      		_target.Close();
        }
    }
}

//If you're reading, do yourself a favour and listen to this!
//https://youtu.be/uAOR6ib95kQ 

//Also DM me on discord so I know someone is reading my code.
//AffonsoAmendola#3532