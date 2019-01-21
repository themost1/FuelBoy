using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
	public GameObject planet;
	public GameObject darkmatter;
	public GameObject enemy;
	public GameObject player;
	public float planetdistance = 5f;
	public float dmdistance = 1f;
	public float enemydistance = 0.33f;
	public int num = 4;
	public float pi = Mathf.PI;
	public bool playerGen = false;

	// Use this for initialization
	void Start () 
	{
		GameObject start = (GameObject)Instantiate(planet,new Vector3(0,0,0), Quaternion.identity);
		generatePlanet(start,0,2*pi,3);
		if(!playerGen)
		{
			Instantiate(player,new Vector3(0,0,0),Quaternion.identity);
		}
	}
	
	void generatePlanet(GameObject begin, float a0, float a1, int n)
	{
		GameObject branch;
		for(int i = 0; i < n; i++)
		{
			float angle = Random.Range(a0+i*(a1-a0)/n,a0+(i+1)*(a1-a0)/n);
			float distance = (5-n)*planetdistance*Random.Range(0.75f,1.5f);
			
			Vector3 offset = new Vector3(distance*Mathf.Cos(angle),distance*Mathf.Sin(angle),0);
			branch = (GameObject)Instantiate(planet, begin.transform.position + offset, Quaternion.identity);
			
			//spawns the player at a random galaxy (has a 5% at each galaxy)
			if(Random.Range(0f,1f) < 0.05 && !playerGen)
			{
				Instantiate(player,begin.transform.position + offset,Quaternion.identity);
				playerGen = true;
			}
			if(Random.Range(0f,1f) < 0.4)
			{
				generatePirates(branch);
			}
			generateBetween(branch,begin,num);
			if(n > 2)
			{
				generatePlanet(branch,a0+i*(a1-a0)/n,a0+(i+1)*(a1-a0)/n,n-1);
			}
		}
	}
	
	GameObject generateBranch(GameObject begin)
	{
		float angle = Random.Range(0,2*Mathf.PI);
		float distance = planetdistance*Random.Range(0.75f,1.25f);
		Vector3 offset = new Vector3(distance*Mathf.Cos(angle),distance*Mathf.Sin(angle),0);
		GameObject branch = (GameObject)Instantiate(planet, begin.transform.position+ offset, Quaternion.identity);
		generateBetween(begin,branch,num);
		return branch;
	}
	
	void generateBetween(GameObject begin, GameObject dest,int num)
	{
		
		for(int i = 0; i < num-1; i++)
		{
			Vector3 middlePoint =  begin.transform.position+ (i+1)*(dest.transform.position-begin.transform.position)/num;
			float angle = Random.Range(0,2*Mathf.PI);
			Vector3 offset = new Vector3(dmdistance*Mathf.Cos(angle),dmdistance*Mathf.Sin(angle),0);
			GameObject dm = (GameObject)Instantiate(darkmatter, middlePoint +offset, Quaternion.identity);
			if(Random.Range(0f,1f) < 0.1)
			{
				generatePirates(dm);
			}
		}
	}
	
	void generatePirates(GameObject thing)
	{
		float angle = Random.Range(0,2*Mathf.PI);
		int n = Random.Range(1,6);
		for(int j = 0; j < n;j++)
		{
			Vector3 offset = new Vector3(enemydistance*Mathf.Cos(angle+2*Mathf.PI*j/n),enemydistance*Mathf.Sin(angle+2*Mathf.PI*j/n),0);
			Instantiate(enemy,thing.transform.position+offset,Quaternion.identity);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
