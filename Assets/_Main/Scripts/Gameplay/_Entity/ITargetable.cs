using UnityEngine;

public interface ITargetable
{
    public Transform transform { get; }
    public GameObject gameObject { get; }
    public bool IsTargetable { get; }
}
