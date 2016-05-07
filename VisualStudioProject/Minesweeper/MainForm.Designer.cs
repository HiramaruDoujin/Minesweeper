namespace Minesweeper
{
	partial class MainForm
	{
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
		protected override void Dispose( bool disposing )
		{
			if( disposing && ( components != null ) )
			{
				components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.gamePanelControl = new System.Windows.Forms.Panel();
			this.resetButtonControl = new System.Windows.Forms.Button();
			this.playTimeLabelControl = new System.Windows.Forms.Label();
			this.mineCountLabelControl = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// gamePanelControl
			// 
			this.gamePanelControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gamePanelControl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.gamePanelControl.Location = new System.Drawing.Point(12, 41);
			this.gamePanelControl.Name = "gamePanelControl";
			this.gamePanelControl.Size = new System.Drawing.Size(323, 325);
			this.gamePanelControl.TabIndex = 0;
			// 
			// resetButtonControl
			// 
			this.resetButtonControl.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.resetButtonControl.Location = new System.Drawing.Point(141, 12);
			this.resetButtonControl.Name = "resetButtonControl";
			this.resetButtonControl.Size = new System.Drawing.Size(80, 23);
			this.resetButtonControl.TabIndex = 1;
			this.resetButtonControl.Text = "Reset";
			this.resetButtonControl.UseVisualStyleBackColor = true;
			// 
			// playTimeLabelControl
			// 
			this.playTimeLabelControl.Location = new System.Drawing.Point(12, 9);
			this.playTimeLabelControl.Name = "playTimeLabelControl";
			this.playTimeLabelControl.Size = new System.Drawing.Size(100, 23);
			this.playTimeLabelControl.TabIndex = 2;
			this.playTimeLabelControl.Text = "プレイ時間を表示";
			this.playTimeLabelControl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// mineCountLabelControl
			// 
			this.mineCountLabelControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.mineCountLabelControl.Location = new System.Drawing.Point(235, 9);
			this.mineCountLabelControl.Name = "mineCountLabelControl";
			this.mineCountLabelControl.Size = new System.Drawing.Size(100, 23);
			this.mineCountLabelControl.TabIndex = 3;
			this.mineCountLabelControl.Text = "地雷の個数を表示";
			this.mineCountLabelControl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(347, 378);
			this.Controls.Add(this.mineCountLabelControl);
			this.Controls.Add(this.playTimeLabelControl);
			this.Controls.Add(this.resetButtonControl);
			this.Controls.Add(this.gamePanelControl);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(363, 416);
			this.Name = "MainForm";
			this.Text = "マインスイーパー";
			this.Load += new System.EventHandler(this.MainFormLoadedHandler);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel gamePanelControl;
		private System.Windows.Forms.Button resetButtonControl;
		private System.Windows.Forms.Label playTimeLabelControl;
		private System.Windows.Forms.Label mineCountLabelControl;
	}
}

