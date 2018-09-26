using System.Windows.Forms;
using System.Drawing;
using System;

public class LightSwitch : Form
{
	private ToggleButton button;
	private BinaryCounter counter;
	
	public LightSwitch(string la1, string la2, int ct)
	{
		Text = "LightSwitch";
		Size = new Size(250, 200);
		
		button = new ToggleButton(la1, la2);
		counter = new BinaryCounter(ct);

		button.Location = new Point(30, 20);
		counter.Location = new Point(30, 50);
		
		button.Click += new EventHandler(counter.OnClick);
		
		Controls.Add(button);
		Controls.Add(counter);
		CenterToScreen();
	}
	
	static public void Main()
	{
		Application.Run(new LightSwitch("Off", "On", 0));
	}
}