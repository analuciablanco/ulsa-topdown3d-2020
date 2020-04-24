using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField, Range(0.1f, 10f)] float moveSpeed;
    [SerializeField, Range(0f, 10f)] float minDistance;

    void Update() {
        if(Attack)
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            transform.LookAt(GameManager.instance.Player.transform);
        }
    }

    public bool Attack
    {
        get => Vector3.Distance(this.transform.position, GameManager.instance.Player.transform.position) <= minDistance;
    }
}
