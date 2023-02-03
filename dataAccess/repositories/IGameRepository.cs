using chessAPI.dataAccess.models;
using chessAPI.models.game;

namespace chessAPI.dataAccess.repositores;

public interface IGameRepository<TI, TC>
        where TI : struct, IEquatable<TI>
        where TC : struct
{
    Task<TI> addGame(clsNewGame game);
    Task<IEnumerable<clsGameEntityModel<TI, TC>>> addGames(IEnumerable<clsNewGame> games);
    Task<clsGameEntityModel<TI,TC>> getGame(TI gameID);
    Task<clsGameEntityModel<TI,TC>> putGame(clsGame<TI> putGame);
}