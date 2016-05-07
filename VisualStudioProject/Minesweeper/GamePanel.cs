using System;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Minesweeper
{
	/// <summary>
	/// タイルパネルを配置するゲームのメイン部分を表します。
	/// </summary>
	class GamePanel
	{
		// ----- 定数 ----- //

		/// <summary>
		/// タイル全体に対する地雷の割合を取得します。
		/// </summary>
		const double mineRate = 0.16;

		// ----- イベント ----- //

		/// <summary>
		/// 最初のタイルボタンの内容が開示された際に発生します。
		/// </summary>
		public event Action onStartGame;

		/// <summary>
		/// 地雷を踏んでしまった際に発生します。
		/// </summary>
		public event Action onFailed;

		/// <summary>
		/// 地雷以外のパネルを全て踏破した際に呼び出されます。
		/// </summary>
		public event Action onComplete;

		/// <summary>
		/// 旗が立てられた際に発生します。
		/// </summary>
		public event Action onAddFlag;

		/// <summary>
		/// 旗が撤去された際に発生します。
		/// </summary>
		public event Action onRemoveFlag;

		// ----- インスタンスフィールド ----- //

		/// <summary>
		/// Windowsパネルコントロールを示します。
		/// </summary>
		readonly Panel panel;

		/// <summary>
		/// 現在表示されているタイルボタンの一覧を示します。
		/// </summary>
		List<TileButton> tileButtons;

		// ----- 公開プロパティ ----- //

		/// <summary>
		/// 現在のゲームでの地雷の総数を取得します。
		/// </summary>
		public int mineCount
		{
			get;
			private set;
		}

		// ----- 初期化 ----- //

		/// <summary>
		/// ゲームパネルクラスの新しいインスタンスを初期化します。
		/// </summary>
		/// <param name="panel">使用するWindowsパネルコントロールを指定してください。</param>
		public GamePanel( Panel panel )
		{
			this.panel = panel;
		}

		// ----- 公開メソッド ----- //

		/// <summary>
		/// 現在表示されているタイルボタンをすべて削除し、フォームの大きさに合わせて新しくタイルボタンを生成して配置します。
		/// </summary>
		public void ResetTileButtons()
		{
			// ゲームパネル上のコントロールをすべて削除
			panel.Controls.Clear();

			// 現在のゲームパネル上に何枚のタイルボタンを置くことができるか計算
			var tileCountX = panel.Size.Width / TileButton.width;
			var tileCountY = panel.Size.Height / TileButton.height;
			var tileCount = tileCountX * tileCountY;

			// 地雷フラグ反復子を作成
			mineCount = (int) Math.Floor( mineRate * tileCount );
			var isMineRandomizer = new Random();
			var isMineEnumerable = Enumerable.Range( 0, tileCount ).Select( i => i < mineCount ).OrderBy( _ => isMineRandomizer.Next() );

			// 座標反復子を作成
			var locationEnumerable =
			from y in Enumerable.Range( 0, tileCountY )
			from x in Enumerable.Range( 0, tileCountX )
			select new Point( x, y );

			// タイルボタン反復子を作成
			var tileButtonEnumerable = Enumerable.Zip( isMineEnumerable, locationEnumerable, ( isMine, location ) => new TileButton( isMine, location, panel ) );

			// タイルボタンを実体化して保持
			this.tileButtons = tileButtonEnumerable.ToList();

			// タイルボタンのイベントを登録
			tileButtons.ForEach( tileButton => tileButton.onOpend += TileButtonOpenedHandler );
			tileButtons.ForEach( tileButton => tileButton.onAddFlag += onAddFlag );
			tileButtons.ForEach( tileButton => tileButton.onRemoveFlag += onRemoveFlag );

			// タイルボタンに周囲のタイルボタンを登録
			foreach( var tileID in Enumerable.Range( 0, tileCount ) )
			{
				var currentTileButton = tileButtons[ tileID ];

				var adjacentTileButtons =
				new List<int>()
				{
					tileID - tileCountX - 1, tileID - tileCountX, tileID - tileCountX + 1,
					tileID              - 1,                      tileID              + 1,
					tileID + tileCountX - 1, tileID + tileCountX, tileID + tileCountX + 1,
				}
				.Where( id => 0 <= id && id < tileCount )
				.Select( id => tileButtons[ id ] )
				.Where
				(
					tileButton =>
					Math.Abs( tileButton.location.X - currentTileButton.location.X ) <= 1 &&
					Math.Abs( tileButton.location.Y - currentTileButton.location.Y ) <= 1
				)
				.ToList();

				currentTileButton.InitializeAdjacentTileButtons( adjacentTileButtons );
			}
		}

		// ----- イベントハンドラ ----- //

		/// <summary>
		/// タイルボタンの内容が開示された際に呼び出されます。
		/// 隣接した地雷があればその個数を表示します。
		/// 隣接した地雷がなければ隣接したタイルボタンをすべて開示します。
		/// 地雷もしくは開示されたボタンがこれで全てであればゲームを終了します。
		/// </summary>
		void TileButtonOpenedHandler( TileButton openedTileButton )
		{
			// 開示したタイルが地雷である場合、ゲーム失敗
			if( openedTileButton.isMine )
			{
				onFailed();
				foreach( var tileButton in tileButtons ) tileButton.SetDisable();
				return;
			}

			// 開示したタイルの数を取得
			var tileOpenedCount = tileButtons.Count( tileButton => tileButton.isOpened );

			// 開示したタイルが１つ目の場合、ゲーム開始を通知
			if( tileOpenedCount == 1 )
			{
				onStartGame();
			}

			// 地雷以外の全てのタイルを開示した場合、ゲーム完遂
			if( tileOpenedCount == tileButtons.Count - mineCount )
			{
				onComplete();
				foreach( var tileButton in tileButtons ) tileButton.SetDisable();
				return;
			}

			// 周囲に地雷が１つもない場合、周囲の全マスを開示
			if( openedTileButton.adjacentMineCount > 0 ) return;
			foreach( var tileButton in openedTileButton.adjacentTileButtons ) tileButton.TryOpen();
		}
	}
}
