using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class BoxMovement : Movement
{
	protected override void move()
	{
		rb.AddForce(transform.TransformDirection(moveDirection) * strength);
		//rb.AddForce(transform.forward * strength);
	}
}
