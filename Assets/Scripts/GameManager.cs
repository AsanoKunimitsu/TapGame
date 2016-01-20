using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	// そばが出てくる確率
	public int SobaProbability;

	// 設定個数を出した時点で終了
	public int EndCount;

	// 表示済みオブジェクト数
	private int counter = 0;

	// うどん連続タップ数
	private int combo = 0;

	// Use this for initialization
	void Start () {
		StartCoroutine ( CreateUdonSoba() );
	}

	IEnumerator CreateUdonSoba() {
		for (int i = 0; i < EndCount; i++) {
            
            // うどんかそばのオブジェクトを生成・表示
			Vector3 pos = new Vector3 (15, 0, 0);
			GameObject prefab;
			// ランダムでそばを出現
			int rnd = Random.Range (0, SobaProbability);
			if (rnd == SobaProbability - 1) {
				prefab = (GameObject)Resources.Load ("Prefabs/Soba");
			} else {
				prefab = (GameObject)Resources.Load ("Prefabs/Udon");
			}
			Instantiate (prefab, pos, Quaternion.identity);

			yield return new WaitForSeconds (1f);
		}
	}

	// うどん/そば がタッチ/クリックされた際の処理
	public void TouchedNoodle(GameObject obj) {
		// prefabに設定されたtagで判別する
		if (obj.tag == "Udon") {
			// うどん連続タップでコンボ数を増やす
			combo++;
		} else {
			// そばの場合はコンボをリセット
			combo = 0;
		}
	}

	// うどん/そば を消去して表示済みカウンターを増やす
	public void DestroyNoodle(GameObject obj) {
		Destroy (obj);
		counter++;
	}

	// Update is called once per frame
	void Update () {
		if (counter == EndCount) {
			// 終了処理
			Debug.Log ("Finish game.");
		}
	}

	void OnGUI() {
		// コンボ数を左上に表示
		GUI.Label (new Rect (10, 10, 100, 20), combo.ToString());
	}
}
