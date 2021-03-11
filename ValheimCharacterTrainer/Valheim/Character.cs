//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace Valheim
//{
//    public class Character
//    {
//        private static int forward_speed = 0;
//        private static int sideway_speed = 0;
//        private static int turn_speed = 0;
//        private static int inWater = 0;
//        private static int onGround = 0;
//        private static int encumbered = 0;
//        private static int flying = 0;
//        private static int m_smokeRayMask = 0;
//        private static bool m_dpsDebugEnabled = false;
//        private static List<KeyValuePair<float, float>> m_enemyDamage = new List<KeyValuePair<float, float>>();
//        private static List<KeyValuePair<float, float>> m_playerDamage = new List<KeyValuePair<float, float>>();
//        private static List<Character> m_characters = new List<Character>();
//        protected static int m_characterLayer = 0;
//        protected static int m_characterNetLayer = 0;
//        protected static int m_characterGhostLayer = 0;
//        protected static int m_animatorTagFreeze = Animator.StringToHash("freeze");
//        protected static int m_animatorTagStagger = Animator.StringToHash("stagger");
//        protected static int m_animatorTagSitting = Animator.StringToHash("sitting");
//        private float m_underWorldCheckTimer;
//        private Collider m_lowestContactCollider;
//        private bool m_groundContact;
//        private Vector3 m_groundContactPoint;
//        private Vector3 m_groundContactNormal;
//        public Action<float, Character> m_onDamaged;
//        public Action m_onDeath;
//        public Action<int> m_onLevelSet;
//        public Action<Vector3> m_onLand;
//        [Header("Character")]
//        public string m_name;
//        public Character.Faction m_faction;
//        public bool m_boss;
//        public string m_bossEvent;
//        public string m_defeatSetGlobalKey;
//        [Header("Movement & Physics")]
//        public float m_crouchSpeed;
//        public float m_walkSpeed;
//        public float m_speed;
//        public float m_turnSpeed;
//        public float m_runSpeed;
//        public float m_runTurnSpeed;
//        public float m_flySlowSpeed;
//        public float m_flyFastSpeed;
//        public float m_flyTurnSpeed;
//        public float m_acceleration;
//        public float m_jumpForce;
//        public float m_jumpForceForward;
//        public float m_jumpForceTiredFactor;
//        public float m_airControl;
//        private const float m_slopeStaminaDrain = 10f;
//        public const float m_minSlideDegreesPlayer = 38f;
//        public const float m_minSlideDegreesMonster = 90f;
//        private const float m_rootMotionMultiplier = 55f;
//        private const float m_continousPushForce = 10f;
//        private const float m_pushForcedissipation = 100f;
//        private const float m_maxMoveForce = 20f;
//        public bool m_canSwim;
//        public float m_swimDepth;
//        public float m_swimSpeed;
//        public float m_swimTurnSpeed;
//        public float m_swimAcceleration;
//        public Character.GroundTiltType m_groundTilt;
//        public bool m_flying;
//        public float m_jumpStaminaUsage;
//        [Header("Bodyparts")]
//        public Transform m_eye;
//        protected Transform m_head;
//        [Header("Effects")]
//        public EffectList m_hitEffects;
//        public EffectList m_critHitEffects;
//        public EffectList m_backstabHitEffects;
//        public EffectList m_deathEffects;
//        public EffectList m_waterEffects;
//        public EffectList m_slideEffects;
//        public EffectList m_jumpEffects;
//        [Header("Health & Damage")]
//        public bool m_tolerateWater;
//        public bool m_tolerateFire;
//        public bool m_tolerateSmoke;
//        public float m_health;
//        public HitData.DamageModifiers m_damageModifiers;
//        public bool m_staggerWhenBlocked;
//        public float m_staggerDamageFactor;
//        private const float m_staggerResetTime = 3f;
//        private float m_staggerDamage;
//        private float m_staggerTimer;
//        private float m_backstabTime;
//        private const float m_backstabResetTime = 300f;
//        private GameObject[] m_waterEffects_instances;
//        private GameObject[] m_slideEffects_instances;
//        protected Vector3 m_moveDir;
//        protected Vector3 m_lookDir;
//        protected Quaternion m_lookYaw;
//        protected bool m_run;
//        protected bool m_walk;
//        protected bool m_attack;
//        protected bool m_attackDraw;
//        protected bool m_secondaryAttack;
//        protected bool m_blocking;
//        protected GameObject m_visual;
//        protected LODGroup m_lodGroup;
//        protected Rigidbody m_body;
//        protected CapsuleCollider m_collider;
//        protected ZNetView m_nview;
//        protected ZSyncAnimation m_zanim;
//        protected Animator m_animator;
//        protected CharacterAnimEvent m_animEvent;
//        protected BaseAI m_baseAI;
//        private const float m_maxFallHeight = 20f;
//        private const float m_minFallHeight = 4f;
//        private const float m_maxFallDamage = 100f;
//        private const float m_staggerDamageBonus = 2f;
//        private const float m_baseVisualRange = 30f;
//        private const float m_autoJumpInterval = 0.5f;
//        private float m_jumpTimer;
//        private float m_lastAutoJumpTime;
//        private float m_lastGroundTouch;
//        private Vector3 m_lastGroundNormal;
//        private Vector3 m_lastGroundPoint;
//        private Collider m_lastGroundCollider;
//        private Rigidbody m_lastGroundBody;
//        private Vector3 m_lastAttachPos;
//        private Rigidbody m_lastAttachBody;
//        protected float m_maxAirAltitude;
//        protected float m_waterLevel;
//        private float m_swimTimer;
//        protected SEMan m_seman;
//        private float m_noiseRange;
//        private float m_syncNoiseTimer;
//        private bool m_tamed;
//        private float m_lastTamedCheck;
//        private int m_level;
//        private Vector3 m_currentVel;
//        private float m_currentTurnVel;
//        private float m_currentTurnVelChange;
//        private Vector3 m_groundTiltNormal;
//        protected Vector3 m_pushForce;
//        private Vector3 m_rootMotion;
//        private float m_slippage;
//        protected bool m_wallRunning;
//        protected bool m_sliding;
//        protected bool m_running;
//        private Vector3 m_originalLocalRef;
//        private bool m_lodVisible;
//        private float m_smokeCheckTimer;

//        protected virtual void Awake()
//        {
//            Character.m_characters.Add(this);
//            this.m_collider = (CapsuleCollider)((Component)this).GetComponent<CapsuleCollider>();
//            this.m_body = (Rigidbody)((Component)this).GetComponent<Rigidbody>();
//            this.m_zanim = (ZSyncAnimation)((Component)this).GetComponent<ZSyncAnimation>();
//            this.m_nview = (ZNetView)((Component)this).GetComponent<ZNetView>();
//            this.m_animator = (Animator)((Component)this).GetComponentInChildren<Animator>();
//            this.m_animEvent = (CharacterAnimEvent)((Component)this.m_animator).GetComponent<CharacterAnimEvent>();
//            this.m_baseAI = (BaseAI)((Component)this).GetComponent<BaseAI>();
//            this.m_animator.set_logWarnings(false);
//            this.m_visual = ((Component)((Component)this).get_transform().Find("Visual")).get_gameObject();
//            this.m_lodGroup = (LODGroup)this.m_visual.GetComponent<LODGroup>();
//            this.m_head = this.m_animator.GetBoneTransform((HumanBodyBones)10);
//            this.m_body.maxDepenetrationVelocity = 2f;
//            if (Character.m_smokeRayMask == 0)
//            {
//                Character.m_smokeRayMask = LayerMask.GetMask(new string[1]
//                {
//        "smoke"
//                });
//                Character.m_characterLayer = LayerMask.NameToLayer("character");
//                Character.m_characterNetLayer = LayerMask.NameToLayer("character_net");
//                Character.m_characterGhostLayer = LayerMask.NameToLayer("character_ghost");
//            }
//            if (Character.forward_speed == 0)
//            {
//                Character.forward_speed = ZSyncAnimation.GetHash("forward_speed");
//                Character.sideway_speed = ZSyncAnimation.GetHash("sideway_speed");
//                Character.turn_speed = ZSyncAnimation.GetHash("turn_speed");
//                Character.inWater = ZSyncAnimation.GetHash("inWater");
//                Character.onGround = ZSyncAnimation.GetHash("onGround");
//                Character.encumbered = ZSyncAnimation.GetHash("encumbered");
//                Character.flying = ZSyncAnimation.GetHash("flying");
//            }
//            if (UnityEngine.Object.op_Implicit((UnityEngine.Object)this.m_lodGroup))
//                this.m_originalLocalRef = this.m_lodGroup.get_localReferencePoint();
//            this.m_seman = new SEMan(this, this.m_nview);
//            if (this.m_nview.GetZDO() == null)
//                return;
//            if (!this.IsPlayer())
//            {
//                if (this.m_nview.IsOwner() && (double)this.GetHealth() == (double)this.GetMaxHealth())
//                    this.SetupMaxHealth();
//                this.m_tamed = this.m_nview.GetZDO().GetBool("tamed", this.m_tamed);
//                this.m_level = this.m_nview.GetZDO().GetInt("level", 1);
//            }
//            this.m_nview.Register<HitData>("Damage", new Action<long, HitData>(this.RPC_Damage));
//            this.m_nview.Register<float, bool>("Heal", new Action<long, float, bool>(this.RPC_Heal));
//            this.m_nview.Register<float>("AddNoise", new Action<long, float>(this.RPC_AddNoise));
//            this.m_nview.Register<Vector3>("Stagger", new Action<long, Vector3>(this.RPC_Stagger));
//            this.m_nview.Register("ResetCloth", new Action<long>(this.RPC_ResetCloth));
//            this.m_nview.Register<bool>("SetTamed", new Action<long, bool>(this.RPC_SetTamed));
//        }

//        private void SetupMaxHealth()
//        {
//            this.SetMaxHealth(this.m_health * Game.instance.GetDifficultyHealthScale(((Component)this).get_transform().get_position()) * (float)this.GetLevel());
//        }

//        protected virtual void Start()
//        {
//            this.m_nview.GetZDO();
//        }

//        public virtual void OnDestroy()
//        {
//            this.m_seman.OnDestroy();
//            Character.m_characters.Remove(this);
//        }

//        public void SetLevel(int level)
//        {
//            if (level < 1)
//                return;
//            this.m_level = level;
//            this.m_nview.GetZDO().Set(nameof(level), level);
//            this.SetupMaxHealth();
//            if (this.m_onLevelSet == null)
//                return;
//            this.m_onLevelSet(this.m_level);
//        }

//        public int GetLevel()
//        {
//            return this.m_level;
//        }

//        public virtual bool IsPlayer()
//        {
//            return false;
//        }

//        public Character.Faction GetFaction()
//        {
//            return this.m_faction;
//        }

//        protected virtual void FixedUpdate()
//        {
//            if (!this.m_nview.IsValid())
//                return;
//            float fixedDeltaTime = Time.get_fixedDeltaTime();
//            this.UpdateLayer();
//            this.UpdateContinousEffects();
//            this.UpdateWater(fixedDeltaTime);
//            this.UpdateGroundTilt(fixedDeltaTime);
//            this.SetVisible(this.m_nview.HasOwner());
//            if (!this.m_nview.IsOwner())
//                return;
//            this.UpdateGroundContact(fixedDeltaTime);
//            this.UpdateNoise(fixedDeltaTime);
//            this.m_seman.Update(fixedDeltaTime);
//            this.UpdateStagger(fixedDeltaTime);
//            this.UpdatePushback(fixedDeltaTime);
//            this.UpdateMotion(fixedDeltaTime);
//            this.UpdateSmoke(fixedDeltaTime);
//            this.UnderWorldCheck(fixedDeltaTime);
//            this.SyncVelocity();
//            this.CheckDeath();
//        }

//        private void UpdateLayer()
//        {
//            if (this.m_collider.get_gameObject().get_layer() != Character.m_characterLayer && this.m_collider.get_gameObject().get_layer() != Character.m_characterNetLayer)
//                return;
//            if (this.m_nview.IsOwner())
//                this.m_collider.get_gameObject().set_layer(this.IsAttached() ? Character.m_characterNetLayer : Character.m_characterLayer);
//            else
//                this.m_collider.get_gameObject().set_layer(Character.m_characterNetLayer);
//        }

//        private void UnderWorldCheck(float dt)
//        {
//            if (this.IsDead())
//                return;
//            this.m_underWorldCheckTimer += dt;
//            if ((double)this.m_underWorldCheckTimer <= 5.0 && !this.IsPlayer())
//                return;
//            this.m_underWorldCheckTimer = 0.0f;
//            float groundHeight = ZoneSystem.instance.GetGroundHeight(((Component)this).get_transform().get_position());
//            if (((Component)this).get_transform().get_position().y >= (double)groundHeight - 1.0)
//                return;
//            Vector3 position = ((Component)this).get_transform().get_position();
//            position.y = (__Null)((double)groundHeight + 0.5);
//            ((Component)this).get_transform().set_position(position);
//            this.m_body.position = position;
//            this.m_body.velocity = Vector3.get_zero();
//        }

//        private void UpdateSmoke(float dt)
//        {
//            if (this.m_tolerateSmoke)
//                return;
//            this.m_smokeCheckTimer += dt;
//            if ((double)this.m_smokeCheckTimer <= 2.0)
//                return;
//            this.m_smokeCheckTimer = 0.0f;
//            if (Physics.CheckSphere(Vector3.op_Addition(this.GetTopPoint(), Vector3.op_Multiply(Vector3.get_up(), 0.1f)), 0.5f, Character.m_smokeRayMask))
//                this.m_seman.AddStatusEffect("Smoked", true);
//            else
//                this.m_seman.RemoveStatusEffect("Smoked", true);
//        }

//        private void UpdateContinousEffects()
//        {
//            this.SetupContinousEffect(((Component)this).get_transform().get_position(), this.m_sliding, this.m_slideEffects, ref this.m_slideEffects_instances);
//            Vector3 position = ((Component)this).get_transform().get_position();
//            position.y = (__Null)((double)this.m_waterLevel + 0.0500000007450581);
//            this.SetupContinousEffect(position, this.InWater(), this.m_waterEffects, ref this.m_waterEffects_instances);
//        }

//        private void SetupContinousEffect(
//          Vector3 point,
//          bool enabled,
//          EffectList effects,
//          ref GameObject[] instances)
//        {
//            if (!effects.HasEffects())
//                return;
//            if (enabled)
//            {
//                if (instances == null)
//                {
//                    instances = effects.Create(point, Quaternion.get_identity(), ((Component)this).get_transform(), 1f);
//                }
//                else
//                {
//                    foreach (GameObject gameObject in instances)
//                    {
//                        if (UnityEngine.Object.op_Implicit((UnityEngine.Object)gameObject))
//                            gameObject.get_transform().set_position(point);
//                    }
//                }
//            }
//            else
//            {
//                if (instances == null)
//                    return;
//                foreach (GameObject gameObject in instances)
//                {
//                    if (UnityEngine.Object.op_Implicit((UnityEngine.Object)gameObject))
//                    {
//                        foreach (ParticleSystem componentsInChild in (ParticleSystem[])gameObject.GetComponentsInChildren<ParticleSystem>())
//                        {
//                            ParticleSystem.EmissionModule emission = componentsInChild.get_emission();
//                            ((ParticleSystem.EmissionModule)ref emission).set_enabled(false);
//                            componentsInChild.Stop();
//                        }
//                        CamShaker componentInChildren1 = (CamShaker)gameObject.GetComponentInChildren<CamShaker>();
//                        if (UnityEngine.Object.op_Implicit((UnityEngine.Object)componentInChildren1))
//                            UnityEngine.Object.Destroy((UnityEngine.Object)componentInChildren1);
//                        ZSFX componentInChildren2 = (ZSFX)gameObject.GetComponentInChildren<ZSFX>();
//                        if (UnityEngine.Object.op_Implicit((UnityEngine.Object)componentInChildren2))
//                            componentInChildren2.FadeOut();
//                        TimedDestruction component = (TimedDestruction)gameObject.GetComponent<TimedDestruction>();
//                        if (UnityEngine.Object.op_Implicit((UnityEngine.Object)component))
//                            component.Trigger();
//                        else
//                            UnityEngine.Object.Destroy((UnityEngine.Object)gameObject);
//                    }
//                }
//                instances = (GameObject[])null;
//            }
//        }

//        protected virtual void OnSwiming(Vector3 targetVel, float dt)
//        {
//        }

//        protected virtual void OnSneaking(float dt)
//        {
//        }

//        protected virtual void OnJump()
//        {
//        }

//        protected virtual bool TakeInput()
//        {
//            return true;
//        }

//        private float GetSlideAngle()
//        {
//            return !this.IsPlayer() ? 90f : 38f;
//        }

//        private void ApplySlide(float dt, ref Vector3 currentVel, Vector3 bodyVel, bool running)
//        {
//            bool flag1 = this.CanWallRun();
//            double num = (double)Mathf.Acos(Mathf.Clamp01((float)this.m_lastGroundNormal.y)) * 57.2957801818848;
//            Vector3 lastGroundNormal = this.m_lastGroundNormal;
//            lastGroundNormal.y = (__Null)0.0;
//            ((Vector3)ref lastGroundNormal).Normalize();
//            Vector3 velocity = this.m_body.velocity;
//            Vector3 vector3_1 = Vector3.Cross(this.m_lastGroundNormal, Vector3.Cross(this.m_lastGroundNormal, Vector3.get_up()));
//            bool flag2 = (double)((Vector3)ref currentVel).get_magnitude() > 0.100000001490116;
//            double slideAngle = (double)this.GetSlideAngle();
//            if (num > slideAngle)
//            {
//                if (running & flag1 & flag2)
//                {
//                    this.UseStamina(10f * dt);
//                    this.m_slippage = 0.0f;
//                    this.m_wallRunning = true;
//                }
//                else
//                    this.m_slippage = Mathf.MoveTowards(this.m_slippage, 1f, 1f * dt);
//                Vector3 vector3_2 = Vector3.op_Multiply(vector3_1, 5f);
//                currentVel = Vector3.Lerp(currentVel, vector3_2, this.m_slippage);
//                this.m_sliding = (double)this.m_slippage > 0.5;
//            }
//            else
//                this.m_slippage = 0.0f;
//        }

//        private void UpdateMotion(float dt)
//        {
//            this.UpdateBodyFriction();
//            this.m_sliding = false;
//            this.m_wallRunning = false;
//            this.m_running = false;
//            if (this.IsDead())
//                return;
//            if (this.IsDebugFlying())
//            {
//                this.UpdateDebugFly(dt);
//            }
//            else
//            {
//                if (this.InIntro())
//                {
//                    this.m_maxAirAltitude = (float)((Component)this).get_transform().get_position().y;
//                    this.m_body.velocity = Vector3.get_zero();
//                    this.m_body.angularVelocity = Vector3.get_zero();
//                }
//                if (!this.InWaterSwimDepth() && !this.IsOnGround())
//                    this.m_maxAirAltitude = Mathf.Max(this.m_maxAirAltitude, (float)((Component)this).get_transform().get_position().y);
//                if (this.IsSwiming())
//                    this.UpdateSwiming(dt);
//                else if (this.m_flying)
//                    this.UpdateFlying(dt);
//                else
//                    this.UpdateWalking(dt);
//                this.m_lastGroundTouch += Time.get_fixedDeltaTime();
//                this.m_jumpTimer += Time.get_fixedDeltaTime();
//            }
//        }

//        private void UpdateDebugFly(float dt)
//        {
//            float num = this.m_run ? 50f : 20f;
//            Vector3 vector3 = Vector3.op_Multiply(this.m_moveDir, num);
//            if (this.TakeInput())
//            {
//                if (ZInput.GetButton("Jump"))
//                    vector3.y = (__Null)(double)num;
//                else if (Input.GetKey((KeyCode)306))
//                    vector3.y = (__Null) - (double)num;
//            }
//            this.m_currentVel = Vector3.Lerp(this.m_currentVel, vector3, 0.5f);
//            this.m_body.velocity = this.m_currentVel;
//            this.m_body.useGravity = false;
//            this.m_lastGroundTouch = 0.0f;
//            this.m_maxAirAltitude = (float)((Component)this).get_transform().get_position().y;
//            this.m_body.rotation = Quaternion.RotateTowards(((Component)this).get_transform().get_rotation(), this.m_lookYaw, this.m_turnSpeed * dt);
//            this.m_body.angularVelocity = Vector3.get_zero();
//            this.UpdateEyeRotation();
//        }

//        private void UpdateSwiming(float dt)
//        {
//            bool flag = this.IsOnGround();
//            if ((double)Mathf.Max(0.0f, this.m_maxAirAltitude - (float)((Component)this).get_transform().get_position().y) > 0.5 && this.m_onLand != null)
//                this.m_onLand(new Vector3((float)((Component)this).get_transform().get_position().x, this.m_waterLevel, (float)((Component)this).get_transform().get_position().z));
//            this.m_maxAirAltitude = (float)((Component)this).get_transform().get_position().y;
//            float speed = this.m_swimSpeed * this.GetAttackSpeedFactorMovement();
//            if (this.InMinorAction())
//                speed = 0.0f;
//            this.m_seman.ApplyStatusEffectSpeedMods(ref speed);
//            Vector3 targetVel = Vector3.op_Multiply(this.m_moveDir, speed);
//            if ((double)((Vector3)ref targetVel).get_magnitude() > 0.0 && this.IsOnGround())
//            {
//                Vector3 vector3 = Vector3.ProjectOnPlane(targetVel, this.m_lastGroundNormal);
//                targetVel = Vector3.op_Multiply(((Vector3)ref vector3).get_normalized(), ((Vector3)ref targetVel).get_magnitude());
//            }
//            if (this.IsPlayer())
//            {
//                this.m_currentVel = Vector3.Lerp(this.m_currentVel, targetVel, this.m_swimAcceleration);
//            }
//            else
//            {
//                float magnitude1 = ((Vector3)ref targetVel).get_magnitude();
//                float magnitude2 = ((Vector3)ref this.m_currentVel).get_magnitude();
//                if ((double)magnitude1 > (double)magnitude2)
//                {
//                    float num = Mathf.MoveTowards(magnitude2, magnitude1, this.m_swimAcceleration);
//                    targetVel = Vector3.op_Multiply(((Vector3)ref targetVel).get_normalized(), num);
//                }
//                this.m_currentVel = Vector3.Lerp(this.m_currentVel, targetVel, 0.5f);
//            }
//            if ((double)((Vector3)ref targetVel).get_magnitude() > 0.100000001490116)
//                this.AddNoise(15f);
//            this.AddPushbackForce(ref this.m_currentVel);
//            Vector3 force = Vector3.op_Subtraction(this.m_currentVel, this.m_body.velocity);
//            force.y = (__Null)0.0;
//            if ((double)((Vector3)ref force).get_magnitude() > 20.0)
//                force = Vector3.op_Multiply(((Vector3)ref force).get_normalized(), 20f);
//            this.m_body.AddForce(force, ForceMode.VelocityChange);
//            float num1 = this.m_waterLevel - this.m_swimDepth;
//            if (((Component)this).get_transform().get_position().y < (double)num1)
//            {
//                float num2 = Mathf.Lerp(0.0f, 10f, Mathf.Clamp01((float)(((double)num1 - ((Component)this).get_transform().get_position().y) / 2.0)));
//                Vector3 velocity = this.m_body.velocity;
//                velocity.y = (__Null)(double)Mathf.MoveTowards((float)velocity.y, num2, 50f * dt);
//                this.m_body.velocity = velocity;
//            }
//            else
//            {
//                float num2 = Mathf.Lerp(0.0f, 10f, Mathf.Clamp01((float)(-((double)num1 - ((Component)this).get_transform().get_position().y) / 1.0)));
//                Vector3 velocity = this.m_body.velocity;
//                velocity.y = (__Null)(double)Mathf.MoveTowards((float)velocity.y, -num2, 30f * dt);
//                this.m_body.velocity = velocity;
//            }
//            float num3 = 0.0f;
//            if ((double)((Vector3)ref this.m_moveDir).get_magnitude() > 0.100000001490116 || this.AlwaysRotateCamera())
//            {
//                float swimTurnSpeed = this.m_swimTurnSpeed;
//                this.m_seman.ApplyStatusEffectSpeedMods(ref swimTurnSpeed);
//                num3 = this.UpdateRotation(swimTurnSpeed, dt);
//            }
//            this.m_body.angularVelocity = Vector3.get_zero();
//            this.UpdateEyeRotation();
//            this.m_body.useGravity = true;
//            float num4 = Vector3.Dot(this.m_currentVel, ((Component)this).get_transform().get_forward());
//            float num5 = Vector3.Dot(this.m_currentVel, ((Component)this).get_transform().get_right());
//            float num6 = Vector3.Dot(this.m_body.velocity, ((Component)this).get_transform().get_forward());
//            this.m_currentTurnVel = Mathf.SmoothDamp(this.m_currentTurnVel, num3, ref this.m_currentTurnVelChange, 0.5f, 99f);
//            this.m_zanim.SetFloat(Character.forward_speed, this.IsPlayer() ? num4 : num6);
//            this.m_zanim.SetFloat(Character.sideway_speed, num5);
//            this.m_zanim.SetFloat(Character.turn_speed, this.m_currentTurnVel);
//            this.m_zanim.SetBool(Character.inWater, !flag);
//            this.m_zanim.SetBool(Character.onGround, false);
//            this.m_zanim.SetBool(Character.encumbered, false);
//            this.m_zanim.SetBool(Character.flying, false);
//            if (flag)
//                return;
//            this.OnSwiming(targetVel, dt);
//        }

//        private void UpdateFlying(float dt)
//        {
//            float num1 = (this.m_run ? this.m_flyFastSpeed : this.m_flySlowSpeed) * this.GetAttackSpeedFactorMovement();
//            this.m_currentVel = Vector3.Lerp(this.m_currentVel, this.CanMove() ? Vector3.op_Multiply(this.m_moveDir, num1) : Vector3.get_zero(), this.m_acceleration);
//            this.m_maxAirAltitude = (float)((Component)this).get_transform().get_position().y;
//            this.ApplyRootMotion(ref this.m_currentVel);
//            this.AddPushbackForce(ref this.m_currentVel);
//            Vector3 force = Vector3.op_Subtraction(this.m_currentVel, this.m_body.velocity);
//            if ((double)((Vector3)ref force).get_magnitude() > 20.0)
//                force = Vector3.op_Multiply(((Vector3)ref force).get_normalized(), 20f);
//            this.m_body.AddForce(force, ForceMode.VelocityChange);
//            float num2 = 0.0f;
//            if (((double)((Vector3)ref this.m_moveDir).get_magnitude() > 0.100000001490116 || this.AlwaysRotateCamera()) && (!this.InDodge() && this.CanMove()))
//            {
//                float flyTurnSpeed = this.m_flyTurnSpeed;
//                this.m_seman.ApplyStatusEffectSpeedMods(ref flyTurnSpeed);
//                num2 = this.UpdateRotation(flyTurnSpeed, dt);
//            }
//            this.m_body.angularVelocity = Vector3.get_zero();
//            this.UpdateEyeRotation();
//            this.m_body.useGravity = false;
//            float num3 = Vector3.Dot(this.m_currentVel, ((Component)this).get_transform().get_forward());
//            float num4 = Vector3.Dot(this.m_currentVel, ((Component)this).get_transform().get_right());
//            float num5 = Vector3.Dot(this.m_body.velocity, ((Component)this).get_transform().get_forward());
//            this.m_currentTurnVel = Mathf.SmoothDamp(this.m_currentTurnVel, num2, ref this.m_currentTurnVelChange, 0.5f, 99f);
//            this.m_zanim.SetFloat(Character.forward_speed, this.IsPlayer() ? num3 : num5);
//            this.m_zanim.SetFloat(Character.sideway_speed, num4);
//            this.m_zanim.SetFloat(Character.turn_speed, this.m_currentTurnVel);
//            this.m_zanim.SetBool(Character.inWater, false);
//            this.m_zanim.SetBool(Character.onGround, false);
//            this.m_zanim.SetBool(Character.encumbered, false);
//            this.m_zanim.SetBool(Character.flying, true);
//        }

//        private void UpdateWalking(float dt)
//        {
//            Vector3 moveDir = this.m_moveDir;
//            bool flag = this.IsCrouching();
//            this.m_running = this.CheckRun(moveDir, dt);
//            float num1 = this.m_speed * this.GetJogSpeedFactor();
//            if ((this.m_walk || this.InMinorAction()) && !flag)
//                num1 = this.m_walkSpeed;
//            else if (this.m_running)
//            {
//                int num2 = (double)this.InWaterDepth() > 0.400000005960464 ? 1 : 0;
//                float num3 = this.m_runSpeed * this.GetRunSpeedFactor();
//                num1 = num2 != 0 ? Mathf.Lerp(num1, num3, 0.33f) : num3;
//                if (this.IsPlayer() && (double)((Vector3)ref moveDir).get_magnitude() > 0.0)
//                    ((Vector3)ref moveDir).Normalize();
//            }
//            else if (flag || this.IsEncumbered())
//                num1 = this.m_crouchSpeed;
//            float speed1 = num1 * this.GetAttackSpeedFactorMovement();
//            this.m_seman.ApplyStatusEffectSpeedMods(ref speed1);
//            Vector3 targetVel = this.CanMove() ? Vector3.op_Multiply(moveDir, speed1) : Vector3.get_zero();
//            Vector3 vector3;
//            if ((double)((Vector3)ref targetVel).get_magnitude() > 0.0 && this.IsOnGround())
//            {
//                vector3 = Vector3.ProjectOnPlane(targetVel, this.m_lastGroundNormal);
//                targetVel = Vector3.op_Multiply(((Vector3)ref vector3).get_normalized(), ((Vector3)ref targetVel).get_magnitude());
//            }
//            float magnitude1 = ((Vector3)ref targetVel).get_magnitude();
//            float magnitude2 = ((Vector3)ref this.m_currentVel).get_magnitude();
//            if ((double)magnitude1 > (double)magnitude2)
//            {
//                float num2 = Mathf.MoveTowards(magnitude2, magnitude1, this.m_acceleration);
//                targetVel = Vector3.op_Multiply(((Vector3)ref targetVel).get_normalized(), num2);
//            }
//            else if (this.IsPlayer())
//            {
//                float num2 = Mathf.MoveTowards(magnitude2, magnitude1, this.m_acceleration * 2f);
//                targetVel = (double)((Vector3)ref targetVel).get_magnitude() > 0.0 ? Vector3.op_Multiply(((Vector3)ref targetVel).get_normalized(), num2) : Vector3.op_Multiply(((Vector3)ref this.m_currentVel).get_normalized(), num2);
//            }
//            this.m_currentVel = Vector3.Lerp(this.m_currentVel, targetVel, 0.5f);
//            Vector3 velocity = this.m_body.velocity;
//            Vector3 currentVel1 = this.m_currentVel;
//            currentVel1.y = velocity.y;
//            if (this.IsOnGround() && UnityEngine.Object.op_Equality((UnityEngine.Object)this.m_lastAttachBody, (UnityEngine.Object)null))
//                this.ApplySlide(dt, ref currentVel1, velocity, this.m_running);
//            this.ApplyRootMotion(ref currentVel1);
//            this.AddPushbackForce(ref currentVel1);
//            this.ApplyGroundForce(ref currentVel1, targetVel);
//            Vector3 force = Vector3.op_Subtraction(currentVel1, velocity);
//            if (!this.IsOnGround())
//                force = (double)((Vector3)ref targetVel).get_magnitude() <= 0.100000001490116 ? Vector3.get_zero() : Vector3.op_Multiply(force, this.m_airControl);
//            if (this.IsAttached())
//                force = Vector3.get_zero();
//            if (this.IsSneaking())
//                this.OnSneaking(dt);
//            if ((double)((Vector3)ref force).get_magnitude() > 20.0)
//                force = Vector3.op_Multiply(((Vector3)ref force).get_normalized(), 20f);
//            if ((double)((Vector3)ref force).get_magnitude() > 0.00999999977648258)
//                this.m_body.AddForce(force, ForceMode.VelocityChange);
//            if (UnityEngine.Object.op_Implicit((UnityEngine.Object)this.m_lastGroundBody) && this.m_lastGroundBody.get_gameObject().get_layer() != ((Component)this).get_gameObject().get_layer() && (double)this.m_lastGroundBody.mass > (double)this.m_body.mass)
//            {
//                float num2 = this.m_body.mass / this.m_lastGroundBody.mass;
//                this.m_lastGroundBody.AddForceAtPosition(Vector3.op_Multiply(Vector3.op_UnaryNegation(force), num2), ((Component)this).get_transform().get_position(), ForceMode.VelocityChange);
//            }
//            float num4 = 0.0f;
//            if (((double)((Vector3)ref moveDir).get_magnitude() > 0.100000001490116 || this.AlwaysRotateCamera()) && (!this.InDodge() && this.CanMove()))
//            {
//                float speed2 = this.m_run ? this.m_runTurnSpeed : this.m_turnSpeed;
//                this.m_seman.ApplyStatusEffectSpeedMods(ref speed2);
//                num4 = this.UpdateRotation(speed2, dt);
//            }
//            this.UpdateEyeRotation();
//            this.m_body.useGravity = true;
//            Vector3 currentVel2 = this.m_currentVel;
//            vector3 = Vector3.ProjectOnPlane(((Component)this).get_transform().get_forward(), this.m_lastGroundNormal);
//            Vector3 normalized1 = ((Vector3)ref vector3).get_normalized();
//            float num5 = Vector3.Dot(currentVel2, normalized1);
//            Vector3 currentVel3 = this.m_currentVel;
//            vector3 = Vector3.ProjectOnPlane(((Component)this).get_transform().get_right(), this.m_lastGroundNormal);
//            Vector3 normalized2 = ((Vector3)ref vector3).get_normalized();
//            float num6 = Vector3.Dot(currentVel3, normalized2);
//            float num7 = Vector3.Dot(this.m_body.velocity, ((Component)this).get_transform().get_forward());
//            this.m_currentTurnVel = Mathf.SmoothDamp(this.m_currentTurnVel, num4, ref this.m_currentTurnVelChange, 0.5f, 99f);
//            this.m_zanim.SetFloat(Character.forward_speed, this.IsPlayer() ? num5 : num7);
//            this.m_zanim.SetFloat(Character.sideway_speed, num6);
//            this.m_zanim.SetFloat(Character.turn_speed, this.m_currentTurnVel);
//            this.m_zanim.SetBool(Character.inWater, false);
//            this.m_zanim.SetBool(Character.onGround, this.IsOnGround());
//            this.m_zanim.SetBool(Character.encumbered, this.IsEncumbered());
//            this.m_zanim.SetBool(Character.flying, false);
//            if ((double)((Vector3)ref this.m_currentVel).get_magnitude() <= 0.100000001490116)
//                return;
//            if (this.m_running)
//            {
//                this.AddNoise(30f);
//            }
//            else
//            {
//                if (flag)
//                    return;
//                this.AddNoise(15f);
//            }
//        }

//        public bool IsSneaking()
//        {
//            return this.IsCrouching() && (double)((Vector3)ref this.m_currentVel).get_magnitude() > 0.100000001490116 && this.IsOnGround();
//        }

//        private float GetSlopeAngle()
//        {
//            return !this.IsOnGround() ? 0.0f : (float)-(90.0 - -(double)Vector3.SignedAngle(((Component)this).get_transform().get_forward(), this.m_lastGroundNormal, ((Component)this).get_transform().get_right()));
//        }

//        protected void AddPushbackForce(ref Vector3 velocity)
//        {
//            if (!Vector3.op_Inequality(this.m_pushForce, Vector3.get_zero()))
//                return;
//            Vector3 normalized = ((Vector3)ref this.m_pushForce).get_normalized();
//            float num = Vector3.Dot(normalized, velocity);
//            if ((double)num < 10.0)
//                velocity = Vector3.op_Addition(velocity, Vector3.op_Multiply(normalized, 10f - num));
//            if (!this.IsSwiming() && !this.m_flying)
//                return;
//            velocity = Vector3.op_Multiply(velocity, 0.5f);
//        }

//        private void ApplyPushback(HitData hit)
//        {
//            if ((double)hit.m_pushForce == 0.0)
//                return;
//            float num = Mathf.Min(40f, (float)((double)hit.m_pushForce / (double)this.m_body.mass * 5.0));
//            Vector3 vector3 = Vector3.op_Multiply(hit.m_dir, num);
//            vector3.y = (__Null)0.0;
//            if ((double)((Vector3)ref this.m_pushForce).get_magnitude() >= (double)((Vector3)ref vector3).get_magnitude())
//                return;
//            this.m_pushForce = vector3;
//        }

//        private void UpdatePushback(float dt)
//        {
//            this.m_pushForce = Vector3.MoveTowards(this.m_pushForce, Vector3.get_zero(), 100f * dt);
//        }

//        private void ApplyGroundForce(ref Vector3 vel, Vector3 targetVel)
//        {
//            Vector3 vector3_1 = Vector3.get_zero();
//            if (this.IsOnGround() && UnityEngine.Object.op_Implicit((UnityEngine.Object)this.m_lastGroundBody))
//            {
//                vector3_1 = this.m_lastGroundBody.GetPointVelocity(((Component)this).get_transform().get_position());
//                vector3_1.y = (__Null)0.0;
//            }
//            Ship standingOnShip = this.GetStandingOnShip();
//            if (UnityEngine.Object.op_Inequality((UnityEngine.Object)standingOnShip, (UnityEngine.Object)null))
//            {
//                if ((double)((Vector3)ref targetVel).get_magnitude() > 0.00999999977648258)
//                    this.m_lastAttachBody = (Rigidbody)null;
//                else if (UnityEngine.Object.op_Inequality((UnityEngine.Object)this.m_lastAttachBody, (UnityEngine.Object)this.m_lastGroundBody))
//                {
//                    this.m_lastAttachBody = this.m_lastGroundBody;
//                    this.m_lastAttachPos = this.m_lastAttachBody.get_transform().InverseTransformPoint(this.m_body.position);
//                }
//                if (UnityEngine.Object.op_Implicit((UnityEngine.Object)this.m_lastAttachBody))
//                {
//                    Vector3 vector3_2 = this.m_lastAttachBody.get_transform().TransformPoint(this.m_lastAttachPos);
//                    Vector3 vector3_3 = Vector3.op_Subtraction(vector3_2, this.m_body.position);
//                    if ((double)((Vector3)ref vector3_3).get_magnitude() < 4.0)
//                    {
//                        Vector3 vector3_4 = vector3_2;
//                        vector3_4.y = this.m_body.position.y;
//                        if (standingOnShip.IsOwner())
//                        {
//                            vector3_3.y = (__Null)0.0;
//                            vector3_1 = Vector3.op_Addition(vector3_1, Vector3.op_Multiply(vector3_3, 10f));
//                        }
//                        else
//                            this.m_body.position = vector3_4;
//                    }
//                    else
//                        this.m_lastAttachBody = (Rigidbody)null;
//                }
//            }
//            else
//                this.m_lastAttachBody = (Rigidbody)null;
//            vel = Vector3.op_Addition(vel, vector3_1);
//        }

//        private float UpdateRotation(float turnSpeed, float dt)
//        {
//            Quaternion q2 = this.AlwaysRotateCamera() ? this.m_lookYaw : Quaternion.LookRotation(this.m_moveDir);
//            float yawDeltaAngle = Utils.GetYawDeltaAngle(((Component)this).get_transform().get_rotation(), q2);
//            float num1 = 1f;
//            if (!this.IsPlayer())
//                num1 = Mathf.Pow(Mathf.Clamp01(Mathf.Abs(yawDeltaAngle) / 90f), 0.5f);
//            float num2 = turnSpeed * this.GetAttackSpeedFactorRotation() * num1;
//            Quaternion quaternion = Quaternion.RotateTowards(((Component)this).get_transform().get_rotation(), q2, num2 * dt);
//            if ((double)Mathf.Abs(yawDeltaAngle) > 1.0 / 1000.0)
//                ((Component)this).get_transform().set_rotation(quaternion);
//            return (float)((double)num2 * (double)Mathf.Sign(yawDeltaAngle) * (Math.PI / 180.0));
//        }

//        private void UpdateGroundTilt(float dt)
//        {
//            if (UnityEngine.Object.op_Equality((UnityEngine.Object)this.m_visual, (UnityEngine.Object)null))
//                return;
//            if (this.m_nview.IsOwner())
//            {
//                if (this.m_groundTilt != Character.GroundTiltType.None)
//                {
//                    if (!this.IsFlying() && this.IsOnGround())
//                    {
//                        Vector3 vector3_1 = this.m_lastGroundNormal;
//                        if (this.m_groundTilt == Character.GroundTiltType.PitchRaycast || this.m_groundTilt == Character.GroundTiltType.FullRaycast)
//                        {
//                            Vector3 p1 = Vector3.op_Addition(((Component)this).get_transform().get_position(), Vector3.op_Multiply(((Component)this).get_transform().get_forward(), this.m_collider.radius));
//                            Vector3 p2 = Vector3.op_Subtraction(((Component)this).get_transform().get_position(), Vector3.op_Multiply(((Component)this).get_transform().get_forward(), this.m_collider.radius));
//                            Vector3 normal1;
//                            ZoneSystem.instance.GetSolidHeight(p1, out float _, out normal1);
//                            Vector3 normal2;
//                            ZoneSystem.instance.GetSolidHeight(p2, out float _, out normal2);
//                            Vector3 vector3_2 = Vector3.op_Addition(Vector3.op_Addition(vector3_1, normal1), normal2);
//                            vector3_1 = ((Vector3)ref vector3_2).get_normalized();
//                        }
//                        this.m_groundTiltNormal = Vector3.Lerp(this.m_groundTiltNormal, Vector3.RotateTowards(Vector3.get_up(), ((Component)this).get_transform().InverseTransformVector(vector3_1), 0.8726646f, 1f), 0.05f);
//                        Vector3 vector3_3 = this.m_groundTilt == Character.GroundTiltType.Pitch || this.m_groundTilt == Character.GroundTiltType.PitchRaycast ? Vector3.op_Subtraction(this.m_groundTiltNormal, Vector3.Project(this.m_groundTiltNormal, Vector3.get_right())) : this.m_groundTiltNormal;
//                        this.m_visual.get_transform().set_localRotation(Quaternion.LookRotation(Vector3.Cross(vector3_3, Vector3.get_left()), vector3_3));
//                    }
//                    else
//                        this.m_visual.get_transform().set_localRotation(Quaternion.RotateTowards(this.m_visual.get_transform().get_localRotation(), Quaternion.get_identity(), dt * 200f));
//                    this.m_nview.GetZDO().Set("tiltrot", this.m_visual.get_transform().get_localRotation());
//                }
//                else
//                {
//                    if (!this.CanWallRun())
//                        return;
//                    if (this.m_wallRunning)
//                    {
//                        Vector3 vector3_1 = Vector3.Lerp(Vector3.get_up(), this.m_lastGroundNormal, 0.65f);
//                        Vector3 vector3_2 = Vector3.ProjectOnPlane(((Component)this).get_transform().get_forward(), vector3_1);
//                        ((Vector3)ref vector3_2).Normalize();
//                        this.m_visual.get_transform().set_rotation(Quaternion.RotateTowards(this.m_visual.get_transform().get_rotation(), Quaternion.LookRotation(vector3_2, vector3_1), 30f * dt));
//                    }
//                    else
//                        this.m_visual.get_transform().set_localRotation(Quaternion.RotateTowards(this.m_visual.get_transform().get_localRotation(), Quaternion.get_identity(), 100f * dt));
//                    this.m_nview.GetZDO().Set("tiltrot", this.m_visual.get_transform().get_localRotation());
//                }
//            }
//            else
//            {
//                if (this.m_groundTilt == Character.GroundTiltType.None && !this.CanWallRun())
//                    return;
//                this.m_visual.get_transform().set_localRotation(this.m_nview.GetZDO().GetQuaternion("tiltrot", Quaternion.get_identity()));
//            }
//        }

//        public bool IsWallRunning()
//        {
//            return this.m_wallRunning;
//        }

//        private bool IsOnSnow()
//        {
//            return false;
//        }

//        public void Heal(float hp, bool showText = true)
//        {
//            if ((double)hp <= 0.0)
//                return;
//            if (this.m_nview.IsOwner())
//                this.RPC_Heal(0L, hp, showText);
//            else
//                this.m_nview.InvokeRPC(nameof(Heal), (object)hp, (object)showText);
//        }

//        private void RPC_Heal(long sender, float hp, bool showText)
//        {
//            if (!this.m_nview.IsOwner())
//                return;
//            float health1 = this.GetHealth();
//            if ((double)health1 <= 0.0 || this.IsDead())
//                return;
//            float health2 = Mathf.Min(health1 + hp, this.GetMaxHealth());
//            if ((double)health2 <= (double)health1)
//                return;
//            this.SetHealth(health2);
//            if (!showText)
//                return;
//            DamageText.instance.ShowText(DamageText.TextType.Heal, this.GetTopPoint(), hp, this.IsPlayer());
//        }

//        public Vector3 GetTopPoint()
//        {
//            Bounds bounds1 = this.m_collider.bounds;
//            Vector3 center = ((Bounds)ref bounds1).get_center();
//            ref Vector3 local = ref center;
//            Bounds bounds2 = this.m_collider.bounds;
//            // ISSUE: variable of the null type
//            __Null y = ((Bounds)ref bounds2).get_max().y;
//            local.y = y;
//            return center;
//        }

//        public float GetRadius()
//        {
//            return this.m_collider.radius;
//        }

//        public Vector3 GetHeadPoint()
//        {
//            return this.m_head.get_position();
//        }

//        public Vector3 GetEyePoint()
//        {
//            return this.m_eye.get_position();
//        }

//        public Vector3 GetCenterPoint()
//        {
//            Bounds bounds = this.m_collider.bounds;
//            return ((Bounds)ref bounds).get_center();
//        }

//        public DestructibleType GetDestructibleType()
//        {
//            return DestructibleType.Character;
//        }

//        public void Damage(HitData hit)
//        {
//            if (!this.m_nview.IsValid())
//                return;
//            this.m_nview.InvokeRPC(nameof(Damage), (object)hit);
//        }

//        private void RPC_Damage(long sender, HitData hit)
//        {
//            if (this.IsDebugFlying() || !this.m_nview.IsOwner() || ((double)this.GetHealth() <= 0.0 || this.IsDead()) || (this.IsTeleporting() || this.InCutscene() || hit.m_dodgeable && this.IsDodgeInvincible()))
//                return;
//            Character attacker = hit.GetAttacker();
//            if (hit.HaveAttacker() && UnityEngine.Object.op_Equality((UnityEngine.Object)attacker, (UnityEngine.Object)null) || this.IsPlayer() && !this.IsPVPEnabled() && (UnityEngine.Object.op_Inequality((UnityEngine.Object)attacker, (UnityEngine.Object)null) && attacker.IsPlayer()))
//                return;
//            if (UnityEngine.Object.op_Inequality((UnityEngine.Object)attacker, (UnityEngine.Object)null) && !attacker.IsPlayer())
//            {
//                float difficultyDamageScale = Game.instance.GetDifficultyDamageScale(((Component)this).get_transform().get_position());
//                hit.ApplyModifier(difficultyDamageScale);
//            }
//            this.m_seman.OnDamaged(hit, attacker);
//            if (UnityEngine.Object.op_Inequality((UnityEngine.Object)this.m_baseAI, (UnityEngine.Object)null) && !this.m_baseAI.IsAlerted() && ((double)hit.m_backstabBonus > 1.0 && (double)Time.get_time() - (double)this.m_backstabTime > 300.0))
//            {
//                this.m_backstabTime = Time.get_time();
//                hit.ApplyModifier(hit.m_backstabBonus);
//                this.m_backstabHitEffects.Create(hit.m_point, Quaternion.get_identity(), ((Component)this).get_transform(), 1f);
//            }
//            if (this.IsStaggering() && !this.IsPlayer())
//            {
//                hit.ApplyModifier(2f);
//                this.m_critHitEffects.Create(hit.m_point, Quaternion.get_identity(), ((Component)this).get_transform(), 1f);
//            }
//            if (hit.m_blockable && this.IsBlocking())
//                this.BlockAttack(hit, attacker);
//            this.ApplyPushback(hit);
//            if (!string.IsNullOrEmpty(hit.m_statusEffect))
//            {
//                StatusEffect statusEffect = this.m_seman.GetStatusEffect(hit.m_statusEffect);
//                if (UnityEngine.Object.op_Equality((UnityEngine.Object)statusEffect, (UnityEngine.Object)null))
//                    statusEffect = this.m_seman.AddStatusEffect(hit.m_statusEffect, false);
//                if (UnityEngine.Object.op_Inequality((UnityEngine.Object)statusEffect, (UnityEngine.Object)null) && UnityEngine.Object.op_Inequality((UnityEngine.Object)attacker, (UnityEngine.Object)null))
//                    statusEffect.SetAttacker(attacker);
//            }
//            HitData.DamageModifiers damageModifiers = this.GetDamageModifiers();
//            HitData.DamageModifier significantModifier;
//            hit.ApplyResistance(damageModifiers, out significantModifier);
//            if (this.IsPlayer())
//            {
//                float bodyArmor = this.GetBodyArmor();
//                hit.ApplyArmor(bodyArmor);
//                this.DamageArmorDurability(hit);
//            }
//            float poison = hit.m_damage.m_poison;
//            float fire = hit.m_damage.m_fire;
//            float spirit = hit.m_damage.m_spirit;
//            hit.m_damage.m_poison = 0.0f;
//            hit.m_damage.m_fire = 0.0f;
//            hit.m_damage.m_spirit = 0.0f;
//            this.ApplyDamage(hit, true, true, significantModifier);
//            this.AddFireDamage(fire);
//            this.AddSpiritDamage(spirit);
//            this.AddPoisonDamage(poison);
//            this.AddFrostDamage(hit.m_damage.m_frost);
//            this.AddLightningDamage(hit.m_damage.m_lightning);
//        }

//        protected HitData.DamageModifier GetDamageModifier(HitData.DamageType damageType)
//        {
//            return this.GetDamageModifiers().GetModifier(damageType);
//        }

//        protected HitData.DamageModifiers GetDamageModifiers()
//        {
//            HitData.DamageModifiers mods = this.m_damageModifiers.Clone();
//            this.ApplyArmorDamageMods(ref mods);
//            this.m_seman.ApplyDamageMods(ref mods);
//            return mods;
//        }

//        public void ApplyDamage(
//          HitData hit,
//          bool showDamageText,
//          bool triggerEffects,
//          HitData.DamageModifier mod = HitData.DamageModifier.Normal)
//        {
//            if (this.IsDebugFlying() || this.IsDead() || (this.IsTeleporting() || this.InCutscene()))
//                return;
//            float totalDamage = hit.GetTotalDamage();
//            if (showDamageText && ((double)totalDamage > 0.0 || !this.IsPlayer()))
//                DamageText.instance.ShowText(mod, hit.m_point, totalDamage, this.IsPlayer());
//            if ((double)totalDamage <= 0.0)
//                return;
//            if (!this.InGodMode() && !this.InGhostMode())
//                this.SetHealth(this.GetHealth() - totalDamage);
//            this.AddStaggerDamage(hit.m_damage.GetTotalPhysicalDamage() * hit.m_staggerMultiplier, hit.m_dir);
//            if (triggerEffects && (double)totalDamage > 2.0)
//            {
//                this.DoDamageCameraShake(hit);
//                if ((double)hit.m_damage.GetTotalPhysicalDamage() > 0.0)
//                    this.m_hitEffects.Create(hit.m_point, Quaternion.get_identity(), ((Component)this).get_transform(), 1f);
//            }
//            this.OnDamaged(hit);
//            if (this.m_onDamaged != null)
//                this.m_onDamaged(totalDamage, hit.GetAttacker());
//            if (!Character.m_dpsDebugEnabled)
//                return;
//            Character.AddDPS(totalDamage, this);
//        }

//        protected virtual void DoDamageCameraShake(HitData hit)
//        {
//        }

//        protected virtual void DamageArmorDurability(HitData hit)
//        {
//        }

//        private void AddFireDamage(float damage)
//        {
//            if ((double)damage <= 0.0)
//                return;
//            SE_Burning seBurning = this.m_seman.GetStatusEffect("Burning") as SE_Burning;
//            if (UnityEngine.Object.op_Equality((UnityEngine.Object)seBurning, (UnityEngine.Object)null))
//                seBurning = this.m_seman.AddStatusEffect("Burning", false) as SE_Burning;
//            seBurning.AddFireDamage(damage);
//        }

//        private void AddSpiritDamage(float damage)
//        {
//            if ((double)damage <= 0.0)
//                return;
//            SE_Burning seBurning = this.m_seman.GetStatusEffect("Spirit") as SE_Burning;
//            if (UnityEngine.Object.op_Equality((UnityEngine.Object)seBurning, (UnityEngine.Object)null))
//                seBurning = this.m_seman.AddStatusEffect("Spirit", false) as SE_Burning;
//            seBurning.AddSpiritDamage(damage);
//        }

//        private void AddPoisonDamage(float damage)
//        {
//            if ((double)damage <= 0.0)
//                return;
//            SE_Poison sePoison = this.m_seman.GetStatusEffect("Poison") as SE_Poison;
//            if (UnityEngine.Object.op_Equality((UnityEngine.Object)sePoison, (UnityEngine.Object)null))
//                sePoison = this.m_seman.AddStatusEffect("Poison", false) as SE_Poison;
//            sePoison.AddDamage(damage);
//        }

//        private void AddFrostDamage(float damage)
//        {
//            if ((double)damage <= 0.0)
//                return;
//            SE_Frost seFrost = this.m_seman.GetStatusEffect("Frost") as SE_Frost;
//            if (UnityEngine.Object.op_Equality((UnityEngine.Object)seFrost, (UnityEngine.Object)null))
//                seFrost = this.m_seman.AddStatusEffect("Frost", false) as SE_Frost;
//            seFrost.AddDamage(damage);
//        }

//        private void AddLightningDamage(float damage)
//        {
//            if ((double)damage <= 0.0)
//                return;
//            this.m_seman.AddStatusEffect("Lightning", true);
//        }

//        private void AddStaggerDamage(float damage, Vector3 forceDirection)
//        {
//            if ((double)this.m_staggerDamageFactor <= 0.0 && !this.IsPlayer())
//                return;
//            this.m_staggerDamage += damage;
//            this.m_staggerTimer = 0.0f;
//            float maxHealth = this.GetMaxHealth();
//            if ((double)this.m_staggerDamage < (this.IsPlayer() ? (double)maxHealth / 2.0 : (double)maxHealth * (double)this.m_staggerDamageFactor))
//                return;
//            this.m_staggerDamage = 0.0f;
//            this.Stagger(forceDirection);
//        }

//        private static void AddDPS(float damage, Character me)
//        {
//            if (UnityEngine.Object.op_Equality((UnityEngine.Object)me, (UnityEngine.Object)Player.m_localPlayer))
//                Character.CalculateDPS("To-you ", Character.m_playerDamage, damage);
//            else
//                Character.CalculateDPS("To-others ", Character.m_enemyDamage, damage);
//        }

//        private static void CalculateDPS(
//          string name,
//          List<KeyValuePair<float, float>> damages,
//          float damage)
//        {
//            float time = Time.get_time();
//            if (damages.Count > 0 && (double)Time.get_time() - (double)damages[damages.Count - 1].Key > 5.0)
//                damages.Clear();
//            damages.Add(new KeyValuePair<float, float>(time, damage));
//            float num1 = Time.get_time() - damages[0].Key;
//            if ((double)num1 < 0.00999999977648258)
//                return;
//            float num2 = 0.0f;
//            foreach (KeyValuePair<float, float> damage1 in damages)
//                num2 += damage1.Value;
//            float num3 = num2 / num1;
//            string text = "DPS " + name + " (" + (object)damages.Count + " attacks): " + num3.ToString("0.0");
//            ZLog.Log((object)text);
//            MessageHud.instance.ShowMessage(MessageHud.MessageType.Center, text, 0, (Sprite)null);
//        }

//        private void UpdateStagger(float dt)
//        {
//            if ((double)this.m_staggerDamageFactor <= 0.0 && !this.IsPlayer())
//                return;
//            this.m_staggerTimer += dt;
//            if ((double)this.m_staggerTimer <= 3.0)
//                return;
//            this.m_staggerDamage = 0.0f;
//        }

//        public void Stagger(Vector3 forceDirection)
//        {
//            if (this.m_nview.IsOwner())
//                this.RPC_Stagger(0L, forceDirection);
//            else
//                this.m_nview.InvokeRPC(nameof(Stagger), (object)forceDirection);
//        }

//        private void RPC_Stagger(long sender, Vector3 forceDirection)
//        {
//            if (this.IsStaggering())
//                return;
//            if ((double)((Vector3)ref forceDirection).get_magnitude() > 0.00999999977648258)
//            {
//                forceDirection.y = (__Null)0.0;
//                ((Component)this).get_transform().set_rotation(Quaternion.LookRotation(Vector3.op_UnaryNegation(forceDirection)));
//            }
//            this.m_zanim.SetTrigger("stagger");
//        }

//        protected virtual void ApplyArmorDamageMods(ref HitData.DamageModifiers mods)
//        {
//        }

//        public virtual float GetBodyArmor()
//        {
//            return 0.0f;
//        }

//        protected virtual bool BlockAttack(HitData hit, Character attacker)
//        {
//            return false;
//        }

//        protected virtual void OnDamaged(HitData hit)
//        {
//        }

//        private void OnCollisionStay(Collision collision)
//        {
//            if (!this.m_nview.IsValid() || !this.m_nview.IsOwner() || (double)this.m_jumpTimer < 0.100000001490116)
//                return;
//            foreach (ContactPoint contact in collision.contacts)
//            {
//                float num = (float)(contact.point.y - ((Component)this).get_transform().get_position().y);
//                if (contact.normal.y > 0.100000001490116 && (double)num < (double)this.m_collider.radius)
//                {
//                    if (contact.normal.y > this.m_groundContactNormal.y || !this.m_groundContact)
//                    {
//                        this.m_groundContact = true;
//                        this.m_groundContactNormal = contact.normal;
//                        this.m_groundContactPoint = contact.point;
//                        this.m_lowestContactCollider = collision.collider;
//                    }
//                    else
//                    {
//                        Vector3 vector3 = Vector3.Normalize(Vector3.op_Addition(this.m_groundContactNormal, contact.normal));
//                        if (vector3.y > this.m_groundContactNormal.y)
//                        {
//                            this.m_groundContactNormal = vector3;
//                            this.m_groundContactPoint = Vector3.op_Multiply(Vector3.op_Addition(this.m_groundContactPoint, contact.point), 0.5f);
//                        }
//                    }
//                }
//            }
//        }

//        private void UpdateGroundContact(float dt)
//        {
//            if (!this.m_groundContact)
//                return;
//            this.m_lastGroundCollider = this.m_lowestContactCollider;
//            this.m_lastGroundNormal = this.m_groundContactNormal;
//            this.m_lastGroundPoint = this.m_groundContactPoint;
//            this.m_lastGroundBody = UnityEngine.Object.op_Implicit((UnityEngine.Object)this.m_lastGroundCollider) ? this.m_lastGroundCollider.attachedRigidbody : (Rigidbody)null;
//            if (!this.IsPlayer() && UnityEngine.Object.op_Inequality((UnityEngine.Object)this.m_lastGroundBody, (UnityEngine.Object)null) && this.m_lastGroundBody.get_gameObject().get_layer() == ((Component)this).get_gameObject().get_layer())
//            {
//                this.m_lastGroundCollider = (Collider)null;
//                this.m_lastGroundBody = (Rigidbody)null;
//            }
//            float num = Mathf.Max(0.0f, this.m_maxAirAltitude - (float)((Component)this).get_transform().get_position().y);
//            if ((double)num > 0.800000011920929)
//            {
//                if (this.m_onLand != null)
//                {
//                    Vector3 lastGroundPoint = this.m_lastGroundPoint;
//                    if (this.InWater())
//                        lastGroundPoint.y = (__Null)(double)this.m_waterLevel;
//                    this.m_onLand(this.m_lastGroundPoint);
//                }
//                this.ResetCloth();
//            }
//            if (this.IsPlayer() && (double)num > 4.0)
//                this.Damage(new HitData()
//                {
//                    m_damage = {
//          m_damage = Mathf.Clamp01((float) (((double) num - 4.0) / 16.0)) * 100f
//        },
//                    m_point = this.m_lastGroundPoint,
//                    m_dir = this.m_lastGroundNormal
//                });
//            this.ResetGroundContact();
//            this.m_lastGroundTouch = 0.0f;
//            this.m_maxAirAltitude = (float)((Component)this).get_transform().get_position().y;
//        }

//        private void ResetGroundContact()
//        {
//            this.m_lowestContactCollider = (Collider)null;
//            this.m_groundContact = false;
//            this.m_groundContactNormal = Vector3.get_zero();
//            this.m_groundContactPoint = Vector3.get_zero();
//        }

//        public Ship GetStandingOnShip()
//        {
//            if (!this.IsOnGround())
//                return (Ship)null;
//            return UnityEngine.Object.op_Implicit((UnityEngine.Object)this.m_lastGroundBody) ? (Ship)this.m_lastGroundBody.GetComponent<Ship>() : (Ship)null;
//        }

//        public bool IsOnGround()
//        {
//            return (double)this.m_lastGroundTouch < 0.200000002980232 || this.m_body.IsSleeping();
//        }

//        private void CheckDeath()
//        {
//            if (this.IsDead() || (double)this.GetHealth() > 0.0)
//                return;
//            this.OnDeath();
//            if (this.m_onDeath == null)
//                return;
//            this.m_onDeath();
//        }

//        protected virtual void OnRagdollCreated(Ragdoll ragdoll)
//        {
//        }

//        protected virtual void OnDeath()
//        {
//            foreach (GameObject gameObject in this.m_deathEffects.Create(((Component)this).get_transform().get_position(), ((Component)this).get_transform().get_rotation(), ((Component)this).get_transform(), 1f))
//            {
//                Ragdoll component1 = (Ragdoll)gameObject.GetComponent<Ragdoll>();
//                if (UnityEngine.Object.op_Implicit((UnityEngine.Object)component1))
//                {
//                    CharacterDrop component2 = (CharacterDrop)((Component)this).GetComponent<CharacterDrop>();
//                    LevelEffects componentInChildren = (LevelEffects)((Component)this).GetComponentInChildren<LevelEffects>();
//                    Vector3 velocity = this.m_body.velocity;
//                    if ((double)((Vector3)ref this.m_pushForce).get_magnitude() * 0.5 > (double)((Vector3)ref velocity).get_magnitude())
//                        velocity = Vector3.op_Multiply(this.m_pushForce, 0.5f);
//                    float hue = 0.0f;
//                    float saturation = 0.0f;
//                    float num = 0.0f;
//                    if (UnityEngine.Object.op_Implicit((UnityEngine.Object)componentInChildren))
//                        componentInChildren.GetColorChanges(out hue, out saturation, out num);
//                    component1.Setup(velocity, hue, saturation, num, component2);
//                    this.OnRagdollCreated(component1);
//                    if (UnityEngine.Object.op_Implicit((UnityEngine.Object)component2))
//                        component2.SetDropsEnabled(false);
//                }
//            }
//            if (!string.IsNullOrEmpty(this.m_defeatSetGlobalKey))
//                ZoneSystem.instance.SetGlobalKey(this.m_defeatSetGlobalKey);
//            ZNetScene.instance.Destroy(((Component)this).get_gameObject());
//            Gogan.LogEvent("Game", "Killed", this.m_name, 0L);
//        }

//        public float GetHealth()
//        {
//            ZDO zdo = this.m_nview.GetZDO();
//            return zdo == null ? this.GetMaxHealth() : zdo.GetFloat("health", this.GetMaxHealth());
//        }

//        public void SetHealth(float health)
//        {
//            ZDO zdo = this.m_nview.GetZDO();
//            if (zdo == null || !this.m_nview.IsOwner())
//                return;
//            if ((double)health < 0.0)
//                health = 0.0f;
//            zdo.Set(nameof(health), health);
//        }

//        public float GetHealthPercentage()
//        {
//            return this.GetHealth() / this.GetMaxHealth();
//        }

//        public virtual bool IsDead()
//        {
//            return false;
//        }

//        public void SetMaxHealth(float health)
//        {
//            if (this.m_nview.GetZDO() != null)
//                this.m_nview.GetZDO().Set("max_health", health);
//            if ((double)this.GetHealth() <= (double)health)
//                return;
//            this.SetHealth(health);
//        }

//        public float GetMaxHealth()
//        {
//            return this.m_nview.GetZDO() != null ? this.m_nview.GetZDO().GetFloat("max_health", this.m_health) : this.m_health;
//        }

//        public virtual float GetMaxStamina()
//        {
//            return 0.0f;
//        }

//        public virtual float GetStaminaPercentage()
//        {
//            return 1f;
//        }

//        public bool IsBoss()
//        {
//            return this.m_boss;
//        }

//        public void SetLookDir(Vector3 dir)
//        {
//            if ((double)((Vector3)ref dir).get_magnitude() <= Mathf.Epsilon)
//                dir = ((Component)this).get_transform().get_forward();
//            else
//                ((Vector3)ref dir).Normalize();
//            this.m_lookDir = dir;
//            dir.y = (__Null)0.0;
//            this.m_lookYaw = Quaternion.LookRotation(dir);
//        }

//        public Vector3 GetLookDir()
//        {
//            return this.m_eye.get_forward();
//        }

//        public virtual void OnAttackTrigger()
//        {
//        }

//        public virtual void OnStopMoving()
//        {
//        }

//        public virtual void OnWeaponTrailStart()
//        {
//        }

//        public void SetMoveDir(Vector3 dir)
//        {
//            this.m_moveDir = dir;
//        }

//        public void SetRun(bool run)
//        {
//            this.m_run = run;
//        }

//        public void SetWalk(bool walk)
//        {
//            this.m_walk = walk;
//        }

//        public bool GetWalk()
//        {
//            return this.m_walk;
//        }

//        protected virtual void UpdateEyeRotation()
//        {
//            this.m_eye.set_rotation(Quaternion.LookRotation(this.m_lookDir));
//        }

//        public void OnAutoJump(Vector3 dir, float upVel, float forwardVel)
//        {
//            if (!this.m_nview.IsValid() || !this.m_nview.IsOwner() || (!this.IsOnGround() || this.IsDead()) || (this.InAttack() || this.InDodge() || (this.IsKnockedBack() || (double)Time.get_time() - (double)this.m_lastAutoJumpTime < 0.5)))
//                return;
//            this.m_lastAutoJumpTime = Time.get_time();
//            if ((double)Vector3.Dot(this.m_moveDir, dir) < 0.5)
//                return;
//            Vector3 zero = Vector3.get_zero();
//            zero.y = (__Null)(double)upVel;
//            this.m_body.velocity = Vector3.op_Addition(zero, Vector3.op_Multiply(dir, forwardVel));
//            this.m_lastGroundTouch = 1f;
//            this.m_jumpTimer = 0.0f;
//            this.m_jumpEffects.Create(((Component)this).get_transform().get_position(), ((Component)this).get_transform().get_rotation(), ((Component)this).get_transform(), 1f);
//            this.SetCrouch(false);
//            this.UpdateBodyFriction();
//        }

//        public void Jump()
//        {
//            if (!this.IsOnGround() || this.IsDead() || (this.InAttack() || this.IsEncumbered()) || (this.InDodge() || this.IsKnockedBack()))
//                return;
//            bool flag = false;
//            if (!this.HaveStamina(this.m_jumpStaminaUsage))
//            {
//                if (this.IsPlayer())
//                    Hud.instance.StaminaBarNoStaminaFlash();
//                flag = true;
//            }
//            float num1 = 0.0f;
//            Skills skills = this.GetSkills();
//            if (UnityEngine.Object.op_Inequality((UnityEngine.Object)skills, (UnityEngine.Object)null))
//            {
//                num1 = skills.GetSkillFactor(Skills.SkillType.Jump);
//                if (!flag)
//                    this.RaiseSkill(Skills.SkillType.Jump, 1f);
//            }
//            Vector3 vector3_1 = this.m_body.velocity;
//            double num2 = (double)Mathf.Acos(Mathf.Clamp01((float)this.m_lastGroundNormal.y));
//            Vector3 vector3_2 = Vector3.op_Addition(this.m_lastGroundNormal, Vector3.get_up());
//            Vector3 normalized = ((Vector3)ref vector3_2).get_normalized();
//            float num3 = (float)(1.0 + (double)num1 * 0.400000005960464);
//            float num4 = this.m_jumpForce * num3;
//            float num5 = Vector3.Dot(normalized, vector3_1);
//            if ((double)num5 < (double)num4)
//                vector3_1 = Vector3.op_Addition(vector3_1, Vector3.op_Multiply(normalized, num4 - num5));
//            Vector3 vector3_3 = Vector3.op_Addition(vector3_1, Vector3.op_Multiply(Vector3.op_Multiply(this.m_moveDir, this.m_jumpForceForward), num3));
//            if (flag)
//                vector3_3 = Vector3.op_Multiply(vector3_3, this.m_jumpForceTiredFactor);
//            this.m_body.WakeUp();
//            this.m_body.velocity = vector3_3;
//            this.ResetGroundContact();
//            this.m_lastGroundTouch = 1f;
//            this.m_jumpTimer = 0.0f;
//            this.m_zanim.SetTrigger("jump");
//            this.AddNoise(30f);
//            this.m_jumpEffects.Create(((Component)this).get_transform().get_position(), ((Component)this).get_transform().get_rotation(), ((Component)this).get_transform(), 1f);
//            this.OnJump();
//            this.SetCrouch(false);
//            this.UpdateBodyFriction();
//        }

//        private void UpdateBodyFriction()
//        {
//            this.m_collider.material.frictionCombine = PhysicMaterialCombine.Multiply;
//            if (this.IsDead())
//            {
//                this.m_collider.material.staticFriction = 1f;
//                this.m_collider.material.dynamicFriction = 1f;
//                this.m_collider.material.frictionCombine = PhysicMaterialCombine.Maximum;
//            }
//            else if (this.IsSwiming())
//            {
//                this.m_collider.material.staticFriction = 0.2f;
//                this.m_collider.material.dynamicFriction = 0.2f;
//            }
//            else if (!this.IsOnGround())
//            {
//                this.m_collider.material.staticFriction = 0.0f;
//                this.m_collider.material.dynamicFriction = 0.0f;
//            }
//            else if (this.IsFlying())
//            {
//                this.m_collider.material.staticFriction = 0.0f;
//                this.m_collider.material.dynamicFriction = 0.0f;
//            }
//            else if ((double)((Vector3)ref this.m_moveDir).get_magnitude() < 0.100000001490116)
//            {
//                this.m_collider.material.staticFriction = (float)(0.800000011920929 * (1.0 - (double)this.m_slippage));
//                this.m_collider.material.dynamicFriction = (float)(0.800000011920929 * (1.0 - (double)this.m_slippage));
//                this.m_collider.material.frictionCombine = PhysicMaterialCombine.Maximum;
//            }
//            else
//            {
//                this.m_collider.material.staticFriction = (float)(0.400000005960464 * (1.0 - (double)this.m_slippage));
//                this.m_collider.material.dynamicFriction = (float)(0.400000005960464 * (1.0 - (double)this.m_slippage));
//            }
//        }

//        public virtual bool StartAttack(Character target, bool charge)
//        {
//            return false;
//        }

//        public virtual void OnNearFire(Vector3 point)
//        {
//        }

//        public ZDOID GetZDOID()
//        {
//            return this.m_nview.IsValid() ? this.m_nview.GetZDO().m_uid : ZDOID.None;
//        }

//        public bool IsOwner()
//        {
//            return this.m_nview.IsValid() && this.m_nview.IsOwner();
//        }

//        public long GetOwner()
//        {
//            return this.m_nview.IsValid() ? this.m_nview.GetZDO().m_owner : 0L;
//        }

//        public virtual bool UseMeleeCamera()
//        {
//            return false;
//        }

//        public virtual bool AlwaysRotateCamera()
//        {
//            return true;
//        }

//        public void SetInWater(float depth)
//        {
//            this.m_waterLevel = depth;
//        }

//        public virtual bool IsPVPEnabled()
//        {
//            return false;
//        }

//        public virtual bool InIntro()
//        {
//            return false;
//        }

//        public virtual bool InCutscene()
//        {
//            return false;
//        }

//        public virtual bool IsCrouching()
//        {
//            return false;
//        }

//        public virtual bool InBed()
//        {
//            return false;
//        }

//        public virtual bool IsAttached()
//        {
//            return false;
//        }

//        protected virtual void SetCrouch(bool crouch)
//        {
//        }

//        public virtual void AttachStart(
//          Transform attachPoint,
//          bool hideWeapons,
//          bool isBed,
//          string attachAnimation,
//          Vector3 detachOffset)
//        {
//        }

//        public virtual void AttachStop()
//        {
//        }

//        private void UpdateWater(float dt)
//        {
//            this.m_swimTimer += dt;
//            if (!this.InWaterSwimDepth())
//                return;
//            if (this.m_nview.IsOwner())
//                this.m_seman.AddStatusEffect("Wet", true);
//            if (!this.m_canSwim)
//                return;
//            this.m_swimTimer = 0.0f;
//        }

//        public bool IsSwiming()
//        {
//            return (double)this.m_swimTimer < 0.5;
//        }

//        public bool InWaterSwimDepth()
//        {
//            return (double)this.InWaterDepth() > (double)Mathf.Max(0.0f, this.m_swimDepth - 0.4f);
//        }

//        private float InWaterDepth()
//        {
//            return UnityEngine.Object.op_Inequality((UnityEngine.Object)this.GetStandingOnShip(), (UnityEngine.Object)null) ? 0.0f : Mathf.Max(0.0f, this.m_waterLevel - (float)((Component)this).get_transform().get_position().y);
//        }

//        public bool InWater()
//        {
//            return (double)this.InWaterDepth() > 0.0;
//        }

//        protected virtual bool CheckRun(Vector3 moveDir, float dt)
//        {
//            return this.m_run && (double)((Vector3)ref moveDir).get_magnitude() >= 0.100000001490116 && (!this.IsCrouching() && !this.IsEncumbered()) && !this.InDodge();
//        }

//        public bool IsRunning()
//        {
//            return this.m_running;
//        }

//        public virtual bool InPlaceMode()
//        {
//            return false;
//        }

//        public virtual bool HaveStamina(float amount = 0.0f)
//        {
//            return true;
//        }

//        public virtual void AddStamina(float v)
//        {
//        }

//        public virtual void UseStamina(float stamina)
//        {
//        }

//        public bool IsStaggering()
//        {
//            AnimatorStateInfo animatorStateInfo = this.m_animator.GetCurrentAnimatorStateInfo(0);
//            return ((AnimatorStateInfo)ref animatorStateInfo).get_tagHash() == Character.m_animatorTagStagger;
//        }

//        public virtual bool CanMove()
//        {
//            AnimatorStateInfo animatorStateInfo = this.m_animator.IsInTransition(0) ? this.m_animator.GetNextAnimatorStateInfo(0) : this.m_animator.GetCurrentAnimatorStateInfo(0);
//            return ((AnimatorStateInfo)ref animatorStateInfo).get_tagHash() != Character.m_animatorTagFreeze && ((AnimatorStateInfo)ref animatorStateInfo).get_tagHash() != Character.m_animatorTagStagger && ((AnimatorStateInfo)ref animatorStateInfo).get_tagHash() != Character.m_animatorTagSitting;
//        }

//        public virtual bool IsEncumbered()
//        {
//            return false;
//        }

//        public virtual bool IsTeleporting()
//        {
//            return false;
//        }

//        private bool CanWallRun()
//        {
//            return this.IsPlayer();
//        }

//        public void ShowPickupMessage(ItemData item, int amount)
//        {
//            this.Message(MessageHud.MessageType.TopLeft, "$msg_added " + item.m_shared.m_name, amount, item.GetIcon());
//        }

//        public void ShowRemovedMessage(ItemData item, int amount)
//        {
//            this.Message(MessageHud.MessageType.TopLeft, "$msg_removed " + item.m_shared.m_name, amount, item.GetIcon());
//        }

//        public virtual void Message(MessageHud.MessageType type, string msg, int amount = 0, Sprite icon = null)
//        {
//        }

//        public CapsuleCollider GetCollider()
//        {
//            return this.m_collider;
//        }

//        public virtual void OnStealthSuccess(Character character, float factor)
//        {
//        }

//        public virtual float GetStealthFactor()
//        {
//            return 1f;
//        }

//        private void UpdateNoise(float dt)
//        {
//            this.m_noiseRange = Mathf.Max(0.0f, this.m_noiseRange - dt * 4f);
//            this.m_syncNoiseTimer += dt;
//            if ((double)this.m_syncNoiseTimer <= 0.5)
//                return;
//            this.m_syncNoiseTimer = 0.0f;
//            this.m_nview.GetZDO().Set("noise", this.m_noiseRange);
//        }

//        public void AddNoise(float range)
//        {
//            if (!this.m_nview.IsValid())
//                return;
//            if (this.m_nview.IsOwner())
//                this.RPC_AddNoise(0L, range);
//            else
//                this.m_nview.InvokeRPC(nameof(AddNoise), (object)range);
//        }

//        private void RPC_AddNoise(long sender, float range)
//        {
//            if (!this.m_nview.IsOwner() || (double)range <= (double)this.m_noiseRange)
//                return;
//            this.m_noiseRange = range;
//            this.m_seman.ModifyNoise(this.m_noiseRange, ref this.m_noiseRange);
//        }

//        public float GetNoiseRange()
//        {
//            if (!this.m_nview.IsValid())
//                return 0.0f;
//            return this.m_nview.IsOwner() ? this.m_noiseRange : this.m_nview.GetZDO().GetFloat("noise", 0.0f);
//        }

//        public virtual bool InGodMode()
//        {
//            return false;
//        }

//        public virtual bool InGhostMode()
//        {
//            return false;
//        }

//        public virtual bool IsDebugFlying()
//        {
//            return false;
//        }

//        public virtual string GetHoverText()
//        {
//            Tameable component = (Tameable)((Component)this).GetComponent<Tameable>();
//            return UnityEngine.Object.op_Implicit((UnityEngine.Object)component) ? component.GetHoverText() : "";
//        }

//        public virtual string GetHoverName()
//        {
//            return Localization.instance.Localize(this.m_name);
//        }

//        public virtual bool IsHoldingAttack()
//        {
//            return false;
//        }

//        public virtual bool InAttack()
//        {
//            return false;
//        }

//        protected virtual void StopEmote()
//        {
//        }

//        public virtual bool InMinorAction()
//        {
//            return false;
//        }

//        public virtual bool InDodge()
//        {
//            return false;
//        }

//        public virtual bool IsDodgeInvincible()
//        {
//            return false;
//        }

//        public virtual bool InEmote()
//        {
//            return false;
//        }

//        public virtual bool IsBlocking()
//        {
//            return false;
//        }

//        public bool IsFlying()
//        {
//            return this.m_flying;
//        }

//        public bool IsKnockedBack()
//        {
//            return Vector3.op_Inequality(this.m_pushForce, Vector3.get_zero());
//        }

//        private void OnDrawGizmosSelected()
//        {
//            if (UnityEngine.Object.op_Inequality((UnityEngine.Object)this.m_nview, (UnityEngine.Object)null) && this.m_nview.GetZDO() != null)
//                Gizmos.DrawWireSphere(((Component)this).get_transform().get_position(), this.m_nview.GetZDO().GetFloat("noise", 0.0f));
//            Gizmos.set_color(Color.get_blue());
//            Gizmos.DrawWireCube(Vector3.op_Addition(((Component)this).get_transform().get_position(), Vector3.op_Multiply(Vector3.get_up(), this.m_swimDepth)), new Vector3(1f, 0.05f, 1f));
//            if (!this.IsOnGround())
//                return;
//            Gizmos.set_color(Color.get_green());
//            Gizmos.DrawLine(this.m_lastGroundPoint, Vector3.op_Addition(this.m_lastGroundPoint, this.m_lastGroundNormal));
//        }

//        public virtual bool TeleportTo(Vector3 pos, Quaternion rot, bool distantTeleport)
//        {
//            return false;
//        }

//        private void SyncVelocity()
//        {
//            this.m_nview.GetZDO().Set("BodyVelocity", this.m_body.velocity);
//        }

//        public Vector3 GetVelocity()
//        {
//            if (!this.m_nview.IsValid())
//                return Vector3.get_zero();
//            return this.m_nview.IsOwner() ? this.m_body.velocity : this.m_nview.GetZDO().GetVec3("BodyVelocity", Vector3.get_zero());
//        }

//        public void AddRootMotion(Vector3 vel)
//        {
//            if (!this.InDodge() && !this.InAttack() && !this.InEmote())
//                return;
//            this.m_rootMotion = Vector3.op_Addition(this.m_rootMotion, vel);
//        }

//        private void ApplyRootMotion(ref Vector3 vel)
//        {
//            Vector3 vector3 = Vector3.op_Multiply(this.m_rootMotion, 55f);
//            if ((double)((Vector3)ref vector3).get_magnitude() > (double)((Vector3)ref vel).get_magnitude())
//                vel = vector3;
//            this.m_rootMotion = Vector3.get_zero();
//        }

//        public static void GetCharactersInRange(Vector3 point, float radius, List<Character> characters)
//        {
//            foreach (Character character in Character.m_characters)
//            {
//                if ((double)Vector3.Distance(((Component)character).get_transform().get_position(), point) < (double)radius)
//                    characters.Add(character);
//            }
//        }

//        public static List<Character> GetAllCharacters()
//        {
//            return Character.m_characters;
//        }

//        public static bool IsCharacterInRange(Vector3 point, float range)
//        {
//            foreach (Component character in Character.m_characters)
//            {
//                if ((double)Vector3.Distance(character.get_transform().get_position(), point) < (double)range)
//                    return true;
//            }
//            return false;
//        }

//        public virtual void OnTargeted(bool sensed, bool alerted)
//        {
//        }

//        public GameObject GetVisual()
//        {
//            return this.m_visual;
//        }

//        protected void UpdateLodgroup()
//        {
//            if (UnityEngine.Object.op_Equality((UnityEngine.Object)this.m_lodGroup, (UnityEngine.Object)null))
//                return;
//            Renderer[] componentsInChildren = (Renderer[])this.m_visual.GetComponentsInChildren<Renderer>();
//            LOD[] loDs = this.m_lodGroup.GetLODs();
//            loDs[0].renderers = (__Null)componentsInChildren;
//            this.m_lodGroup.SetLODs(loDs);
//        }

//        public virtual float GetEquipmentMovementModifier()
//        {
//            return 0.0f;
//        }

//        protected virtual float GetJogSpeedFactor()
//        {
//            return 1f;
//        }

//        protected virtual float GetRunSpeedFactor()
//        {
//            return 1f;
//        }

//        protected virtual float GetAttackSpeedFactorMovement()
//        {
//            return 1f;
//        }

//        protected virtual float GetAttackSpeedFactorRotation()
//        {
//            return 1f;
//        }

//        public virtual void RaiseSkill(Skills.SkillType skill, float value = 1f)
//        {
//        }

//        public virtual Skills GetSkills()
//        {
//            return (Skills)null;
//        }

//        public virtual float GetSkillFactor(Skills.SkillType skill)
//        {
//            return 0.0f;
//        }

//        public virtual float GetRandomSkillFactor(Skills.SkillType skill)
//        {
//            return UnityEngine.Random.Range(0.75f, 1f);
//        }

//        public bool IsMonsterFaction()
//        {
//            if (this.IsTamed())
//                return false;
//            return this.m_faction == Character.Faction.ForestMonsters || this.m_faction == Character.Faction.Undead || (this.m_faction == Character.Faction.Demon || this.m_faction == Character.Faction.PlainsMonsters) || this.m_faction == Character.Faction.MountainMonsters || this.m_faction == Character.Faction.SeaMonsters;
//        }

//        public Transform GetTransform()
//        {
//            return UnityEngine.Object.op_Equality((UnityEngine.Object)this, (UnityEngine.Object)null) ? (Transform)null : ((Component)this).get_transform();
//        }

//        public Collider GetLastGroundCollider()
//        {
//            return this.m_lastGroundCollider;
//        }

//        public Vector3 GetLastGroundNormal()
//        {
//            return this.m_groundContactNormal;
//        }

//        public void ResetCloth()
//        {
//            this.m_nview.InvokeRPC(ZNetView.Everybody, nameof(ResetCloth), (object[])Array.Empty<object>());
//        }

//        private void RPC_ResetCloth(long sender)
//        {
//            foreach (Cloth componentsInChild in (Cloth[])((Component)this).GetComponentsInChildren<Cloth>())
//            {
//                if (componentsInChild.get_enabled())
//                {
//                    componentsInChild.set_enabled(false);
//                    componentsInChild.set_enabled(true);
//                }
//            }
//        }

//        public virtual bool GetRelativePosition(
//          out ZDOID parent,
//          out Vector3 relativePos,
//          out Vector3 relativeVel)
//        {
//            relativeVel = Vector3.get_zero();
//            if (this.IsOnGround() && UnityEngine.Object.op_Implicit((UnityEngine.Object)this.m_lastGroundBody))
//            {
//                ZNetView component = (ZNetView)this.m_lastGroundBody.GetComponent<ZNetView>();
//                if (UnityEngine.Object.op_Implicit((UnityEngine.Object)component) && component.IsValid())
//                {
//                    parent = component.GetZDO().m_uid;
//                    relativePos = ((Component)component).get_transform().InverseTransformPoint(((Component)this).get_transform().get_position());
//                    relativeVel = ((Component)component).get_transform().InverseTransformVector(Vector3.op_Subtraction(this.m_body.velocity, this.m_lastGroundBody.velocity));
//                    return true;
//                }
//            }
//            parent = ZDOID.None;
//            relativePos = Vector3.get_zero();
//            return false;
//        }

//        public Quaternion GetLookYaw()
//        {
//            return this.m_lookYaw;
//        }

//        public Vector3 GetMoveDir()
//        {
//            return this.m_moveDir;
//        }

//        public BaseAI GetBaseAI()
//        {
//            return this.m_baseAI;
//        }

//        public float GetMass()
//        {
//            return this.m_body.mass;
//        }

//        protected void SetVisible(bool visible)
//        {
//            if (UnityEngine.Object.op_Equality((UnityEngine.Object)this.m_lodGroup, (UnityEngine.Object)null) || this.m_lodVisible == visible)
//                return;
//            this.m_lodVisible = visible;
//            if (this.m_lodVisible)
//                this.m_lodGroup.set_localReferencePoint(this.m_originalLocalRef);
//            else
//                this.m_lodGroup.set_localReferencePoint(new Vector3(999999f, 999999f, 999999f));
//        }

//        public void SetTamed(bool tamed)
//        {
//            if (!this.m_nview.IsValid() || this.m_tamed == tamed)
//                return;
//            this.m_nview.InvokeRPC(nameof(SetTamed), (object)tamed);
//        }

//        private void RPC_SetTamed(long sender, bool tamed)
//        {
//            if (!this.m_nview.IsOwner() || this.m_tamed == tamed)
//                return;
//            this.m_tamed = tamed;
//            this.m_nview.GetZDO().Set(nameof(tamed), this.m_tamed);
//        }

//        public bool IsTamed()
//        {
//            if (!this.m_nview.IsValid())
//                return false;
//            if (!this.m_nview.IsOwner() && (double)Time.get_time() - (double)this.m_lastTamedCheck > 1.0)
//            {
//                this.m_lastTamedCheck = Time.get_time();
//                this.m_tamed = this.m_nview.GetZDO().GetBool("tamed", this.m_tamed);
//            }
//            return this.m_tamed;
//        }

//        public SEMan GetSEMan()
//        {
//            return this.m_seman;
//        }

//        public bool InInterior()
//        {
//            return ((Component)this).get_transform().get_position().y > 3000.0;
//        }

//        public static void SetDPSDebug(bool enabled)
//        {
//            Character.m_dpsDebugEnabled = enabled;
//        }

//        public static bool IsDPSDebugEnabled()
//        {
//            return Character.m_dpsDebugEnabled;
//        }

//        public Character()
//        {
//            base.\u002Ector();
//        }

//        public enum Faction
//        {
//            Players,
//            AnimalsVeg,
//            ForestMonsters,
//            Undead,
//            Demon,
//            MountainMonsters,
//            SeaMonsters,
//            PlainsMonsters,
//            Boss,
//        }

//        public enum GroundTiltType
//        {
//            None,
//            Pitch,
//            Full,
//            PitchRaycast,
//            FullRaycast,
//        }
//    }

//}
