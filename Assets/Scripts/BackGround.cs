using UnityEngine;
using System.Collections;

//背景
public class BackGround : MonoBehaviour {
	private MainCamera mainCamera;
	private Renderer _renderer;
	private GameObject obj_camera = null;
  private Vector3 offset = Vector3.zero;
// Use this for initialization
	void Start () {
		mainCamera = GameObject.Find("MainCamera").GetComponent<MainCamera>();
		_renderer = this.GetComponent<Renderer>();
    obj_camera = GameObject.FindGameObjectWithTag("MainCamera");
    offset = transform.position - obj_camera.transform.position;
	}
	
	public float speed = 0.01f;			// スクロールするスピード
  void Update () {
  	switch (GameManager.gameState) {
			case GameManager.GameState.Title: 
			break;
      case GameManager.GameState.Play: 
        if (!isMove()) return ;
      break;
			case GameManager.GameState.GameOver: if (!isMove()) return ; break;
			case GameManager.GameState.GameClear: if (!isMove()) return ; break;
		}
	  float scroll = Mathf.Repeat (Time.time * speed, 1);		// 時間によってYの値が0から1に変化していく.1になったら0に戻り繰り返す.
	  Vector2 offset = new Vector2 (scroll, 0);				// Xの値がずれていくオフセットを作成.
    _renderer.sharedMaterial.SetTextureOffset ("_MainTex", offset);	// マテリアルにオフセットを設定する.

  }

  void LateUpdate(){
	  Vector3 new_position = transform.position;
    new_position = obj_camera.transform.position + offset;
    transform.position =  new_position;
  }
	

	// Update is called once per frame
	//void Update () {
		//SetPosition();
	//}

	private bool isMove() {
		Vector2 pos = mainCamera.GetMovePosition();
		if (pos.x <= 0) return false;

		return true;
	}

	/*public float speed = 0.1f;
	void Update () {
    float y = Mathf.Repeat (Time.time * speed, 1);
	  Vector2 offset = new Vector2 (0,y);
	  _renderer.sharedMaterial.SetTextureOffset ("_MainTex", offset);
  }*/
  }
