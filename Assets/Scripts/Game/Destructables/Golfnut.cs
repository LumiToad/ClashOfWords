using UnityEngine;

namespace Clash_Of_Words.Destructable
{
    public class Golfnut : Destructable, IHitable
    {
        private HitBoxBase hitBy;

        private Kickable kickable;

        private GolfnutHPUI golfnutHPUI;

        private MeshController meshController;
        private MeshFilter meshFilter; 

        private Vector3 startTransform;

        private PlayerDTO playerDTO;
        public PlayerDTO PlayerDTO
        { 
            get => playerDTO;
            set 
            {
                playerDTO = value;
                TintNut();
            } 
        }
        
        [SerializeField]
        private int startHP = 8;
        public int StartHP { get => startHP; }

        private int hp;
        public int HP
        {
            get { return hp; } 
            private set 
            {
                hp = value;
                ChangeNutState();
                golfnutHPUI.SetHP(hp, startHP);
            }
        }

        public int hitsTaken = 0;

        private void Awake()
        {
            meshController = GetComponentInChildren<MeshController>();
            meshFilter = GetComponentInChildren<MeshFilter>();
            golfnutHPUI = GetComponentInChildren<GolfnutHPUI>();
            kickable = GetComponent<Kickable>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                hitsTaken = 0;
                ResetNut();
            }
        }

        private void Start()
        {
            kickable.powerModifier = 200.0f;
            startTransform = transform.position;
            golfnutHPUI.SetHP(hp, startHP);
            ResetNut();
        }

        private void ResetNut()
        {
            HP = startHP;
            transform.position = startTransform;
            Debug.Log(kickable);
            kickable.ResetVelocity();
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

            hitsTaken++;
        }

        protected override void Destruction()
        {
            Game.Instance.SFXPlayer.PlaySFXByType(SFXClip.NutCracking, gameObject);
            ResetNut();
        }

        private void TakeDamage(int damage)
        {
            HP -= damage;
        }

        private void ChangeNutState()
        {  
            if (HP == 0)
            {
                Destruction();
            }
            if (HP == 1)
            {
                meshController.ChangeMeshByKey("Nut1HP", meshFilter);
            }
            if (HP == startHP / 2 || HP == (startHP / 2) - 1)
            {
                meshController.ChangeMeshByKey("Nut2HP", meshFilter);
            }
            if (HP == startHP)
            {
                meshController.ChangeMeshByKey("Nut3HP", meshFilter);
            }
            TintNut();
        }

        private void TintNut()
        {
            Color color = Game.Instance.PlayerColorManager.GetColorByType(PlayerDTO.ColorType);
            meshController.TintMaterial(color);
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
