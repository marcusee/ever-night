using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EffectSystem : MonoBehaviour
{
	public List<ParticleSystem> particleSystems_list;

	private int currentId;
	// Use this for initialization

/// <summary>
/// this method will be called in turnsystem.
/// </summary>
	public void Start()
{
	StopAll();
}

	public void StopAll()
	{
		foreach (ParticleSystem ps in particleSystems_list)
		{
			ps.Stop();
		}
	}
	
	public void PlayEffect(int id)
	{

		if(id < 0 || id >= particleSystems_list.Count) id = 0;
	
		var ps = particleSystems_list[id];
		//ps.transform.position = new Vector3(0,0,0);
		currentId = id;
		//foreach (ParticleSystem psc in ps.transform.GetComponentsInChildren<ParticleSystem>())
		//{
		//	psc.transform.position = Vector3.zero;
		//	psc.Emit(1);

		//}
		ps.Play();
		//StartCoroutine("WaitAwhile", ps);
		if (id == 2) return;
		
		Invoke("stopEffect" , 0.5f);

	}

	private void stopEffect()
	{
		particleSystems_list[currentId].Stop(true, ParticleSystemStopBehavior.StopEmitting);

	}
	
	public  IEnumerable WaitAwhile(ParticleSystem ps)
	{

		yield return new WaitForSeconds(0.5f);
		ps.Stop(true, ParticleSystemStopBehavior.StopEmitting);

	}


}
