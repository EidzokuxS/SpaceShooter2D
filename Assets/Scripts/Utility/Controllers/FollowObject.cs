using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [Header("Объект для камеры")]
    [SerializeField] private Transform target;

    private void Start()
    {
        if (target == null) return;        
    }
    private void Update()
    {
        if (target == null) return;       
        transform.position = target.position;
    }
}
