using System.Collections;
using UnityEngine;

public class Explorer2D : MonoBehaviour
{

	private enum Mode { simple, adaptive }
	[SerializeField] private Mode mode;
	[SerializeField] private float radius;
	[SerializeField] private float power;
	[SerializeField] private LayerMask layerMask;
	private GameObject go = null;

	public void Explosion2D(Vector3 position)
	{
		Collider2D[] colliders = Physics2D.OverlapCircleAll(position, radius, layerMask);

        for (int i = 0; i < colliders.Length; i++)
        {
			GameObject Coll = colliders[i].gameObject;
            if (Coll.tag=="Player")
            {
				if (go == Coll) return;
				go = Coll.gameObject;
				Debug.LogError("palyer");
                if (transform.root.gameObject.tag=="heroExplosive")
                {
					Coll.GetComponent<PlayerMeneger>().Takedamage(WeaponType.Explosives);

				}
                else
                {
					Coll.GetComponent<PlayerMeneger>().Takedamage(GetComponent<projectileEnemyGun>().enemy_type);
				}
               
                
            }
			if (Coll.gameObject.tag=="bonus_up")
            {
				Coll.GetComponent<bonus_up>().Explorer();
            }
			if (Coll.tag == "Ground" && Coll.GetComponent<BlicSprit>())
            {
				Coll.GetComponent<BlicSprit>().blic_mateial();
            }
            if (Coll.tag=="enemyHelic")
            {
				Coll.GetComponent<EnemyGunMeneger>().TakeDamageOnBomb();
				//Debug.LogError(Coll);
			}
    //        if (Coll.tag=="heroExplosive")
    //        {
				//Coll.GetComponent<Explorer2D>().Explosion2D(Coll.transform.position);
    //        }
        }
		foreach (Collider2D hit in colliders)
		{
			if (hit.attachedRigidbody != null)
			{
				Vector3 direction = hit.transform.position - position;
				direction.z = 0;

				if (CanUse(position, hit.attachedRigidbody))
				{
					hit.attachedRigidbody.AddForce(direction.normalized * power);
				}
			}
		}
	}

	bool CanUse(Vector3 position, Rigidbody2D body)
	{
		if (mode == Mode.simple) return true;

		RaycastHit2D hit = Physics2D.Linecast(position, body.position);

		if (hit.rigidbody != null && hit.rigidbody == body)
		{
			return true;
		}

		return false;
	}

	
}
