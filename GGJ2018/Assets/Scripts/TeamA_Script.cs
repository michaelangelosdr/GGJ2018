using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamA_Script : TeamScript{

	public TeamA_Script()
	{
		Patience_Value = 100.0f;
		JammerName = "Jammer A";
		CurrentState = Teamstate.WORKING_STATE;
	}

}
