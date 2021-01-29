using DashlorisX.Properties;
using System.Drawing;

namespace DashlorisX
{
    partial class DashlorisX
    {
	/// <summary>
	/// Required designer variable.
	/// </summary>
	private System.ComponentModel.IContainer components = null;

	/// <summary>
	/// Clean up any resources being used.
	/// </summary>
	/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	protected override void Dispose(bool disposing)
	{
	    if (disposing && (components != null))
	    {
		components.Dispose();
	    }
	    base.Dispose(disposing);
	}

	#region Windows Form Designer generated code

	/// <summary>
	/// Required method for Designer support - do not modify
	/// the contents of this method with the code editor.
	/// </summary>
	private void InitializeComponent()
	{
	    this.SuspendLayout();
	    // 
	    // DashlorisX
	    // 
	    this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
	    this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
	    this.ClientSize = new System.Drawing.Size(334, 261);
	    this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
	    this.MaximizeBox = false;
	    this.MaximumSize = new System.Drawing.Size(350, 300);
	    this.MinimizeBox = false;
	    this.MinimumSize = new System.Drawing.Size(350, 300);
	    this.Name = "DashlorisX";
	    this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
	    this.Text = "Dashloris-X";
			this.BackColor = Color.FromArgb(6, 17, 33);//MidnightBlue;
		this.Icon = Resources.ICON;

		Tool.Round(this, 6);
	    this.ResumeLayout(false);

	}

	#endregion
    }
}

