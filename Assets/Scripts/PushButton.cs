using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PushButton : MonoBehaviour 
{
	public bool activated = false; //Has been pressed
	public bool disabled = false; //Wont be possible to press

	public UnityEvent trigger;

	public Transform on_position;
	public Transform reset_position;

	public bool reset_enabled = true;
	public float reset_time = 1.0f;
	public float reset_timer_progress = 0.0f;

	public MeshRenderer mesh_renderer;
	Material[] mats;

	public Material on_button;
	public Material off_button;

	public bool blink_state;

	public float blink_rate = 0.2f;
	public float blink_progess = 0.0f;

	public void Start()
	{
		mesh_renderer = gameObject.GetComponent<MeshRenderer>();
		mats = mesh_renderer.materials;

		if(disabled)
		{
			Disable();
		}
	}

	public void Enable()
	{
		disabled = false;
		DeActivate();
	}

	public void Disable()
	{
		disabled = true;

		gameObject.transform.position = on_position.position;
		gameObject.GetComponent<Rigidbody>().isKinematic = true;

		mats[1] = off_button;
		mesh_renderer.materials = mats;
	}

	public void Activate()
	{
		if(!disabled)
		{
			activated = true;
			trigger.Invoke();
			gameObject.transform.position = on_position.position;
			gameObject.GetComponent<Rigidbody>().isKinematic = true;

			mats[1] = off_button;
			mesh_renderer.materials = mats;
		}
	}

	public void DeActivate()
	{
		activated = false;
		gameObject.transform.position = reset_position.position;
		gameObject.GetComponent<Rigidbody>().isKinematic = false;
	}
		
	public void Update()
	{
		if(disabled)
		{
			return;
		}

		if(activated && reset_enabled)
		{
			reset_timer_progress += Time.deltaTime;

			if(reset_timer_progress > reset_time)
			{
				DeActivate();
				reset_timer_progress = 0.0f;
			}
		}
		
		if(!activated)
		{
			blink_progess += Time.deltaTime;

			if(blink_progess > blink_rate)
			{
				blink_progess = 0.0f;

				blink_state = !blink_state;

				if(blink_state)
				{	
					mats[1] = on_button;
				} 
				else
				{
					mats[1] = off_button;
				}

				mesh_renderer.materials = mats;
			}
		}
	}
}
