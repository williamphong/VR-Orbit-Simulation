using UnityEngine;
using System.Collections;


public class Spin : MonoBehaviour {
	public bool spinning = true;
	public float rotationDuration = 1f;

	public GameObject masterControl;
	SimulationController simuControl;


	[HideInInspector]
	public float direction = 1f;
	[HideInInspector]
	public float directionChangeSpeed = 2f;


	void Start()
	{
		simuControl = masterControl.GetComponent<SimulationController>();
	}

	void Update()
	{
		if (direction < 1f) direction += Time.deltaTime * simuControl.simulationSpeed / (directionChangeSpeed / 2);

		if (spinning) transform.Rotate(Vector3.up, (360/rotationDuration) * Time.deltaTime * simuControl.simulationSpeed);
	}
}