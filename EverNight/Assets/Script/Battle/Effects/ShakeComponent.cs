using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeComponent : MonoBehaviour
{
	private Vector3 originalPos;
	private float shakeDuration = 0f;

	private bool shaking;
	// Use this for initialization
	void Start () {
		originalPos = transform.localPosition;
		shaking = false;
	}

	public void StartShake()
	{
		shakeDuration = 1;
		shaking = true;
	}
	
	// Update is called once per frame
	void Update () {

		if (shaking)
		{
			if (shakeDuration > 0)
			{
				transform.localPosition = originalPos + Random.insideUnitSphere * 0.1f;
				shakeDuration -= Time.deltaTime * 2;
			}
			else
			{
				shakeDuration = 0f;
				transform.localPosition = originalPos;
				shaking =  false;
			}
		}
	}
}
