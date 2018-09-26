using System.Windows.Forms;
using System.Drawing;
using System;

public class ToggleButton : Button
{
	
	private string label1; // default label
	private string label2; // toggled label
	
	public ToggleButton(string l1, string l2)
	{
		Text = l1;
		label1 = l1;
		label2 = l2;
		Click += new EventHandler(OnClick);
	}
	
	// swaps labels when button is pressed
	void OnClick(object sender, EventArgs e) {
		string s = label2;
		label2 = label1;
		label1 = s;
		Text = label1;
	}
}