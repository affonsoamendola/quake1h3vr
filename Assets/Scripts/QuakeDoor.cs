using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QUAKE_DOOR_STATE
{
	OPEN,
	OPENING,
	RECESSED_DELAYING,
	RECESSED_OPENING,
	RECESSED_CLOSING,
	CLOSED,
	CLOSING
};

public class QuakeDoor : MonoBehaviour {

	public bool recessed = false;

	public bool auto_reset = false;
	public QUAKE_DOOR_STATE auto_reset_default;
	public float auto_reset_timer = 5.0f;
	public float auto_reset_progress = 0.0f;

	public Vector3 closed_offset;
	public Vector3 recess_offset;
	public Vector3 open_offset;

	public Vector3 start_position;
	public Vector3 current_position;
	public Vector3 target_position;

	public float recess_delay = 1.0f; //In seconds

	public float progress;
	public float progress_normalized;

	public float duration; //Time between door closed and door open or door recessed 
	public float duration_recess; //Time between door recessed and door open

	public QUAKE_DOOR_STATE state = QUAKE_DOOR_STATE.CLOSED;
	public QUAKE_DOOR_STATE recessed_state;
	
	void Start()
	{
		if(state == QUAKE_DOOR_STATE.CLOSED)
		{
			gameObject.transform.position = closed_offset;
		}

		if(state == QUAKE_DOOR_STATE.OPEN)
		{
			gameObject.transform.position = open_offset;
		}
	}


	// Update is called once per frame
	void Update () 
	{
		if(state != QUAKE_DOOR_STATE.OPEN && state != QUAKE_DOOR_STATE.CLOSED)
		{
			progress += Time.deltaTime;
			progress_normalized = progress / duration;

			if((state == QUAKE_DOOR_STATE.OPENING || state == QUAKE_DOOR_STATE.CLOSING) && recessed == false)
			{
				if(progress_normalized > 1.0f) 
				{
					progress = duration;
					progress_normalized = 1.0f;

					if(state == QUAKE_DOOR_STATE.OPENING)
					{
						state = QUAKE_DOOR_STATE.OPEN;
					}
					else if(state == QUAKE_DOOR_STATE.CLOSING)
					{
						state = QUAKE_DOOR_STATE.CLOSED;
					}
				}
			}
			else if((state == QUAKE_DOOR_STATE.OPENING || state == QUAKE_DOOR_STATE.CLOSING) && recessed == true)
			{
				if(progress_normalized > 1.0f)
				{	
					progress = progress - duration; //Subtract duration so we reset the counter for delaying, while not losing the information of time passed.
					progress_normalized = 1.0f; //Set this to 1.0f so that the update position puts the door in the recessed position
					
					recessed_state = state;
					state = QUAKE_DOOR_STATE.RECESSED_DELAYING;
				}
			}
			else if(state == QUAKE_DOOR_STATE.RECESSED_DELAYING)
			{
				if(progress_normalized > 1.0f)
				{
					progress = progress - duration;
					start_position = recess_offset;

					if(recessed_state == QUAKE_DOOR_STATE.OPENING)
					{
						state = QUAKE_DOOR_STATE.RECESSED_OPENING;
						target_position = open_offset;
					}
					else if(recessed_state == QUAKE_DOOR_STATE.CLOSING)
					{
						state = QUAKE_DOOR_STATE.RECESSED_CLOSING;
						target_position = closed_offset;
					}
				}
				return;
			}
			else if(state == QUAKE_DOOR_STATE.RECESSED_OPENING || state == QUAKE_DOOR_STATE.RECESSED_CLOSING )
			{
				if(progress_normalized > 1.0f)
				{
					progress_normalized = 1.0f;

					if(state == QUAKE_DOOR_STATE.RECESSED_OPENING)
					{
						state = QUAKE_DOOR_STATE.OPEN;
					}
					else if(state == QUAKE_DOOR_STATE.RECESSED_CLOSING)
					{
						state = QUAKE_DOOR_STATE.CLOSED;
					}	
				}
			}

			current_position = Vector3.Lerp(start_position, target_position, progress_normalized);
			gameObject.transform.position = current_position;
		}

		if(auto_reset && auto_reset_default == QUAKE_DOOR_STATE.CLOSED 
			&& state == QUAKE_DOOR_STATE.OPEN)
		{
			auto_reset_progress += Time.deltaTime;

			if(auto_reset_progress >= auto_reset_timer)
			{
				auto_reset_progress = 0.0f;
				Close();
			}
		}

		if(auto_reset && auto_reset_default == QUAKE_DOOR_STATE.OPEN 
			&& state == QUAKE_DOOR_STATE.CLOSED)
		{
			auto_reset_progress += Time.deltaTime;

			if(auto_reset_progress >= auto_reset_timer)
			{
				auto_reset_progress = 0.0f;
				Open();
			}
		}
	}

	public void Open()
	{
		if(state == QUAKE_DOOR_STATE.CLOSED) 
		{
			progress = 0.0f;
			state = QUAKE_DOOR_STATE.OPENING;
			start_position = closed_offset;

			if(recessed == true)
			{
				target_position = recess_offset;
			}
			else
			{
				target_position = open_offset;
			}
		}
	}

	public void Close()
	{
		if(state == QUAKE_DOOR_STATE.OPEN)
		{
			progress = 0.0f;
			state = QUAKE_DOOR_STATE.CLOSING;
			start_position = open_offset;

			if(recessed == true)
			{
				target_position = recess_offset;
			}
			else
			{
				target_position = closed_offset;
			}
		}
	}
}
