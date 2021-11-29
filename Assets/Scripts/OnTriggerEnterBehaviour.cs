using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerEnterBehaviour : MonoBehaviour
{
	public UnityEvent chosen_event;

	void OnTriggerEnter(Collider other)
	{
		chosen_event.Invoke();
	}
}
