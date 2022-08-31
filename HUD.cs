using Godot;

public class HUD : CanvasLayer
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private int[] highScores = new int[3] {5,15,25};

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
        updateHighScore(HighScore);
        await ToSignal(messageTimer, "timeout");

        string totals="";
        foreach (var item in highScores)
        {
            totals+=" "+item;
        }

        ShowMessage("High Scores\n" + highScores[2]+ "\n" + highScores[1] + "\n" + highScores[0]);
        await ToSignal(messageTimer, "timeout");



        var message = GetNode<Label>("Message");
        message.Text = "Make it to the afterparty!";
        message.Show();

        await ToSignal(GetTree().CreateTimer(1), "timeout");
        GetNode<Button>("StartButton").Show();
    }


    public void updateHighScore(int highscore)
    {
        for( int i = 2; i >= 0; i-- ) { 
            if (highScores[i]<highscore)
            {
                highScores[i] = highscore;
                break;
            }
        }
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

    // // Called every frame. 'delta' is the elapsed time since the previous frame.
    // public override void _Process(float delta)
    // {
    //
    // }
}
