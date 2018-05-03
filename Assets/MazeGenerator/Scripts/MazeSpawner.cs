using UnityEngine;
using System.Collections;

//<summary>
//Game object, that creates maze and instantiates it in scene
//</summary>
public class MazeSpawner1 : MonoBehaviour {
	public enum MazeGenerationAlgorithm{
		PureRecursive,
		RecursiveTree,
		RandomTree,
		OldestTree,
		RecursiveDivision,
	}

	public MazeGenerationAlgorithm Algorithm = MazeGenerationAlgorithm.PureRecursive;
	public bool FullRandom = false;
	public int RandomSeed = 12345;
	public GameObject Floor = null;
	public GameObject Wall = null;
	public GameObject Pillar = null;
	public int Rows = 5;
	public int Columns = 5;
	public float CellWidth = 5;
	public float CellHeight = 8;
	public bool AddGaps = true;
	public GameObject GoalPrefab = null;

	private BasicMazeGenerator mMazeGenerator = null;

	void Start () {
		Debug.Log ("iepa");
		if (!FullRandom) {
			Random.seed = RandomSeed;
		}
		switch (Algorithm) {
		case MazeGenerationAlgorithm.PureRecursive:
			mMazeGenerator = new RecursiveMazeGenerator (Rows, Columns);
			break;
		case MazeGenerationAlgorithm.RecursiveTree:
			mMazeGenerator = new RecursiveTreeMazeGenerator (Rows, Columns);
			break;
		case MazeGenerationAlgorithm.RandomTree:
			mMazeGenerator = new RandomTreeMazeGenerator (Rows, Columns);
			break;
		case MazeGenerationAlgorithm.OldestTree:
			mMazeGenerator = new OldestTreeMazeGenerator (Rows, Columns);
			break;
		case MazeGenerationAlgorithm.RecursiveDivision:
			mMazeGenerator = new DivisionMazeGenerator (Rows, Columns);
			break;
		}
		mMazeGenerator.GenerateMaze ();
		for (int row = 0; row < Rows; row++) {
			for(int column = 0; column < Columns; column++){
				float x = column*(CellWidth+(AddGaps?.2f:0)) ;
				float z = row*(CellHeight+(AddGaps?.2f:0));
				MazeCell cell = mMazeGenerator.GetMazeCell(row,column);
				GameObject tmp;
				tmp = Instantiate(Floor,new Vector3(x,0,z), Quaternion.Euler(0,0,0)) as GameObject;
				tmp.transform.parent = transform;
				tmp.transform.localScale = new Vector3 (6.5f,0.10f, 6.5f);
				Wall.transform.localScale = new Vector3(1.72f,2.5f,1.6f);
				if(cell.WallRight){
					tmp = Instantiate(Wall,new Vector3(x+0.1f+CellWidth/2,0,z)+Wall.transform.position,Quaternion.Euler(0,90,0)) as GameObject;// right
					tmp.transform.parent = transform;
				}
				if(cell.WallFront){
					tmp = Instantiate(Wall,new Vector3(x,0,z+0.1f+CellHeight/2)+Wall.transform.position,Quaternion.Euler(0,0,0)) as GameObject;// front
					tmp.transform.parent = transform;
				}
				if(cell.WallLeft){
					tmp = Instantiate(Wall,new Vector3(x-0.1f-CellWidth/2,0,z)+Wall.transform.position,Quaternion.Euler(0,270,0)) as GameObject;// left
					tmp.transform.parent = transform;
				}
				if(cell.WallBack){
					tmp = Instantiate(Wall,new Vector3(x,0,z-0.1f-CellHeight/2)+Wall.transform.position,Quaternion.Euler(0,180,0)) as GameObject;// back
					tmp.transform.parent = transform;
				}
				if(cell.IsGoal && GoalPrefab != null){
					tmp = Instantiate(GoalPrefab,new Vector3(x,1,z), Quaternion.Euler(0,0,0)) as GameObject;
					tmp.transform.parent = transform;
				}
			}
		}
		if(Pillar != null){
			for (int row = 0; row < Rows+1; row++) {
				for (int column = 0; column < Columns+1; column++) {
					float x = column*(CellWidth+(AddGaps?.2f:0));
					float z = row*(CellHeight+(AddGaps?.2f:0));
					GameObject tmp = Instantiate(Pillar,new Vector3(x-CellWidth/2,0,z-CellHeight/2),Quaternion.identity) as GameObject;
					tmp.transform.parent = transform;
					tmp.transform.localScale = new Vector3 (1.5f,2.0f,1.5f);
					tmp.SetActive(false);
				}
			}
		}

		this.transform.localScale = new Vector3 (2.0f,1.0f, 2.0f);
		this.transform.position = new Vector3(-58.6f,0.0f,-61.0f);

		GameObject initWall = FindAt(new Vector3(-64.8f,2.5f,-61.0f));
		if(initWall != null){
			initWall.SetActive(false);
		}

		GameObject finishWall = FindAt(new Vector3(59.2f,2.5f,50.6f));
		if(finishWall != null){
			finishWall.tag = "finish";
			Renderer rend = finishWall.GetComponent<Renderer>();
			MeshCollider myMesh = finishWall.GetComponent<MeshCollider>();
			myMesh.convex = true;
			myMesh.isTrigger = true;
			rend.enabled = false;
			//finishWall.SetActive(false);
		}


	}

	GameObject FindAt ( Vector3 pos  ){
		// get all colliders that intersect pos:
		Collider[] cols= Physics.OverlapSphere(pos, 0.1f);
		// find the nearest one:
		float dist = Mathf.Infinity;
		GameObject nearest = null;
		foreach(Collider col in cols){
			// find the distance to pos:
			float d= Vector3.Distance(pos, col.transform.position);
			if (d < dist) { // if closer...
				dist = d; // save its distance... 
				nearest = col.gameObject; // and its gameObject
			}
		}
		return nearest;
	}

}
