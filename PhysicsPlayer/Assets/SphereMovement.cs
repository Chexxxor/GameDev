using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class SphereMovement : Movement
{
	protected override void move()
	{
		rb.AddForce(moveDirection * strength);
	}
}
