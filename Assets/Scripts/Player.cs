using UnityEngine;
using System.Collections;

public class Player : Entity {

	public override void Die ()
	{
		health += 100;
	}
}
