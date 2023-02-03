using chessAPI.business.interfaces;
using chessAPI.dataAccess.repositores;
using chessAPI.models.game;

namespace chessAPI.business.impl;

public sealed class clsGameBusiness<TI, TC> : IGameBusiness<TI> 
    where TI : struct, IEquatable<TI>
    where TC : struct
{
    internal readonly IGameRepository<TI, TC> gameRepository;

    public clsGameBusiness(IGameRepository<TI, TC> gameRepository)
    {
        this.gameRepository = gameRepository;
    }


    public async Task<clsGame<TI>> addGame(clsNewGame newGame)
    {
        var x = await gameRepository.addGame(newGame).ConfigureAwait(false);
        return new clsGame<TI>(x, newGame.started, newGame.whites, newGame.blacks, newGame.turn, newGame.winner);
    }


    public async Task<clsGame<TI>> getGame(TI ID)
    {
        var x = await gameRepository.getGame(ID).ConfigureAwait(false);
        return new clsGame<TI>(ID, x.started, x.whites, x.blacks, x.turn, x.winner);
    }
    public async Task<clsGame<TI>> putGame(clsGame<TI> putGame)
    {
        var x = await gameRepository.putGame(putGame).ConfigureAwait(false);
        if(x == null){
            return null;
        }
        return new clsGame<TI>(x.id, x.started, x.whites, x.blacks, x.turn, x.winner);
    }
}