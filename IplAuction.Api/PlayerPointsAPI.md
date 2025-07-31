# Player Points API Documentation

This API provides endpoints for calculating player points based on match and season statistics.

## Endpoints

### 1. Get Player Points by Match

**GET** `/api/playerpoints/match/{matchId}/player/{playerId}`

Returns the points for a specific player in a specific match.

**Parameters:**

- `matchId` (int): The ID of the match
- `playerId` (int): The ID of the player

**Response:**

```json
{
  "playerId": 1,
  "playerName": "Virat Kohli",
  "imageUrl": "https://example.com/image.jpg",
  "matchId": 1,
  "seasonId": 0,
  "fours": 3,
  "sixes": 1,
  "runs": 45,
  "wickets": 0,
  "maidenOvers": 0,
  "catches": 1,
  "stumpings": 0,
  "runOuts": 0,
  "totalPoints": 125
}
```

### 2. Get Player Points by Season

**GET** `/api/playerpoints/season/{seasonId}/player/{playerId}`

Returns the aggregated points for a specific player across all matches in a season.

**Parameters:**

- `seasonId` (int): The ID of the season
- `playerId` (int): The ID of the player

**Response:** Same format as above, but with aggregated statistics across all matches.

### 3. Get Multiple Players Points by Match

**POST** `/api/playerpoints/match/{matchId}/players`

Returns points for multiple players in a specific match.

**Parameters:**

- `matchId` (int): The ID of the match
- `playerIds` (array): List of player IDs

**Request Body:**

```json
[1, 2, 3, 4]
```

**Response:**

```json
[
  {
    "playerId": 1,
    "playerName": "Virat Kohli",
    "totalPoints": 125
  },
  {
    "playerId": 2,
    "playerName": "Rohit Sharma",
    "totalPoints": 98
  }
]
```

### 4. Get Multiple Players Points by Season

**POST** `/api/playerpoints/season/{seasonId}/players`

Returns aggregated points for multiple players across all matches in a season.

**Parameters:**

- `seasonId` (int): The ID of the season
- `playerIds` (array): List of player IDs

**Request Body:**

```json
[1, 2, 3, 4]
```

**Response:** Same format as above, but with aggregated statistics across all matches.

## Points Calculation

The points are calculated based on the following scoring rules:

- **Runs**: Base points per run + bonus points
- **Fours**: 4 runs + bonus points for four
- **Sixes**: 6 runs + bonus points for six
- **Wickets**: Points per wicket
- **Catches**: Points per catch
- **Stumpings**: Points per stumping
- **Run Outs**: Points per run out
- **Maiden Overs**: Points per maiden over

The scoring rules are configurable and can be managed through the Scoring Rules API.

## Notes

- For season-based calculations, the current implementation aggregates all player match states. For proper season filtering, you may need to add a `SeasonId` to the `Match` model.
- If a player is not found, the API returns a response with "Player not found" and 0 points.
- All endpoints return appropriate HTTP status codes (200 for success, 400 for bad requests).
