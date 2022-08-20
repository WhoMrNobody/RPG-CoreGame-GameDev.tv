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
        float _timeSinceLastAttack = 0f;
        void Update()
        {   
            _timeSinceLastAttack += Time.deltaTime;

            if(_target == null) return;
            if(_target.IsDead()) return;

            if (!GetIsInRange())
            {

                GetComponent<Mover>().MoveTo(_target.transform.position);

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
            if(_timeSinceLastAttack > timeBetweenAttack){

                GetComponent<Animator>().SetTrigger("attack");
                _timeSinceLastAttack = 0f;
               
            }
            
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, _target.transform.position) < weaponRange;
        }

        public void Attack(CombatTarget combatTarget){
            
            GetComponent<ActionSchedular>().StartAction(this);
            _target = combatTarget.GetComponent<Health>();
        }

        public void Cancel(){
            GetComponent<Animator>().SetTrigger("stopAttack");
            _target = null;
        }

        // Animation Event
        void Hit(){

            _target.TakeDamage(weaponDamage);
        }
    }
}
