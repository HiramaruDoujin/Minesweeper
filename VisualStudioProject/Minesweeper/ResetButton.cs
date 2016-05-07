using System;
using System.Windows.Forms;

namespace Minesweeper
{
	/// <summary>
	/// ゲームの再生成と達成状態の表示を表します。
	/// </summary>
	class ResetButton
	{
		// ----- 定数 ----- //

		/// <summary>
		/// 通常状態でのテキストを取得します。
		/// </summary>
		const string defaultText = "Reset";

		/// <summary>
		/// ゲーム失敗時のテキストを取得します。
		/// </summary>
		const string failedText = "Failed";

		/// <summary>
		/// ゲーム完遂時のテキストを取得します。
		/// </summary>
		const string completeText = "Complete";

		// ----- イベント ----- //

		/// <summary>
		/// ボタンが押された際に発生します。
		/// </summary>
		public event Action onClicked;

		// ----- インスタンスフィールド ----- //

		/// <summary>
		/// Windowsボタンコントロールを示します。
		/// </summary>
		readonly Button button;

		// ----- 初期化 ----- //

		/// <summary>
		/// リセットボタンクラスの新しいインスタンスを初期化します。
		/// </summary>
		/// <param name="button">ゲームの再生性と達成状態の表示を行うWindowsボタンコントロールを指定してください。</param>
		public ResetButton( Button button )
		{
			this.button = button;
			this.button.Click += ( sender, e ) => onClicked();
			this.button.Click += ( sender, e ) => ButtonClickedHandler();
		}

		// ----- 公開メソッド　----- //

		/// <summary>
		/// 表示テキストを失敗時のものに変更します。
		/// </summary>
		public void ShowFailed()
		{
			button.Text = failedText;
		}

		/// <summary>
		/// 表示テキストを完遂時のものに変更します。
		/// </summary>
		public void ShowComplete()
		{
			button.Text = completeText;
		}

		// ----- イベントハンドラ ----- //

		/// <summary>
		/// ボタンがクリックされた際に呼び出されます。
		/// テキストの表示を初期化します。
		/// </summary>
		void ButtonClickedHandler()
		{
			button.Text = defaultText;
		}
	}
}
