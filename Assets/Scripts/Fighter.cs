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
        Transform _target;
        float _timeSinceLastAttack = 0f;
        void Update()
        {   
            _timeSinceLastAttack += Time.deltaTime;

            if(_target == null) return;

            if (!GetIsInRange())
            {

                GetComponent<Mover>().MoveTo(_target.position);

            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttachBehaviour();
            }
        }

        private void AttachBehaviour()
        {   
            if(_timeSinceLastAttack > timeBetweenAttack){

                GetComponent<Animator>().SetTrigger("attack");
                _timeSinceLastAttack = 0f;
               
            }
            
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, _target.position) < weaponRange;
        }

        public void Attack(CombatTarget combatTarget){
            
            GetComponent<ActionSchedular>().StartAction(this);
            _target = combatTarget.transform;
        }

        public void Cancel(){
            _target = null;
        }

        // Animation Event
        void Hit(){

            Health healthComponent = _target.GetComponent<Health>();
            healthComponent.TakeDamage(weaponDamage);
        }
    }
}
