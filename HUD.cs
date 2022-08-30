using Godot;

public class HUD : CanvasLayer
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    [Signal]
    public delegate void StartGame();

    public void ShowMessage(string text)
    {
        var message = GetNode<Label>("Message");
        message.Text = text;
        message.Show();

        GetNode<Timer>("MessageTimer").Start();
    }

    async public void ShowGameOver(int HighScore)
    {
        ShowMessage("Game Over");

        var messageTimer = GetNode<Timer>("MessageTimer");
        await ToSignal(messageTimer, "timeout");

        ShowMessage("Your highscore: " + HighScore);

        await ToSignal(messageTimer, "timeout");


        var message = GetNode<Label>("Message");
        message.Text = "Dodge the\nCreeps!";
        message.Show();

        await ToSignal(GetTree().CreateTimer(1), "timeout");
        GetNode<Button>("StartButton").Show();
    }

    public void UpdateScore(int score)
    {
        GetNode<Label>("ScoreLabel").Text = score.ToString();
    }

    public void OnStartButtonPressed()
    {
        GetNode<Button>("StartButton").Hide();
        EmitSignal("StartGame");
    }

    public void OnMessageTimerTimeout()
    {
        GetNode<Label>("Message").Hide();
    }


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
