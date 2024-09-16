using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Entity/Enemy")]
public class EnemyData : ScriptableObject
{
    [Header("Info")]
    [SerializeField] private string enemyName;
    [SerializeField] private GameObject enemyModel;
    [SerializeField] private string description;
    [Header("Stats")]
    [SerializeField][Range(1, 100)] private int health;
    [SerializeField][Range(0, 50)] private int flourish;
    [SerializeField][Range(0, 10)] private int shield;
    [SerializeField][Range(0, 20)] private int elan;

    public string EnemyName => enemyName;
    public GameObject EnemyModel => enemyModel;
    public int Health => health;
    public int Flourish => flourish;
    public int Shield => shield;

    public int Elan => elan;

}
