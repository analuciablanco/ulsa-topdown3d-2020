using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField, Range(0.1f, 10f)] float moveSpeed;
    [SerializeField, Range(0f, 10f)] float minDistance;

    NavMeshAgent navMeshAgent;

    [SerializeField] Animator anim;

    void Awake() {
        anim = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();    
    }

    void Update() {
        if(Attack)
        {
            if(!GameManager.instance.IsInCombat)
            {
                GameManager.instance.IsInCombat = true;
            }
            
            //transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            navMeshAgent.destination = GameManager.instance.Player.transform.position;
            transform.LookAt(GameManager.instance.Player.transform);
        }
        else 
        {
            navMeshAgent.destination = transform.position;

            if(GameManager.instance.IsInCombat)
            {
                GameManager.instance.StartCombat();
                anim.SetLayerWeight(1, 1);
            }
        }
    }

    void LateUpdate() {
        anim.SetBool("run", Attack);
    }

    public bool Attack
    {
        get => distanceBtwPlayer <= minDistance
        && distanceBtwPlayer > navMeshAgent.stoppingDistance;
    }

    float distanceBtwPlayer 
    {
        get => Vector3.Distance(this.transform.position, GameManager.instance.Player.transform.position);
    }
}
