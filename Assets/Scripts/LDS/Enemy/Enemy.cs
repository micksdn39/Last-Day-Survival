using LDS.GamePlay;
using UnityEngine;

namespace LDS.Enemy
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private GameObject root;
        [SerializeField] private int hp = 100;
        
        [SerializeField] private ItemDropSO drop;
        public void OnHit(int damage)
        {
            hp -= damage;
            if (hp <= 0)
            {
                var dropList = drop.GetDrop();
                GameManagers.Instance().UpdateInventory(dropList);
                Destroy(root);
            }
        }
    }
}
