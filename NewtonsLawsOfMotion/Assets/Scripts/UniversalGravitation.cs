using UnityEngine;
using System.Collections;

public class UniversalGravitation : MonoBehaviour {
	private PhysicsEngine[] physicsEngineArray;
	private const float bigG = 6.673e-11f; // N⋅(m/kg)2 [ kg m s^2 m^2 kg^-2]

	void Start()
	{
		physicsEngineArray = GameObject.FindObjectsOfType<PhysicsEngine>();
	}

	void FixedUpdate()
	{
		CalculateGravity();
	}

	void CalculateGravity()
	{
		foreach (PhysicsEngine physicsEngineA in physicsEngineArray){
			foreach (PhysicsEngine physicsEngineB in physicsEngineArray){
				if (physicsEngineA != physicsEngineB && physicsEngineA != this) {
					Debug.Log("Calculating Gravitational Force Exerted on " + physicsEngineA.name +
						" due to the gravity of " + physicsEngineB.name);

					Vector3 offset = physicsEngineA.transform.position - physicsEngineB.transform.position;
					float rSquared = Mathf.Pow(offset.magnitude, 2f);
					float gravityMagnitude = bigG * physicsEngineA.mass * physicsEngineB.mass / rSquared;
					Vector3 gravityFeltVector = gravityMagnitude * offset.normalized;

					physicsEngineA.AddForce(-gravityFeltVector);
				}
			}
		}
	}
}
