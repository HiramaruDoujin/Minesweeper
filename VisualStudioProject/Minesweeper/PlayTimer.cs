using System;
using System.Windows.Forms;

namespace Minesweeper
{
	/// <summary>
	/// プレイ時間の計測と表示を表します。
	/// </summary>
	public class PlayTimer
	{
		// ----- 定数 ----- //

		/// <summary>
		/// プレイ時間の最大値(秒)を取得します。
		/// </summary>
		const int maxPlayTime = 99;

		/// <summary>
		/// プレイ時間の表示フォーマットを取得します。
		/// </summary>
		const string labelFormat = "{0:00}秒";

		// ----- インスタンスフィールド ----- //

		/// <summary>
		/// ゲーム開始からの時間を表示するWindowsラベルコントロールを示します。
		/// </summary>
		readonly Label label;

		/// <summary>
		/// ゲーム開始からの時間を計測するWindowsタイマーコントロールを示します。
		/// </summary>
		readonly Timer timer;

		/// <summary>
		/// プレイ開始からの時間(秒)を取得または設定します。
		/// </summary>
		int playTime;

		// ----- 初期化 ----- //

		/// <summary>
		/// プレイ時間タイマークラスの新しいインスタンスを初期化します。
		/// </summary>
		/// <param name="label">プレイ時間を表示するWindowsラベルコントロールを指定してください。</param>
		public PlayTimer( Label label )
		{
			this.label = label;
			this.timer = new Timer();
			this.timer.Interval = 1000;
			this.timer.Tick += PlayTimerTickHandler;
			UpdateLabelText();
		}

		// ----- 公開メソッド ----- //

		/// <summary>
		/// プレイ時間の表記を初期化します。
		/// </summary>
		public void Reset()
		{
			Stop();
			playTime = 0;
			UpdateLabelText();
		}

		/// <summary>
		/// プレイ時間の計測を開始します。
		/// </summary>
		public void Start()
		{
			timer.Start();
		}

		/// <summary>
		/// プレイ時間の計測を終了します。
		/// </summary>
		public void Stop()
		{
			timer.Stop();
		}

		// ----- イベントハンドラ ----- //

		/// <summary>
		/// ゲームプレイ時に１秒ごとに呼び出されます。
		/// プレイ時間の加算を行ってプレイ時間ラベルの表示を更新します。
		/// </summary>
		void PlayTimerTickHandler( object sender, EventArgs e )
		{
			playTime = playTime < maxPlayTime
			? playTime + 1
			: maxPlayTime;

			UpdateLabelText();
		}

		// ----- 非公開メソッド ----- //

		/// <summary>
		/// プレイ時間の表示を更新します。
		/// </summary>
		void UpdateLabelText()
		{
			label.Text = string.Format( labelFormat, playTime );
		}
	}
}
