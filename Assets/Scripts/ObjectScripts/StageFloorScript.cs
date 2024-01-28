using UnityEngine;
/// <summary>
/// °‚ÌŠî–{‚ÌƒNƒ‰ƒX
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
