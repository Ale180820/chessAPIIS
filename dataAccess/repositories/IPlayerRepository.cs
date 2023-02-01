using chessAPI.dataAccess.models;
using chessAPI.models.player;

namespace chessAPI.dataAccess.repositores;

public interface IPlayerRepository<TI, TC>
        where TI : struct, IEquatable<TI>
        where TC : struct
{
    Task<TI> addPlayer(clsNewPlayer player);
    Task<IEnumerable<clsPlayerEntityModel<TI, TC>>> addPlayers(IEnumerable<clsNewPlayer> players);
    Task<IEnumerable<clsPlayerEntityModel<TI, TC>>> getPlayersByGame(TI gameId);
    Task updatePlayer(clsPlayer<TI> updatedPlayer);
    Task deletePlayer(TI id);
    Task<clsPlayerEntityModel<TI,TC>> getPlayer(TI playerID);
    Task<clsPlayerEntityModel<TI,TC>> putPlayer(clsPlayer<TI> putPlayer);
}