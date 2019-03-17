using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Attractions
{
    public class AttractionLogic : MonoBehaviour, IAttraction
    {
        public List<KillTool> KillTools = new List<KillTool>();
        public ParticleSystem Smoke;
        public string AttractionName { get; protected set; }

        protected float Speed;

        private float _startTime = 0;
        private int _kills;
        private readonly float _maxSpeed = 60;
        private bool _broken;

        public void Start()
        {
            _startTime = Time.time;
            foreach (var killTool in KillTools)
            {
                killTool.ZombieKilled += ZombieKilled;
            }
        }

        private void ZombieKilled()
        {
            _kills += 1;
        }

        public virtual void Update()
        {
            var changedSpeed = _broken ? -Time.deltaTime * 30 : Time.deltaTime * 60;
            Speed = Mathf.Clamp(Speed + changedSpeed, 0, _maxSpeed);

            var wear = Time.time - _startTime + _kills * 1;
            if (wear > 20 && !_broken)
            {
                BreakDown();
            }

            var canHurt = Speed > 30;
            foreach (var killTool in KillTools)
            {
                killTool.CanHurt = canHurt;
            }
        }

        private void BreakDown()
        {
            _broken = true;
            Smoke.Play();
            Debug.Log("BROKEN");
        }

        public void Repair()
        {
            _kills = 0;
            _startTime = Time.time;
            _broken = false;
            Smoke.Stop();
        }

        public void Upgrade()
        {
        }
    }
}