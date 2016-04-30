using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PhysicsEngine : MonoBehaviour {
	public float mass;
	public Vector3 velocityVector; // average velocity this FixedUpdate()
	public Vector3 netForceVector;
	public List<Vector3>forceVectorList = new List<Vector3>();

	void Start () 
	{
		SetupThrustTrails();
	}
	
	void FixedUpdate ()
	{
		SumForces();
		RenderTrails();
		UpdatePosition();

	}

	public void AddForce(){
	  
	}

	void SumForces()
	{
		netForceVector = Vector3.zero;

		foreach(Vector3 forceVector in forceVectorList){
			netForceVector += forceVector;
		}
	}

	void UpdatePosition()
	{
		Vector3 accelerationVector = netForceVector/mass;
		velocityVector+=accelerationVector * Time.deltaTime;
		transform.position += velocityVector * Time.deltaTime;

	}

	/// <summary>
	/// Code For Drawing Thrust Trails
	/// </summary>
	public bool showTrails = true;

	private LineRenderer lineRenderer;
	private int numberOfForces;

	// Use this for initialization
	void SetupThrustTrails () {
		lineRenderer = gameObject.AddComponent<LineRenderer>();
		lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
		lineRenderer.SetColors(Color.yellow, Color.yellow);
		lineRenderer.SetWidth(0.2F, 0.2F);
		lineRenderer.useWorldSpace = false;
	}

	// Update is called once per frame
	void RenderTrails () {
		if (showTrails) {
			lineRenderer.enabled = true;
			numberOfForces = forceVectorList.Count;
			lineRenderer.SetVertexCount(numberOfForces * 2);
			int i = 0;
			foreach (Vector3 forceVector in forceVectorList) {
				lineRenderer.SetPosition(i, Vector3.zero);
				lineRenderer.SetPosition(i+1, -forceVector);
				i = i + 2;
			}
		} else {
			lineRenderer.enabled = false;
		}
	}
}
