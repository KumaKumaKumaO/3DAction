public interface IErrorManager
{
	/// <summary>
	/// 自身のインスタンスを生成
	/// </summary>
	public void InstantiationMyInstance();
	/// <summary>
	/// 自身のインスタンスを削除
	/// </summary>
	public void DeleteMyInstance();
	/// <summary>
	/// シングルトンなのに二つ目をインスタンスしていたらエラー
	/// </summary>
	/// <param name="className">クラスの名前</param>
	public void SingleTonError(string className);
	/// <summary>
	/// スクリプトが存在しなかった時のエラー
	/// </summary>
	/// <param name="className">存在しなかったクラスの名前</param>
	public void NullScriptError(string className);
	/// <summary>
	/// オブジェクトが取得できなかった場合のエラー
	/// </summary>
	/// <param name="objectName">存在しなかったオブジェクトの名前</param>
	public void NullGameObjectError(string objectName);
	/// <summary>
	/// シーン名が存在しなかった時のエラー
	/// </summary>
	/// <param name="sceneName">存在しなかったシーン名</param>
	public void NullSceneNameError(string sceneName);
	/// <summary>
	/// 存在しないはずのオブジェクトが見つかった場合のエラー
	/// </summary>
	/// <param name="objName">存在しないはずのオブジェクト名</param>
	public void CantExistObject(string objName);
	/// <summary>
	/// コンポーネントが取得できなかった場合のエラー
	/// </summary>
	/// <param name="compornentName">取得できなかったコンポーネント名</param>
	public void NullCompornentError(string compornentName);
	/// <summary>
	/// オーバーフローした場合のエラー
	/// </summary>
	/// <param name="className">オーバーフローが起きたクラス</param>
	public void OverFlow(string className,int i);
}
