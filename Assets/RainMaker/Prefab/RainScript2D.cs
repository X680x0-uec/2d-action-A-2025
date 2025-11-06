//
// Rain Maker (c) 2015 Digital Ruby, LLC
// http://www.digitalruby.com
//

using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph;

namespace DigitalRuby.RainMaker
{
    public class RainScript2D : BaseRainScript
    {
        private static readonly Color32 explosionColor = new Color32(255, 255, 255, 255);

        private float cameraMultiplier = 1.0f;
        private Bounds visibleBounds = new Bounds();
        private float yOffset;
        private float visibleWorldWidth;
        private float initialEmissionRain;
        private Vector2 initialStartSpeedRain;
        private Vector2 initialStartSizeRain;
        private Vector2 initialStartSpeedMist;
        private Vector2 initialStartSizeMist;
        private Vector2 initialStartSpeedExplosion;
        private Vector2 initialStartSizeExplosion;
        private CustomWindZone currentWindZone;

        //
        private ParticleSystem.Particle[] particles = new ParticleSystem.Particle[2048];
        //
        //private ParticleSystem ps;
        //private ParticleSystem.Particle[] moveParticle = new ParticleSystem.Particle[2048];

        [Tooltip("The starting y offset for rain and mist. This will be offset as a percentage of visible height from the top of the visible world.")]
        public float RainHeightMultiplier = 0.15f;

        [Tooltip("The total width of the rain and mist as a percentage of visible width")]
        public float RainWidthMultiplier = 1.5f;

        [Tooltip("Collision mask for the rain particles")]
        public LayerMask CollisionMask = -1;

        [Tooltip("Lifetime to assign to rain particles that have collided. 0 for instant death. This can allow the rain to penetrate a little bit beyond the collision point.")]
        [Range(0.0f, 0.5f)]
        public float CollisionLifeTimeRain = 0.0f;

        [Tooltip("Multiply the velocity of any mist colliding by this amount")]
        [Range(0.0f, 0.99f)]
        public float RainMistCollisionMultiplier = 0.75f;

        public BoxCollider2D player;
        public float WindForce;
        public float widthLimit;

        private void EmitExplosion(ref Vector3 pos)
        {
            int count = UnityEngine.Random.Range(2, 5);
            while (count != 0)
            {
                float xVelocity = UnityEngine.Random.Range(-2.0f, 2.0f) * cameraMultiplier;
                float yVelocity = UnityEngine.Random.Range(1.0f, 3.0f) * cameraMultiplier;
                float lifetime = UnityEngine.Random.Range(0.1f, 0.2f);
                float size = UnityEngine.Random.Range(0.05f, 0.1f) * cameraMultiplier;
                ParticleSystem.EmitParams param = new ParticleSystem.EmitParams();
                param.position = pos;
                param.velocity = new Vector3(xVelocity, yVelocity, 0.0f);
                param.startLifetime = lifetime;
                param.startSize = size;
                param.startColor = explosionColor;
                RainExplosionParticleSystem.Emit(param, 1);
                count--;
            }
        }

        private void TransformParticleSystem(ParticleSystem p, Vector2 initialStartSpeed, Vector2 initialStartSize)
        {
            if (p == null)
            {
                return;
            }
            if (FollowCamera)
            {
                p.transform.position = new Vector3(Camera.transform.position.x, visibleBounds.max.y + yOffset, p.transform.position.z);
            }
            else
            {
                p.transform.position = new Vector3(p.transform.position.x, visibleBounds.max.y + yOffset, p.transform.position.z);
            }
            p.transform.localScale = new Vector3(visibleWorldWidth * RainWidthMultiplier, 1.0f, 1.0f);
            var m = p.main;
            var speed = m.startSpeed;
            var size = m.startSize;
            speed.constantMin = initialStartSpeed.x * cameraMultiplier;
            speed.constantMax = initialStartSpeed.y * cameraMultiplier;
            size.constantMin = initialStartSize.x * cameraMultiplier;
            size.constantMax = initialStartSize.y * cameraMultiplier;
            m.startSpeed = speed;
            m.startSize = size;
        }

        private void CheckForCollisionsRainParticles()
        {
            int count = 0;
            bool changes = false;

            if (CollisionMask != 0)
            {
                count = RainFallParticleSystem.GetParticles(particles);
                RaycastHit2D hit;

                for (int i = 0; i < count; i++)
                {
                    Vector3 pos = particles[i].position + RainFallParticleSystem.transform.position;
                    hit = Physics2D.Raycast(pos, particles[i].velocity.normalized, particles[i].velocity.magnitude * Time.deltaTime * 1f);
                    if (hit.collider != null && ((1 << hit.collider.gameObject.layer) & CollisionMask) != 0)
                    {
                        if (CollisionLifeTimeRain == 0.0f)
                        {
                            if (hit.collider.gameObject.CompareTag("player"))
                            {
                                var wet = player.gameObject.GetComponentInParent<WetnessCounter>();
                                wet.wetness = Mathf.Min(wet.wetnessSup, wet.wetness + 1);
                                Debug.Log("濡れちゃいました");
                            }
                            particles[i].remainingLifetime = 0.0f;
                        }
                        else
                        {
                            particles[i].remainingLifetime = Mathf.Min(particles[i].remainingLifetime, UnityEngine.Random.Range(CollisionLifeTimeRain * 0.5f, CollisionLifeTimeRain * 2.0f));
                            pos += (particles[i].velocity * Time.deltaTime);
                        }
                        changes = true;
                    }
                }
            }

            if (RainExplosionParticleSystem != null)
            {
                if (count == 0)
                {
                    count = RainFallParticleSystem.GetParticles(particles);
                }
                for (int i = 0; i < count; i++)
                {
                    if (particles[i].remainingLifetime < 0.24f)
                    {
                        Vector3 pos = particles[i].position + RainFallParticleSystem.transform.position;
                        EmitExplosion(ref pos);
                    }
                }
            }
            if (changes)
            {
                RainFallParticleSystem.SetParticles(particles, count);
            }
        }

        private void CheckForCollisionsMistParticles()
        {
            if (RainMistParticleSystem == null || CollisionMask == 0)
            {
                return;
            }

            int count = RainMistParticleSystem.GetParticles(particles);
            bool changes = false;
            RaycastHit2D hit;

            for (int i = 0; i < count; i++)
            {
                Vector3 pos = particles[i].position + RainMistParticleSystem.transform.position;
                hit = Physics2D.Raycast(pos, particles[i].velocity.normalized, particles[i].velocity.magnitude * Time.deltaTime, CollisionMask);
                if (hit.collider != null)
                {
                    particles[i].velocity *= RainMistCollisionMultiplier;
                    changes = true;
                }
            }

            if (changes)
            {
                RainMistParticleSystem.SetParticles(particles, count);
            }
        }

        protected override void Start()
        {
            base.Start();
            player = GameObject.Find("Player").GetComponent<BoxCollider2D>();
            ParticleReloadFunction();
            initialEmissionRain = RainFallParticleSystem.emission.rateOverTime.constant;
            initialStartSpeedRain = new Vector2(RainFallParticleSystem.main.startSpeed.constantMin, RainFallParticleSystem.main.startSpeed.constantMax);
            initialStartSizeRain = new Vector2(RainFallParticleSystem.main.startSize.constantMin, RainFallParticleSystem.main.startSize.constantMax);

            if (RainMistParticleSystem != null)
            {
                initialStartSpeedMist = new Vector2(RainMistParticleSystem.main.startSpeed.constantMin, RainMistParticleSystem.main.startSpeed.constantMax);
                initialStartSizeMist = new Vector2(RainMistParticleSystem.main.startSize.constantMin, RainMistParticleSystem.main.startSize.constantMax);
            }

            if (RainExplosionParticleSystem != null)
            {
                initialStartSpeedExplosion = new Vector2(RainExplosionParticleSystem.main.startSpeed.constantMin, RainExplosionParticleSystem.main.startSpeed.constantMax);
                initialStartSizeExplosion = new Vector2(RainExplosionParticleSystem.main.startSize.constantMin, RainExplosionParticleSystem.main.startSize.constantMax);
            }
        }

        protected override void Update()
        {
            base.Update();

            cameraMultiplier = (Camera.orthographicSize * 0.25f);
            visibleBounds.min = Camera.main.ViewportToWorldPoint(Vector3.zero);
            visibleBounds.max = Camera.main.ViewportToWorldPoint(Vector3.one);
            visibleWorldWidth = visibleBounds.size.x;
            yOffset = (visibleBounds.max.y - visibleBounds.min.y) * RainHeightMultiplier;
            if (this.gameObject.name != "RainPrefab2D_field")
            {
                var RainVOL = RainFallParticleSystem.velocityOverLifetime;
                RainVOL.x = WindForce;
            }
            TransformParticleSystem(RainFallParticleSystem, initialStartSpeedRain, initialStartSizeRain);
            TransformParticleSystem(RainMistParticleSystem, initialStartSpeedMist, initialStartSizeMist);
            TransformParticleSystem(RainExplosionParticleSystem, initialStartSpeedExplosion, initialStartSizeExplosion);

            CheckForCollisionsRainParticles();
            CheckForCollisionsMistParticles();
        }

        protected override float RainFallEmissionRate()
        {
            return initialEmissionRain * RainIntensity;
        }

        protected override bool UseRainMistSoftParticles
        {
            get
            {
                return false;
            }
        }
        protected void ParticleReloadFunction()
        {
            if (this.gameObject.CompareTag("raindrops"))
            {
                ParticleReload(RainFallParticleSystem);
                if (RainExplosionParticleSystem != null) { ParticleReload(RainExplosionParticleSystem); }
                if (RainMistParticleSystem != null) { ParticleReload(RainMistParticleSystem); }
            }
        }
        protected void ParticleReload(ParticleSystem p)
        {
            ParticleSystem.EmissionModule emit = p.emission;
            ParticleSystem.ShapeModule shape = p.shape;
            ParticleSystem.TriggerModule trigger = p.trigger;
            PolygonCollider2D umbrella = GameObject.FindWithTag("umbrella").GetComponent<PolygonCollider2D>();
            BoxCollider2D player = GameObject.FindWithTag("player").GetComponent<BoxCollider2D>();
            trigger.SetCollider(0, umbrella);
            trigger.SetCollider(1, player);
            float rot = emit.rateOverTime.constant;
            emit.rateOverTime = new ParticleSystem.MinMaxCurve(rot * widthLimit / 18);
            float rad = shape.radius;
            shape.radius /= 18 / widthLimit;
        }
        public void SetCurrentWindZone(CustomWindZone zone)
        {
            currentWindZone = zone;
        }
        public void ClearWindZone(CustomWindZone zone)
        {
            if (currentWindZone == zone)
            {
                currentWindZone = null;
            }
        }
        void FixedUpdate()
        {
            if (currentWindZone != null)
            {
                if (currentWindZone.currentDirection.x == 1)
                {
                    this.WindForce = currentWindZone.currentStrength;
                }
                else
                {
                    this.WindForce = -currentWindZone.currentStrength;
                }
            }
            else
            {
                this.WindForce = 0;
            }
        }
    }
}