using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat{

    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttack = 1f;
        [SerializeField] float weaponDamage = 5f;
        Health _target;
        float _timeSinceLastAttack = Mathf.Infinity;
        void Update()
        {   
            _timeSinceLastAttack += Time.deltaTime;

            if(_target == null) return;
            if(_target.IsDead()) return;

            if (!GetIsInRange())
            {

                GetComponent<Mover>().MoveTo(_target.transform.position, 1f);

            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttachBehaviour();
            }
        }

        private void AttachBehaviour()
        {   
            transform.LookAt(_target.transform);
            if(_timeSinceLastAttack > timeBetweenAttack)
            {
                TriggerAttack();
                _timeSinceLastAttack = 0f;

            }

        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("attack");
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, _target.transform.position) < weaponRange;
        }

        public void Attack(GameObject combatTarget){
            
            GetComponent<ActionSchedular>().StartAction(this);
            _target = combatTarget.GetComponent<Health>();
        }

        public bool CanAttack(GameObject combatTarget){
            
            if(combatTarget == null) { return false; }
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();

        }

        public void Cancel()
        {
            StopAttack();
            _target = null;
        }

        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }

        // Animation Event
        void Hit(){
            
            if(_target == null) { return; }
            _target.TakeDamage(weaponDamage);
        }
    }
}
