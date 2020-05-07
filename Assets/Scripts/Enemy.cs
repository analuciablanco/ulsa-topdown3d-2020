using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField, Range(0.1f, 10f)] float moveSpeed;
    [SerializeField, Range(0f, 10f)] float minDistance;

    NavMeshAgent navMeshAgent;

    Animator anim;

    [SerializeField] GameObject weapon;

    void Awake() 
    {
        anim = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();    
    }

    void Start() {
        WeaponVisible(false);    
    }

    void Update() 
    {
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

            if(StopCombat && GameManager.instance.IsInCombat)
            {
                GameManager.instance.IsInCombat = false;
                anim.SetLayerWeight(0, 1);
                anim.SetLayerWeight(1, 0);
                GameManager.instance.StopCombat();
                WeaponVisible(false);
            }

            if(GameManager.instance.IsInCombat)
            {
                GameManager.instance.StartCombat();
                anim.SetLayerWeight(1, 1);
                WeaponVisible(true);
            }
        }
    }

    void LateUpdate() 
    {
        anim.SetBool("run", Attack);
    }

    bool StopCombat
    {
        get => !(distanceBtwPlayer <= minDistance);
    }

    public bool Attack
    {
        get => !StopCombat && distanceBtwPlayer > navMeshAgent.stoppingDistance;
    }

    float distanceBtwPlayer
    {
        get => Vector3.Distance(this.transform.position, GameManager.instance.Player.transform.position);
    }

    public void WeaponVisible(bool visible)
    {
        weapon.SetActive(visible);
    }
}
