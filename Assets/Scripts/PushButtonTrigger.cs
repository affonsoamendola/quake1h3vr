using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushButtonTrigger : MonoBehaviour 
{
	public PushButton push_button;

	public void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.GetComponent<PushButton>() == push_button)
		{
			push_button.Activate();
		}
	}
}
