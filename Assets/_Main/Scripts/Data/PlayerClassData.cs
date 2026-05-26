using UnityEngine;


[CreateAssetMenu(fileName = "NewPlayerClassData", menuName = "ScriptableObjects/Player Class Data")]
public class PlayerClassData : ScriptableObject
{
    [SerializeField] private string className;
    [SerializeField] private float maxHp;
    [SerializeField] private float atk;
    [SerializeField] private float defense;
    [SerializeField] private float moveSpeed;

    public string ClassName => className;
    public float MaxHp => maxHp;
    public float Atk => atk;
    public float Defense => defense;
    public float MoveSpeed => moveSpeed;
}
