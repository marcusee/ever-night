using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    public BasicUnit unit;
    Slider slider;

    // Use this for initialization
    void Start ()
    {
	    unit = transform.parent.parent.gameObject.GetComponent<BasicUnit>();
         slider = GetComponent<Slider>();
	}

	// Update is called once per frame
	void Update () {
        slider.value = unit.template.currentHP/ unit.template.HP;
    }
}
