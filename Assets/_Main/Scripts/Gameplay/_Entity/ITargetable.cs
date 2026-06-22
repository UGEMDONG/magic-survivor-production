using UnityEngine;

public interface ITargetable
{
    public Transform transform { get; }
    public bool IsTargetable { get; }
}
