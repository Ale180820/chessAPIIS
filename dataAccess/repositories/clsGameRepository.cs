using chessAPI.dataAccess.common;
using chessAPI.dataAccess.interfaces;
using chessAPI.dataAccess.models;
using chessAPI.models.game;
using Dapper;

namespace chessAPI.dataAccess.repositores;

public sealed class clsGameRepository<TI, TC> : clsDataAccess<clsGameEntityModel<TI, TC>, TI, TC>, IGameRepository<TI, TC>
        where TI : struct, IEquatable<TI>
        where TC : struct
{
    public clsGameRepository(IRelationalContext<TC> rkm,
                               ISQLData queries,
                               ILogger<clsGameRepository<TI, TC>> logger) : base(rkm, queries, logger)
    {
    }

    public async Task<TI> addGame(clsNewGame game)
    {
        var p = new DynamicParameters();
        p.Add("STARTED", game.started);
        p.Add("WHITES", game.whites);
        p.Add("TURN", game.turn);
        return await add<TI>(p).ConfigureAwait(false);
    }

    public async Task<IEnumerable<clsGameEntityModel<TI, TC>>> addGames(IEnumerable<clsNewGame> games)
    {
        var r = new List<clsGameEntityModel<TI, TC>>(games.Count());
        foreach (var game in games)
        {
            TI gameId = await addGame(game).ConfigureAwait(false);
            r.Add(new clsGameEntityModel<TI, TC>() { id = gameId, started = game.started, whites = game.whites, turn = game.turn });
        }
        return r;
    }

     public async Task<clsGameEntityModel<TI,TC>> getGame(TI gameID)
    {
        return await getEntity(gameID).ConfigureAwait(false);
    }

    protected override DynamicParameters fieldsAsParams(clsGameEntityModel<TI, TC> entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        var p = new DynamicParameters();
        p.Add("ID", entity.id);
        p.Add("STARTED", entity.started);
        p.Add("WHITES", entity.whites);
        p.Add("BLACKS", entity.blacks);
        p.Add("TURN", entity.turn);
        p.Add("WINNER", entity.winner);
        return p;
    }

    protected override DynamicParameters keyAsParams(TI key)
    {
        var p = new DynamicParameters();
        p.Add("ID", key);
        return p;
    }

    public async Task<clsGameEntityModel<TI,TC>> putGame(clsGame<TI> game)
    {
        var p = new DynamicParameters();
        p.Add("BLACKS", game.blacks);
        p.Add("ID", game.id);
        var result = await set<clsGameEntityModel<TI,TC>>(p,null,queries.UpdateWholeEntity, null).ConfigureAwait(false);
        return result;
    }
}