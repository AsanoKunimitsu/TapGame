using UnityEngine;
using System.Collections;

public class UdonSoba : MonoBehaviour {

	// 流れる速度
	public float Speed;

	// コンポーネント格納用
	private Rigidbody2D rigid;
	private Renderer render;

	// GameManagerオブジェクト
	private GameManager gameManager;

	// Use this for initialization
	void Start () {
		// コンポーネントの読み込み
		rigid = GetComponent<Rigidbody2D> ();
		render = GetComponent<Renderer> ();

		// GameManagerオブジェクトを取得
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();

		// 左方向に移動させる
		rigid.velocity = new Vector3(-1, 0, 0) * Speed;
	}
	
	// Update is called once per frame
	void Update () {
		// 画面の左外に出たら自身を削除する
		if (!render.isVisible && transform.position.x < 0) {
			gameManager.DestroyNoodle(gameObject);
		}

		// タッチ/クリックされた場合はGameManagerの判定イベントを呼び出して自身を削除
		if (Input.GetMouseButtonDown(0)) {

			// タッチされたオブジェクトを検出し、自分自身であれば判定イベントと削除を行う
			Vector3 touchPoint = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			Collider2D collider = Physics2D.OverlapPoint (touchPoint);
			if (collider && collider.transform.gameObject == gameObject) {

				gameManager.TouchedNoodle (gameObject);
				gameManager.DestroyNoodle(gameObject);
			}
		}
	}
}
