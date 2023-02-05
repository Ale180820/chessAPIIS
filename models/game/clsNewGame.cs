namespace chessAPI.models.game;

public sealed class clsNewGame
{
    public clsNewGame(){}

    public DateTime started { get; set; } = DateTime.Now;
    public int whites {get;set;}
    public bool turn {get;set;}
}