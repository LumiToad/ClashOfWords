using UnityEngine;


namespace Clash_Of_Words.Destructable
{
    public class Walnut : Destructable, IHitable
    {
        [SerializeField]
        private GameObject letterItem;

        [SerializeField]
        private GameObject football;

        private HitBoxBase hitBy;

        private MeshController MeshController => GetComponentInChildren<MeshController>();
        private MeshFilter MeshFilter => GetComponentInChildren<MeshFilter>();

        private int hp = 3;
        private int HP
        {
            get { return hp; } 
            set 
            {
                hp = value;
                ChangeNutState();
            }
        }

        protected override void OnHit(HitBoxBase hitBox)
        {
            var playerHitBox = hitBox.GetComponent<HitBoxBase>();

            if (playerHitBox == null) return;

            if (hitBox is PlayerTailHitBox)
            {
                TakeDamage(1);
            }

            if (hitBox is PlayerNutcrackerHitBox)
            {
                base.OnHit(hitBox);
            }
        }

        protected override void Destruction()
        {
            Game.Instance.SFXPlayer.PlaySFXByType(SFXClip.NutCracking, gameObject);
            DropItem();
            base.Destruction();
        }

        public void DropItem()
        {
            GameObject drop = RandomDrop();

            if (drop == null)
            {
                Debug.LogWarning("LUMI: Does not contain an item to drop!");
                return;
            }

            //DEBUG LUMI (game transform)
            GameObject goInstance = Instantiate(drop, transform.position, Quaternion.identity, FindObjectOfType<Game>().transform);

            ExcludeItemDropFromHitBox(goInstance);
        }

        private GameObject RandomDrop()
        {
            GameObject result = letterItem;

            float randomIndex = Random.Range(0, 100.0f);
            if (randomIndex <= 5.0f) 
            {
                result = football;
            }

            return result;
        }

        private void ExcludeItemDropFromHitBox(GameObject go)
        {
            foreach (IHitable hitable in go.GetComponentsInChildren<IHitable>())
            {
                hitBy.AddHitableToExcludeList(hitable);
            }
        }

        private void TakeDamage(int damage)
        {
            HP -= damage;
        }

        private void ChangeNutState()
        {
            switch (HP) 
            {
                case 0:
                    Destruction();
                    break;
                case 1:
                    MeshController.ChangeMeshByKey("Nut1HP", MeshFilter);
                    break;
                case 2:
                    MeshController.ChangeMeshByKey("Nut2HP", MeshFilter);
                    break;
                case 3:
                    MeshController.ChangeMeshByKey("Nut3HP", MeshFilter);
                    break;
            }
        }

        #region IHitable

        public void OnHitReceived(HitBoxBase hitBox)
        {
            hitBy = hitBox;

            OnHit(hitBox);
        }

        #endregion IHitable
    }
}
