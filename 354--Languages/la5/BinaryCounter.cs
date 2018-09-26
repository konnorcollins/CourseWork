using System.Windows.Forms;
using System.Drawing;
using System;

public class BinaryCounter : TextBox
{
	private int count; // current count
	
	public BinaryCounter(int i)
	{
		count = i;
		Text = Convert.ToString(i, 2);
		ReadOnly = true;
	}
	
	public void OnClick(object sender, EventArgs e)
	{
		count++;
		Text = Convert.ToString(count, 2); // converts integer to binary string representation
	}
}