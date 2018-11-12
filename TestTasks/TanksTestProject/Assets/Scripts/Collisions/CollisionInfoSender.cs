using UnityEngine;

namespace TankGame.Battle.Interaction
{
	public class CollisionInfoSender : MonoBehaviour
	{
		private void OnTriggerEnter2D(Collider2D collision)
		{
			GameCollisionManager.Instance.ProcessCollisionInfo(this.gameObject, collision.gameObject);
		}

		private void OnCollisionEnter2D(Collision2D collision)
		{
			GameCollisionManager.Instance.ProcessCollisionInfo(this.gameObject, collision.gameObject);
		}
	}
}
