using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Destroy(transform.gameObject,3f);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.up * Time.deltaTime * 4f);
	}
}
