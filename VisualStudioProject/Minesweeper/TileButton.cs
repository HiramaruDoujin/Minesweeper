using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace Minesweeper
{
	/// <summary>
	/// 地雷原の１区画を表します。
	/// </summary>
	class TileButton
	{
		// ----- 定数 ----- //

		/// <summary>
		/// ボタンコントロールの幅を取得します。
		/// </summary>
		public const int width = 40;

		/// <summary>
		/// ボタンコントロールの高さを取得します。
		/// </summary>
		public const int height = 40;

		/// <summary>
		/// ボタンコントロールの画面上の大きさを取得します。
		/// </summary>
		static readonly Size tileButtonSize = new Size( width, height );

		/// <summary>
		/// 未開示状態のテキストを取得します。
		/// </summary>
		const string defaultText = "";

		/// <summary>
		/// 旗を立てた際のテキストを取得します。
		/// </summary>
		const string hasFlagText = "F";

		/// <summary>
		/// 地雷のテキストを取得します。
		/// </summary>
		const string mineText = "×";

		// ----- イベント ----- //

		/// <summary>
		/// 内容が開示された際に発生します。
		/// </summary>
		public event Action<TileButton> onOpend;

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
		/// 地雷であるかどうかを示す値を取得します。
		/// </summary>
		public readonly bool isMine;

		/// <summary>
		/// 格子座標上の位置を取得します。
		/// </summary>
		public readonly Point location;

		/// <summary>
		/// Windowsボタンコントロールを示します。
		/// </summary>
		readonly Button button;

		// ----- 公開プロパティ ----- //

		/// <summary>
		/// 地雷であると予想した際の旗が立っているかどうかを示す値を取得します。
		/// </summary>
		public bool hasFlag { get; private set; }

		/// <summary>
		/// 内容が開示されているかどうかを示す値を取得します。
		/// </summary>
		public bool isOpened { get; private set; }

		/// <summary>
		/// 隣接しているタイルボタンの一覧を示します。
		/// </summary>
		public IReadOnlyList<TileButton> adjacentTileButtons { get; private set; }

		/// <summary>
		/// 隣接している地雷の数を取得します。
		/// </summary>
		public int? adjacentMineCount { get; private set; }

		// ----- 初期化 ----- //

		/// <summary>
		/// タイルボタンクラスの新しいインスタンスを初期化します。
		/// </summary>
		/// <param name="isMine">地雷である場合にTrueを指定してください。</param>
		/// <param name="location">格子座標上の位置を指定してください。</param>
		public TileButton( bool isMine, Point location, Control parentControl )
		{
			this.isMine = isMine;
			this.location = location;
			this.button = new Button();
			button.Size = tileButtonSize;
			button.Location = new Point( location.X * width, location.Y * height );
			parentControl.Controls.Add( button );
			button.MouseDown += ( sender, e ) => MouseDownHandler( e );
			button.Click += ( sender, e ) => ClickHandler();
		}

		/// <summary>
		/// 隣接しているタイルボタンの一覧を初期化します。
		/// </summary>
		/// <param name="adjacentTileButtons">隣接しているタイルボタンの一覧を示すコレクションを指定してください。</param>
		public void InitializeAdjacentTileButtons( IList<TileButton> adjacentTileButtons )
		{
			this.adjacentTileButtons = new ReadOnlyCollection<TileButton>( adjacentTileButtons );
		}

		// ----- 公開メソッド ----- //

		/// <summary>
		/// 内容の開示を試行します。
		/// 既に開示されているもしくは旗が立っている場合は開示を中止します。
		/// </summary>
		public void TryOpen()
		{
			if( hasFlag ) return;
			if( isOpened ) return;
			isOpened = true;

			if( isMine )
			{
				button.Text = mineText;
			}
			else
			{
				adjacentMineCount = adjacentTileButtons.Count( tileButton => tileButton.isMine );
				button.Text = adjacentMineCount > 0
				? adjacentMineCount.ToString()
				: defaultText;
			}

			button.Enabled = false;

			onOpend( this );
		}

		/// <summary>
		/// オブジェクトを非有効化します。
		/// ゲームが失敗した際に呼び出してください。
		/// </summary>
		public void SetDisable()
		{
			button.Enabled = false;
		}

		// ----- イベントハンドラ ----- //

		/// <summary>
		/// ボタン上でマウスボタンが押された際に呼び出されます。
		/// 地雷予想旗の状態切替を試行します。
		/// </summary>
		void MouseDownHandler( MouseEventArgs e )
		{
			if( e.Button != MouseButtons.Right ) return;
			if( isOpened ) return;
			hasFlag = !hasFlag;

			button.Text = hasFlag
			? hasFlagText
			: defaultText;

			var raisingEvent = hasFlag
			? onAddFlag
			: onRemoveFlag;
			raisingEvent.Invoke();
		}

		/// <summary>
		/// ボタンがクリックされた際に呼び出されます。
		/// 内容開示を試行します。
		/// </summary>
		void ClickHandler()
		{
			TryOpen();
		}
	}
}
