using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushButton : MonoBehaviour 
{
	public bool activated = false;

	public Transform off_position;
	public Transform on_position;

	public float reset_duration;
	public float progress;

	public Vector3 enter_contact_pos;
	public Vector3 offset;
	public float projection;

	int environment_layer;

	void Start()
	{
		environment_layer = LayerMask.NameToLayer("Environment");
	}

	public bool GetNonEnvironmentFirstContact(Collision collision, ref Vector3 contact_pos)
	{
		foreach(ContactPoint contact_point in collision.contacts)
		{
			if(contact_point.otherCollider.gameObject.layer == environment_layer)
			{
				continue;
			}
			else
			{	
				contact_pos = contact_point.point;
				return true;
			}
		}

		return false;
	}

	void OnCollisionEnter(Collision collision)
	{
		GetNonEnvironmentFirstContact(collision, ref enter_contact_pos);
	}

	void OnCollisionStay(Collision collision)
	{
		Vector3 contact_pos = new Vector3();		
		GetNonEnvironmentFirstContact(collision, ref contact_pos);

		offset = contact_pos - enter_contact_pos;

		Vector3 normal = on_position.position - off_position.position;

		projection = Vector3.Dot(offset, normal);

		transform.position = transform.position + projection * normal;
	}

	void OnCollisionExit(Collision collision)
	{
		enter_contact_pos = Vector3.zero;
	}

	void Update()
	{

	}
}
