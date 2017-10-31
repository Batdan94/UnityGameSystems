using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombificate : MonoBehaviour {

	public Material matPrefab;
	public List<MeshRenderer> bits;
	public List<Material> mats;
	public BoidStats boid;

	// Use this for initialization
	void Start () {
		bits = new List<MeshRenderer> ();
		bits.AddRange (transform.GetComponentsInChildren<MeshRenderer> ());
		for ( int j = 0; j < bits.Count; j++)
		{
			for (int i = 0; i < bits[j].materials.Length; i++){
				bits [j].material = Instantiate (matPrefab);
				bits[j].materials[i].SetFloat ("_Displacement", Utils.Map(boid.heatlh, 0.0f, 10.0f, 0.0f, 0.2f));
			}
		}
		var a = 1.0f - Utils.Map (boid.heatlh, 0.0f, 10.0f, 0.95f, 1.0f);
		transform.localScale = new Vector3 (a, a, a);
	}
	
	// Update is called once per frame
	void Update () {
		for ( int j = 0; j < bits.Count; j++)
		{
			for (int i = 0; i < bits[j].materials.Length; i++){
				bits[j].materials[i].SetFloat ("_Displacement", Utils.Map(boid.heatlh, 0.0f, 10.0f, 0.0f, 0.2f));
			}
			bits [j].enabled = boid.heatlh > 2.0f;
		}
		var a = Utils.Map (boid.heatlh, 2.0f, 6.0f, 0.95f, 1.0f);
		transform.localScale = new Vector3 (a, a, a);
	}
}
