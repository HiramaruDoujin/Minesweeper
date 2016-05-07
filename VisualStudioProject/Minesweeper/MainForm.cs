using System;
using System.Windows.Forms;

namespace Minesweeper
{
	/// <summary>
	/// アプリケーションの実行土台となる主要フォームを表します。
	/// </summary>
	public partial class MainForm : Form
	{
		// ----- インスタンスフィールド ----- //

		/// <summary>
		/// ゲームを再生性するオブジェクトのインスタンスを示します。
		/// </summary>
		ResetButton resetButton;

		/// <summary>
		/// プレイ時間の計測と表示を行うオブジェクトのインスタンスを示します。
		/// </summary>
		PlayTimer playTimer;

		/// <summary>
		/// 地雷の残り数を表示するオブジェクトのインスタンスを示します。
		/// </summary>
		MineCounter mineCounter;

		/// <summary>
		/// タイルボタンの配置と制御を行うオブジェクトのインスタンスを示します。
		/// </summary>
		GamePanel gamePanel;

		// ----- 初期化 ----- //

		/// <summary>
		/// メインフォームのコンストラクタです。
		/// ここに処理を追加することは推奨されていません。
		/// 初期化処理を書く際はMainForm.MainForm_Loadメソッドに追記することを推奨します。
		/// </summary>
		public MainForm()
		{
			InitializeComponent();
		}

		// ----- イベントハンドラ ----- //

		/// <summary>
		/// メインフォームが読み込まれた際に呼び出されます。
		/// アプリケーション全体の初期化処理を行います。
		/// </summary>
		void MainFormLoadedHandler( object sender, EventArgs e )
		{
			// リセットボタン初期化
			resetButton = new ResetButton( resetButtonControl );
			resetButton.onClicked += ResetButtonClickedHandler;

			// プレイ時間タイマー初期化
			playTimer = new PlayTimer( playTimeLabelControl );

			// 地雷カウンター初期化
			mineCounter = new MineCounter( mineCountLabelControl );

			// ゲームパネル初期化
			gamePanel = new GamePanel( gamePanelControl );
			gamePanel.onStartGame += GamePanelStartGameHandler;
			gamePanel.onFailed += GamePanelFailedHandler;
			gamePanel.onComplete += GamePanelCompeleteHandler;
			gamePanel.onAddFlag += GamePanelAddFlagHandler;
			gamePanel.onRemoveFlag += GamePanelRemoveFlagHandler;

			// ゲーム生成
			CreateNewGame();
		}

		/// <summary>
		/// リセットボタンがクリックされた際に呼び出されます。
		/// ゲームを再生成します。
		/// </summary>
		void ResetButtonClickedHandler()
		{
			CreateNewGame();
		}

		/// <summary>
		/// ゲームパネル上でゲームが開始された際に呼び出されます。
		/// プレイ時間の計測を開始します。
		/// </summary>
		void GamePanelStartGameHandler()
		{
			playTimer.Start();
		}

		/// <summary>
		/// ゲームパネル上でゲームプレイが失敗した際に呼び出されます。
		/// プレイ時間の計測終了と失敗結果の表示を行います。
		/// </summary>
		void GamePanelFailedHandler()
		{
			playTimer.Stop();
			resetButton.ShowFailed();
		}

		/// <summary>
		/// ゲームパネル上でゲームを完遂した際に呼び出されます。
		/// プレイ時間の計測終了と成功結果の表示を行います。
		/// </summary>
		void GamePanelCompeleteHandler()
		{
			playTimer.Stop();
			mineCounter.Reset( 0 );
			resetButton.ShowComplete();
		}

		/// <summary>
		/// ゲームパネル上で旗が建てられた際に呼び出されます。
		/// 地雷の表示数を１つ減らします。
		/// </summary>
		void GamePanelAddFlagHandler()
		{
			mineCounter.RemoveCount();
		}

		/// <summary>
		/// ゲームパネル上で旗が撤去された際に呼び出されます。
		/// 地雷の表示数を１つ増やします。
		/// </summary>
		void GamePanelRemoveFlagHandler()
		{
			mineCounter.AddCount();
		}

		// ----- 非公開メソッド ----- //

		/// <summary>
		/// 現在のゲームを破棄してゲームを新規生成します。
		/// </summary>
		void CreateNewGame()
		{
			playTimer.Reset();
			gamePanel.ResetTileButtons();
			mineCounter.Reset( gamePanel.mineCount );
		}
	}
}
