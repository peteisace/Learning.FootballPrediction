SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[club_selectByName]
(
    @Name VARCHAR(50)
)
AS
BEGIN
    SELECT      id
                , name
    FROM        dbo.club
    WHERE       name = @Name
    
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[squadMember_insert]
(
    @MatchId INT
    , @MatchRole TINYINT
    , @PlayerId INT
    , @PositionId TINYINT
)
AS
BEGIN
    INSERT INTO dbo.match_squads
    (
        match_id
        , club_type
        , player_id
        , start_positionid
    )
    VALUES
    (
        @MatchId
        , @MatchRole
        , @PlayerId
        , @PositionId
    )

    SELECT CAST(SCOPE_IDENTITY() AS INT) AS match_squad_id

END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[player_insert]
(
    @FullName VARCHAR(100)
    , @DateOfBirth DATETIME
    , @NameHash INT
)
AS
BEGIN
    INSERT INTO dbo.player
    (
        full_name
        , date_of_birth
        , name_hash
    )
    VALUES
    (
        @FullName
        , @DateOfBirth
        , @NameHash
    )

    SELECT CAST(SCOPE_IDENTITY() AS INT) AS player_id

END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[match_insert]
(
    @Played DATETIME
    , @HomeTeamId INT
    , @AwayTeamId INT
)
AS
BEGIN
    INSERT INTO dbo.[match]
    (
        played
        , home_teamid
        , away_teamid
    )
    VALUES
    (
        @Played
        , @HomeTeamId
        , @AwayTeamId
    )

    SELECT CAST(SCOPE_IDENTITY() AS INT) AS match_id

END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[matchevent_insert]
(
    @MatchId INT
    , @EventType TINYINT
    , @PlayerId INT
    , @Minute TINYINT
)
AS
BEGIN
    INSERT INTO dbo.match_events
    (
        match_id
        , event_type
        , player_id
        , [minute]
    )
    VALUES
    (
        @MatchId
        , @EventType
        , @PlayerId
        , @Minute
    )
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[player_select]
(
    @PlayerId INT
)
AS
BEGIN
    SELECT      id
    ,           full_name
    ,           date_of_birth
    FROM        dbo.player
    WHERE       id = @PlayerId

END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[position_insert]
(
    @Name VARCHAR(50)
)
AS
BEGIN
    INSERT INTO dbo.[position]
    (
        name
    )
    VALUES
    (
        @Name
    )

    SELECT CAST(SCOPE_IDENTITY() AS INT) AS position_id

END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[position_selectByName]
(
    @Name VARCHAR(50)
)
AS
BEGIN
    SELECT      id
                , name
    FROM        dbo.[position]
    WHERE       name = @Name

END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[club_insert]
(
    @Name VARCHAR(50)
)
AS
BEGIN
    INSERT INTO dbo.club
    (
        name
    )
    VALUES
    (
        @Name
    )

    SELECT CAST(SCOPE_IDENTITY() AS INT) AS club_id

END
GO
