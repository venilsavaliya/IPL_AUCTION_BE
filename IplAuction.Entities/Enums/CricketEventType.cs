namespace IplAuction.Entities.Enums;

public enum CricketEventType
{
    // Batting Events
    Run,                // Single run
    Four,               // Boundary (4 runs)
    Six,                // Six runs
    HalfCentury,        // Score >= 50 runs
    Century,            // Score >= 100 runs
    Duck,               // Out on 0

    // Fielding Events
    Catch,
    ThreeCatchHaul,     // 3 or more catches in match
    Stumping,
    DirectRunOut,
    AssistedRunOut,

    // Bowling Events
    Wicket,
    ThreeWicketHaul,
    FourWicketHaul,
    FiveWicketHaul,
    MaidenOver
}

