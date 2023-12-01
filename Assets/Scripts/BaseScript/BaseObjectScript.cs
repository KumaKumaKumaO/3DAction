using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseObjectScript : MonoBehaviour
{
    private ObjectControllerScript _objectControllerScript = default;
    [SerializeField]
    private ColisionType _myColisionType = default;
    [SerializeField]
    private Vector3 _colisionAreaSize = default;
    protected ObjectControllerScript ObjectControllerScript { get { return _objectControllerScript; } }
    public ColisionType MyColisionType { get { return _myColisionType; } }
    public Vector3 ColisionAreaSize { get { return _colisionAreaSize; } }
    private void OnEnable()
    {
        GameObject objectControllerObj = GameObject.FindWithTag("ObjectController");
        if(objectControllerObj  == null)
        {
            Debug.LogError("ObjectControllerÇ™éÊìæÇ≈Ç´Ç‹ÇπÇÒÇ≈ÇµÇΩÅB");
        }
        _objectControllerScript = objectControllerObj.GetComponent<ObjectControllerScript>();
    }
    public bool IsColision(BaseObjectScript targetObj)
    {
        return false;
    }
}
