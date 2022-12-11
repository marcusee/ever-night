using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSystem : MonoBehaviour
{

	[HideInInspector]
	public GameObject meleeActor;
	[HideInInspector]
	public GameObject targetActor;

	[HideInInspector]
	public bool startMelee;

	private Vector3 originalPosition;

	private bool movingToTarget;

	[HideInInspector]
	private turnSystemScript09 turnSystem;
	
	// Use this for initialization
	void Start ()
	{
		turnSystem = GetComponent<turnSystemScript09>();
		startMelee = false;
		movingToTarget = true;
	}

	public void StartMelee(GameObject meleeActor , GameObject targetActor )
	{
		turnSystem.state = STATE.ANIMATING;

		this.meleeActor = turnSystem.currentOBJ;
		this.targetActor = turnSystem.targetOBJ;

		startMelee = true;

		originalPosition = meleeActor.transform.position;
		movingToTarget = true;
	}
	
	
	// Update is called once per frame
	void Update ()
	{

		if (!startMelee) return;

		if (meleeActor == null || targetActor == null)
		{
			Debug.LogError("null melee agents");
			return;
		}

		if(movingToTarget)
			moveToTarget();
		else
		{
			moveBackFromTarget();
		}
	}


	private void moveToTarget()
	{
		
		Vector3 targetPosition = targetActor.transform.position + Vector3.back *  2.5f;
		
		if(targetActor.GetComponent<Enemy>() != null)
			targetPosition = targetActor.transform.position + Vector3.forward *  2.5f;
		
		
		meleeActor.transform.position = Vector3.MoveTowards(meleeActor.transform.position, targetPosition, Time.deltaTime * 20);
		float dist = Vector3.Distance(meleeActor.transform.position, targetPosition);
		//Debug.Log( meleeActor.transform.position + " " + targetPosition);

		
		if (dist < 1)
		{
			movingToTarget = false;
			turnSystem.ApplyDamageToTarget();
		}
		
	}

	private void moveBackFromTarget()
	{

		meleeActor.transform.position = Vector3.MoveTowards(meleeActor.transform.position, originalPosition, Time.deltaTime * 20);
		float dist = Vector3.Distance(meleeActor.transform.position, originalPosition);

		if (dist < 1)
		{
			meleeActor.transform.position = originalPosition;
			startMelee = false;
			turnSystem.state = STATE.DECIDING;
			turnSystem.EndCurrentTurn();

		}
	}
}
