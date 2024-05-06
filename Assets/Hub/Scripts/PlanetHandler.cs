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

		private float currentLinearMapping = 0.5f;


		//-------------------------------------------------
		void Awake()
		{
			if (simuControl == null)
			{
				simuControl = masterControl.GetComponent<SimulationController>(); ;
			}

			if (linearMapping == null)
			{
				linearMapping = GetComponent<LinearMapping>();
			}

			simuControl.simulationSpeed = 1f;
		}


		//-------------------------------------------------
		void Update()
		{
			if (currentLinearMapping != linearMapping.value)
			{
				currentLinearMapping = linearMapping.value;

				if (currentLinearMapping != 0)
				{
					simuControl.simulationSpeed = Mathf.Pow(500f, linearMapping.value - 0.5f);
				}

				else
				{
					simuControl.simulationSpeed = 0;
				}
			}
		}
	}
}