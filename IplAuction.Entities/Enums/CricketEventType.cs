namespace IplAuction.Entities.Enums;

public enum CricketEventType
{
    // Batting Events
    Run,                // Single run
    Four,               // Boundary (4 runs)
    Six,                // Six runs

    // Fielding Events
    Catch,
    Stumping,
    RunOut,

    // Bowling Events
    Wicket,
    MaidenOver
}

