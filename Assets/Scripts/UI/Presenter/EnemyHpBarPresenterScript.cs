using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class EnemyHpBarPresenterScript : MonoBehaviour
{
	[SerializeField]
	private EnemyHPBarViewScript _viewScript = default;
	public void HPBarInit(BaseCharacterScript characterScript)
	{
		_viewScript.Init(characterScript.MyTransform
			, characterScript.MyCollisionAreaData.HalfAreaSize.y * Vector3.up) ;
		characterScript.MyCharcterStatus.Hp
			.Select(value => value = value / characterScript.MyCharcterStatus.MaxHP)
			.Where(value => !float.IsNaN(value))
			.Subscribe(value => _viewScript.Display(value)).AddTo(this);

		this.UpdateAsObservable().Subscribe(_ => _viewScript.UpdatePos());
	}
}
