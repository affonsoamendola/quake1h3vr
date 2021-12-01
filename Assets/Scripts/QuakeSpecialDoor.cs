using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuakeSpecialDoor : MonoBehaviour 
{
	public PushButton first;
	public PushButton second;
	public PushButton third;

	public QuakeDoor door;

	public bool first_pressed = false;
	public bool second_pressed = false;
	public bool third_pressed = false;

	public void Start()
	{
		first.Enable();

		second.Disable();
		third.Disable();
	}

	public void activate_first()
	{
		if(first_pressed == false)
		{
			first_pressed = true;
			second.Enable();
		}
	}

	public void activate_second()
	{
		if( first_pressed == true && 
			second_pressed == false)
		{
			second_pressed = true;
			third.Enable();
		}
	}

	public void activate_third()
	{
		if( first_pressed == true && 
			second_pressed == true &&
			third_pressed == false) 
		{
			third_pressed = true;
			door.Open();
		}
	}
	
}
