using System.Windows.Forms;

namespace Minesweeper
{
	/// <summary>
	/// 地雷の残り数の表示を表します。
	/// </summary>
	public class MineCounter
	{
		// ----- 定数 ----- //

		/// <summary>
		/// 地雷数の表示フォーマットを取得します。
		/// </summary>
		const string labelFormat = "地雷残り{0}個";

		// ----- インスタンスフィールド ----- //

		/// <summary>
		/// 地雷数を表示するWindowsラベルコントロールを示します。
		/// </summary>
		readonly Label label;

		/// <summary>
		/// 現在の地雷表示数を取得または設定します。
		/// </summary>
		int mineCount;

		// ----- 初期化 ----- //

		/// <summary>
		/// 地雷数表示クラスの新しいインスタンスを初期化します。
		/// </summary>
		/// <param name="label">地雷の数を表示するWindowsラベルコントロールを指定してください。</param>
		public MineCounter( Label label )
		{
			this.label = label;
			UpdateLabelText();
		}

		// ----- 公開メソッド ----- //

		/// <summary>
		/// 地雷の数を指定して地雷数表示オブジェクトを初期化します。
		/// </summary>
		/// <param name="mineCount">ゲーム開始時の地雷の数を指定してください。</param>
		public void Reset( int mineCount )
		{
			this.mineCount = mineCount;
			UpdateLabelText();
		}

		/// <summary>
		/// 地雷の表示数を１つ減らします。旗を立てたときに呼び出してください。
		/// </summary>
		public void RemoveCount()
		{
			mineCount--;
			UpdateLabelText();
		}

		/// <summary>
		/// 地雷の表示数を１つ増やします。旗を撤去した際に呼び出してください。
		/// </summary>
		public void AddCount()
		{
			mineCount++;
			UpdateLabelText();
		}

		// ----- 非公開メソッド ----- //

		/// <summary>
		/// 地雷数の表示を更新します。
		/// </summary>
		void UpdateLabelText()
		{
			label.Text = string.Format( labelFormat, mineCount );
		}
	}
}
