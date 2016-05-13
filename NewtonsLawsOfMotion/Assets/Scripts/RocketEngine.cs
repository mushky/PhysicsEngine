using UnityEngine;
using System.Collections;

[RequireComponent (typeof(PhysicsEngine))]

public class RocketEngine : MonoBehaviour {

	public float fuelMass;   		// [kg]
	public float maxThrust;  		// kN [kg = m * s^-2]
	[Range (0,1f)] public float thrustPercentage;	// [none]
	public Vector3 thrustUnitVector;// [none]

	private PhysicsEngine physicsEngine;
	private float currentThrust;    // N

	void Start () 
	{
		physicsEngine = GetComponent<PhysicsEngine>();
		physicsEngine.mass+=fuelMass; 
	}
	
	void FixedUpdate () 
	{
		if (fuelMass > FuelThisUpdate()) {
			fuelMass-=FuelThisUpdate();	
		    physicsEngine.mass-=FuelThisUpdate();
			ExertForce();
			//physicsEngine.AddForce(thrustUnitVector);
		}
		else {
			Debug.Log("Out of Rocket Fuel");
		}
	}

	float FuelThisUpdate()
	{
		float exhaustMassFlowRate;		// [ ]
		float effectiveExhaustVelocity; // [ ]

		effectiveExhaustVelocity = 4462f; // [m s^1] liquid hydrogen oxygen engine 
		exhaustMassFlowRate  = currentThrust / effectiveExhaustVelocity;

		return exhaustMassFlowRate * Time.deltaTime;	// [kg]
	}
	void ExertForce()
	{
		currentThrust = thrustPercentage * maxThrust * 1000f;
		Vector3 thrustVector = thrustUnitVector.normalized * currentThrust; // N
		physicsEngine.AddForce(thrustVector);
	}
}
