using UnityEngine;

namespace Game.Gameplay.Projectiles
{
    //TODO: Remove monobehaviour
    public class ProjectileView : MonoBehaviour
    {
        [SerializeField] private bool _pierceThrough = false;
        [SerializeField] private float _distance = 100f;
        [SerializeField] private LayerMask _layerMask;


        private RaycastHit2D _hit;
        private void Start()
        {
            _hit = Physics2D.Raycast(transform.position, transform.up, _distance, _layerMask);
            if(_hit.collider != null)
            {
                Debug.Log($"Hitted some object with name {_hit.collider.gameObject.name}");
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, _hit.point);
        }
    }
}