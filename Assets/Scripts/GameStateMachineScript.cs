using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ゲーム全体の状態を遷移させる
/// </summary>
public class GameStateMachineScript
{
	/// <summary>
	/// 状態を変えれる場合は変える
	/// </summary>
	public BaseGameStateScript UpdateState()
	{
        switch (SceneManager.GetActiveScene().name)
        {
			case "Title":
                {
					return new TitleGameState();
                }
			case "InGame":
                {
					return new InGameGameStateScript();
                }
			case "GameOver":
                {
					return new GameOverStateScript();
                }
        }
		Debug.LogError("存在しないシーン名です。");
		return null;
	}
}
