using UnityEngine;
using System.Collections;

public class BulletSpawn : MonoBehaviour {

    public GameObject bullet, bulletSpawn;
    public int chargeTime;
    public bool fired;

    private PlayerMove player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindObjectOfType<PlayerMove>();
        chargeTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
	    if ((Input.GetKeyDown(KeyCode.Z) || (Input.GetAxis("RT") > 0)) && !fired)
        {
            fired = true;
            Instantiate(this.bullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
        }
        if (Input.GetKeyUp(KeyCode.Z) || (Input.GetAxis("RT") < 1))
        {
            fired = false;
        }
	}
}
