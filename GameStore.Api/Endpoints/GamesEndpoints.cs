using GameStore.Api.Dtos;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
    const string GetGamesEndpointName = "GetGame";

    private static readonly List<GameDto> games = [
        new (
        1,
        "Street Fighter II",
        "Fighting",
        19.99m,
        new DateOnly(1991, 2, 1)
        ),
    new (
        2,
        "Final Fantasy VII",
        "Role-playing",
        49.99m,
        new DateOnly(1997, 1, 31)
    ),
    new (
        3,
        "The Legend of Zelda: Ocarina of Time",
        "Action-adventure",
        59.99m,
        new DateOnly(1998, 11, 21)
        ),
    new (
        4,
        "Super Mario Bros.",
        "Platform",
        59.99m,
        new DateOnly(1985, 9, 13)
        ),
    new (
        5,
        "Sonic the Hedgehog",
        "Platform",
        19.99m,
        new DateOnly(1991, 6, 23)
        ),
    ];

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("Games").WithParameterValidation();

        // GET /Games
        group.MapGet("/", () => games);


        // GET /Games/{id}
        group.MapGet("/{id}", (int id) =>
        {
            GameDto? game = games.Find(g => g.Id == id);

            return game is not null ? Results.Ok(game) : Results.NotFound();
        })
            .WithName(GetGamesEndpointName);


        // POST /Games
        group.MapPost("/", (CreateGameDto newGame) =>
        {
            GameDto game = new GameDto(
                    games.Count + 1,
                    newGame.Name,
                    newGame.Genre,
                    newGame.Price,
                    newGame.ReleaseDate
                );

            // Add game to the list
            games.Add(game);

            // Return the created game
            return Results.CreatedAtRoute(GetGamesEndpointName, new { id = game.Id }, game);
        });


        // PUT /Games/{id}
        group.MapPut("/{id}", (int id, UpdateGameDto updateGame) =>
        {
            int index = games.FindIndex(g => g.Id == id);
            if (index == -1)
            {
                return Results.NotFound();
            }

            games[index] = new GameDto(
                id,
                updateGame.Name,
                updateGame.Genre,
                updateGame.Price,
                updateGame.ReleaseDate
            );

            // return Results.Ok(game);
            return Results.NoContent();
        });


        // DELETE /Games/{id}
        group.MapDelete("/{id}", (int id) =>
        {
            int index = games.FindIndex(g => g.Id == id);
            if (index == -1)
            {
                return Results.NotFound();
            }

            games.RemoveAll(g => g.Id == id);

            return Results.NoContent();
        });

        return group;
    }
}