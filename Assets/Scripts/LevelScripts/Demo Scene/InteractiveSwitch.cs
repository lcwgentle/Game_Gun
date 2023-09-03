using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is created for the example scene. There is no support for this script.
public class InteractiveSwitch : MonoBehaviour
{
	public List<TargetHealth> targets;
	public bool startVisible;
	public InteractiveSwitch nextStage;
	public bool levelEnd;
	public bool timeTrial;
	public AudioClip activateSound;

	private GameObject player;
	private TargetHealth boss;
	private TargetHealth box;
	private int minionsDead = 0;
	private State currentState;

	private TimeTrialManager timer;

	private enum State
	{
		DISABLED,
		MINIONS,
		BOSS,
		BOX,
		END
	}

	private void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		this.ToggleState(false, startVisible);
		timer = GameObject.Find("Timer").GetComponent<TimeTrialManager>();

		if (levelEnd)
		{
			currentState = State.END;
		}
		else
			currentState = State.DISABLED;
	}

	void Update()
	{
		switch (currentState)
		{
			case State.MINIONS:
				minionsDead = 0;
				foreach (TargetHealth target in targets)
				{
					if (!target.box&&!target.boss && target.IsDead)
					{
						StartCoroutine(DecreaseRroll(target.gameObject));
						//target.gameObject.SetActive(false);
						minionsDead++;
					}
				}
				if (minionsDead == targets.Count - 2)
				{
					boss.Revive();
					minionsDead++;
					currentState = State.BOSS;
				}
				break;
			case State.BOSS:
				if(boss.IsDead)
				{
					currentState = State.BOX;
					StartCoroutine(DecreaseRroll(boss.gameObject));
                    //this.ToggleState(false, false);
                    //if (nextStage)
                    //{
                    //    nextStage.ToggleState(false, true);
                    //}
                }
				break;
			case State.BOX:
				box.Revive();
                this.ToggleState(false, false);
                if (nextStage)
                {
                    nextStage.ToggleState(false, true);
                }
                break;
		}
	}

    IEnumerator DecreaseRroll(GameObject g)
    {
		g.GetComponent<CapsuleCollider>().enabled = false;
		g.GetComponent<trollManagers>().anim.SetFloat("death", 1.0F);	
        yield return new WaitForSeconds(1.4f);	
		g.SetActive(false);

    }

    public void ToggleState(bool active, bool visible)
	{
		if (active)
			currentState = State.MINIONS;
		else
			currentState = State.DISABLED;
		this.GetComponent<BoxCollider>().enabled = visible;
		this.GetComponent<MeshRenderer>().enabled = visible;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == player)
		{
			if (levelEnd)
			{
				timer.EndTimer();

				ToggleState(false, false);

				if (nextStage)
				{
					nextStage.ToggleState(false, true);
				}
			}
			else
			{
				if(timeTrial && !timer.IsRunning)
				{
					timer.StartTimer();
				}
				ToggleState(true, false);
				foreach (TargetHealth target in targets)
				{
					if(target.boss)
					{
						boss = target;
						boss.Kill();
					} else if(target.box)
                    {
						box = target;
						box.Kill();
                    }else
                    {
						target.Revive();
                    }
				}
			}
			AudioSource.PlayClipAtPoint(activateSound, transform.position + Vector3.up);
		}
	}
}

