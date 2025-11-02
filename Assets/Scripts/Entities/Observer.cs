using UnityEngine;

public abstract class Observer<T> : MonoBehaviour
{
    private bool isTargetInRange;
    private bool isHit;
    private Transform targetTransform;


    private void Update()
    {
        if (isTargetInRange && targetTransform != null && ConditionToRayCast())
        {
            HandleRayCast();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        IObserver observer = other.GetComponent<IObserver>();
        if (observer == null)
            return;
        T target = observer.GetTransform().GetComponent<T>();

        if (target != null)
        {
            Debug.Log($"[{name}]: Target is in range");
            isTargetInRange = true;
            targetTransform = observer.GetTransform();

            OnTargetInRange(target);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IObserver observer = other.GetComponent<IObserver>();
        if (observer == null)
            return;
        T target = observer.GetTransform().GetComponent<T>();

        if (target != null)
        {
            Debug.Log($"[{name}]: Target is exit range");
            isTargetInRange = false;
            targetTransform = null;

            OnTargetExitRange(target);
        }
    }

    private void HandleRayCast()
    {
        Vector3 direction = targetTransform.position - transform.position + Vector3.up;

        if (Physics.Raycast(transform.position, direction, out RaycastHit raycastHit))
        {
            T hitTarget = raycastHit.collider.GetComponent<T>();

            isHit = hitTarget != null;

            if (isHit)
                OnRayCastHit(hitTarget);
        }
    }


    /// <summary>
    /// This is condition option to execute [HandleRayCast] function
    /// </summary>
    /// <returns></returns>
    protected abstract bool ConditionToRayCast();

    /// <summary>
    /// OnTrigger Raycast hit [T] target
    /// </summary>
    /// <param name="target"></param>
    protected abstract void OnRayCastHit(T target);

    protected virtual void OnTargetInRange(T target) { } // Option override in chilren class

    protected virtual void OnTargetExitRange(T target) { } // Option override in chilren class
}
