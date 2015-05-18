using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyHpBar : MonoBehaviour {

    public GameObject enemy;
    private float fillValue = 1;
    private Image thisImage;
    private float enemyHp;
	// Use this for initialization
	void Start () {
     
        thisImage = GetComponent<Image>();
        GameObject levelcanvas = GameObject.FindGameObjectWithTag("EnemyHpBars");
        gameObject.transform.parent = levelcanvas.transform;
        enemyHp = enemy.GetComponent<EnemyScript>().life;
        
	}
	
	// Update is called once per frame
	void Update () {    
        if (enemy == null)
        {
            Destroy(gameObject);
        }

        if (enemy != null)
        {
            Vector3 pos;
            if (enemy.name == "tank_boss")
            {
                pos = new Vector3(enemy.transform.position.x, enemy.transform.position.y + 3.5f, enemy.transform.position.z);
            }
            else
            {
                pos = new Vector3(enemy.transform.position.x, enemy.transform.position.y + 2.2f, enemy.transform.position.z);
            }
            pos = Camera.main.WorldToScreenPoint(pos);
            transform.position = pos;
            if (enemy.GetComponent<EnemyScript>().life < fillValue * enemyHp)
            {
                fillValue -= 0.01f;
            }
            thisImage.fillAmount = fillValue;
        }
		
	}
}
