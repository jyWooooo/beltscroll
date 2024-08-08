using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField] private float _distantViewValue;
    [SerializeField] private bool _useRecycle;
    [SerializeField] private float _recycleThreshold;

    public void MoveNextStage()
    {
        transform.position += _distantViewValue * Time.deltaTime * Vector3.left;

        if (transform.position.x <= _recycleThreshold && _useRecycle)
            transform.position = new(-_recycleThreshold, transform.position.y);
    }
}