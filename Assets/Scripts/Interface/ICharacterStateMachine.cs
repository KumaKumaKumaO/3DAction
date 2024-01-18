public interface ICharacterStateMachine{
	/// <summary>
	/// キャラクターのステートを更新する
	/// </summary>
	/// <returns>次のステート</returns>
	public BaseCharcterStateScript UpdateState();
}
