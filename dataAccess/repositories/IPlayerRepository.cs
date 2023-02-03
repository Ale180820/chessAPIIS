using chessAPI.dataAccess.models;
using chessAPI.models.player;

namespace chessAPI.dataAccess.repositores;

public interface IPlayerRepository<TI, TC>
        where TI : struct, IEquatable<TI>
        where TC : struct
{
    Task<TI> addPlayer(clsNewPlayer player);
    Task<IEnumerable<clsPlayerEntityModel<TI, TC>>> addPlayers(IEnumerable<clsNewPlayer> players);
    Task<clsPlayerEntityModel<TI,TC>> getPlayer(TI playerID);
    Task<clsPlayerEntityModel<TI,TC>> putPlayer(clsPlayer<TI> putPlayer);
}