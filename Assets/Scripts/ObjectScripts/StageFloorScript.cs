using UnityEngine;
/// <summary>
/// ���̊�{�̃N���X
/// </summary>
public class StageFloorScript : BaseObjectScript
{
    private void Start()
    {
        GetComponent<Renderer>().material.color = Color.black;
    }
    public virtual void OnTopCharcter(BaseCharcterScript charcterScript)
	{

	}
}
