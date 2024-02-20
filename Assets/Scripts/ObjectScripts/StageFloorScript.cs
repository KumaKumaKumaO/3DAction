using UnityEngine;
/// <summary>
/// 床の基本のクラス
/// </summary>
public class StageFloorScript : BaseObjectScript
{
    private void Start()
    {
        GetComponent<Renderer>().material.color = Color.black;
    }
    public virtual void OnTopCharcter(BaseCharacterScript charcterScript)
	{

	}
}
