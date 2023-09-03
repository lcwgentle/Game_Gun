using System.Collections;
using UnityEngine;

public class TargetHealth : HealthManager
{
	public bool boss;
	public bool box;
	public AudioClip deadSound;
	public AudioClip startSound;

	private Vector3 targetRotation;
	public GameObject effect;
	private float health;  //生命值
	public float totalHealth = 100;
	private RectTransform healthBar;       //血条
	private float originalBarScale;
	private bool dead;
	private GameObject boxFlood;
	

	void Awake ()
	{
		
		targetRotation = this.transform.localEulerAngles;
		gameObject.SetActive(false);
		//targetRotation.x = -90;
		if (boss)
		{
			healthBar = this.transform.Find("Health/Bar").GetComponent<RectTransform>();
			healthBar.parent.gameObject.SetActive(false);
			originalBarScale = healthBar.sizeDelta.x;
		}
		if (box)
		{
			boxFlood = this.transform.parent.Find("Bottle").gameObject;
			healthBar = this.transform.Find("Health/Bar").GetComponent<RectTransform>();
			healthBar.parent.gameObject.SetActive(false);
			originalBarScale = healthBar.sizeDelta.x;
		}
		dead = true;
		health = totalHealth;
	}

	void Update ()
	{
		this.transform.localRotation = Quaternion.Slerp(this.transform.localRotation, Quaternion.Euler(targetRotation), 10 * Time.deltaTime);
	}

	public bool IsDead { get { return dead; } }

	public override void TakeDamage(Vector3 location, Vector3 direction, float damage)
	{
		if (boss)
		{
			health -= damage;
			//effect.GetComponent<ParticleSystem>().Play();
			UpdateHealthBar();
			if (health <= 0 && (int)this.transform.localEulerAngles.x == 0)
			{
				Kill();
				PlayerHealth.coin +=3;
			}
		}
		else if(box)
        {
			//print(health);
			health -= damage;
			//effect.GetComponent<ParticleSystem>().Play();
			UpdateHealthBar();
			if (health <= 0)
			{
				StartCoroutine(BoxCour());
				Kill();		
			}
		}
		else if ((int)this.transform.localEulerAngles.x >= -15 && !dead)
		{
			Kill();
			PlayerHealth.coin += 1;
		}
	}
	IEnumerator BoxCour()
    {
		transform.GetComponent<Animator>().SetFloat("A", 1);
		yield return new WaitForSeconds(2f);
		boxFlood.SetActive(true);
		gameObject.SetActive(false);
	}
	public void Kill()
	{
		if(boss||box)
			healthBar.parent.gameObject.SetActive(false);
        dead = true;
        //targetRotation.x = -90;
		//gameObject.SetActive(false);
		AudioSource.PlayClipAtPoint(deadSound, transform.position); 
	}
	//IEnumerator DecreaseRroll()
 //   {
	//	yield return new WaitForSeconds(1.5f);
		
	//	gameObject.SetActive(false);
		
	//}

	public void Revive()
	{
		if (boss)
		{
			health = totalHealth;
			healthBar.parent.gameObject.SetActive(true);
			UpdateHealthBar();
		}
		if (box)
		{
			health = totalHealth;
			
			healthBar.parent.gameObject.SetActive(true);
			UpdateHealthBar();
		}
		dead = false;
		//targetRotation.x = 0;
		gameObject.SetActive(true);
		AudioSource.PlayClipAtPoint(startSound, transform.position);
	}

	private void UpdateHealthBar()  //血条更新条
	{
		float scaleFactor = health / totalHealth;

		healthBar.sizeDelta = new Vector2(scaleFactor * originalBarScale, healthBar.sizeDelta.y);
	}
    
}
