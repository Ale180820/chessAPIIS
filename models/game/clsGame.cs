namespace chessAPI.models.game;

public sealed class clsGame<TI>
    where TI : struct, IEquatable<TI>
{
    public clsGame(TI id, DateTime started, int whites, int blacks, bool turn, int winner)
    {
        this.id = id;
        this.started = started;
        this.whites = whites;
        this.blacks = blacks;
        this.turn = turn;
        this.winner = winner;
    }

    public TI id { get; set; }
    public DateTime started { get; set; } = DateTime.Now;
    public int whites {get;set;}
    public int blacks {get;set;}
    public bool turn {get;set;}
    public int winner {get;set;}
}