using UnityEngine;
public interface IInputCharcterAction:IDeletable
{
	/// <summary>
	/// 移動
	/// </summary>
	/// <returns>移動する方向</returns>
	Vector2 MoveInput { get; }
	/// <summary>
	/// ジャンプ
	/// </summary>
	/// <returns>ジャンプボタンが押されているか</returns>
	bool IsJump { get;}

	/// <summary>
	/// 攻撃
	/// </summary>
	/// <returns>攻撃ボタンが押されているか</returns>
	bool IsAttack { get;}

	/// <summary>
	/// 回避
	/// </summary>
	/// <returns>回避ボタンが押されているか</returns>
	bool IsEvasion { get; }

	/// <summary>
	/// 走る
	/// </summary>
	/// <returns>走るボタンが押されているか</returns>
	bool IsRun { get;}

	/// <summary>
	/// 武器切り替え
	/// </summary>
	/// <returns>どの武器を切り替え</returns>
	int ChangeWeapon();

	/// <summary>
	/// アイテムを使う
	/// </summary>
	/// <returns>どのアイテムを使うか</returns>
	int UseItem();
}
public interface IInputPlayerAction:IDeletable
{
	/// <summary>
	/// ポーズを開く
	/// </summary>
	/// <returns>ポーズボタンが押されているか</returns>
	bool IsOpenPose { get; }

	/// <summary>
	/// インベントリーを開く
	/// </summary>
	/// <returns>インベントリーボタンが押されているか</returns>
	bool IsOpenInventory { get; }
}
public interface IInputCameraControl:IDeletable
{
	Vector2 CameraMoveInput { get; }
}
public interface IInputUIContorl:IDeletable
{
	Vector2 MoveInput { get; }
	bool IsSubmit { get; }
}

