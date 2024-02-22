//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: Animator whose speed is set based on a linear mapping
//
//=============================================================================

using UnityEngine;
using System.Collections;

namespace Valve.VR.InteractionSystem
{
	//-------------------------------------------------------------------------
	public class PlanetHandler : MonoBehaviour
	{
		public LinearMapping linearMapping;
        public GameObject masterControl;
        SimulationController simuControl;

        private float currentLinearMapping = float.NaN;
		private int framesUnchanged = 0;


		//-------------------------------------------------
		void Awake()
		{
			if (simuControl == null )
			{
					simuControl = masterControl.GetComponent<SimulationController>(); ;
			}

			if ( linearMapping == null )
			{
				linearMapping = GetComponent<LinearMapping>();
			}
		}


		//-------------------------------------------------
		void Update()
		{
			if ( currentLinearMapping != linearMapping.value )
			{
				currentLinearMapping = linearMapping.value;

				simuControl.simulationSpeed = linearMapping.value * 20;


				framesUnchanged = 0;
			}
			else
			{
				framesUnchanged++;
				if ( framesUnchanged > 2 )
				{
					//animator.enabled = false;
				}
			}
		}
	}
}
