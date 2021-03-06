CREATE DATABASE fp;
GO

USE fp;
GO

CREATE LOGIN fp_api WITH PASSWORD='M@chineLearning',
DEFAULT_DATABASE = fp;
GO

CREATE USER fp_api FOR LOGIN fp_api
GO

GRANT EXECUTE TO fp_api
GO

IF EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.TABLES t WHERE t.TABLE_NAME  = 'match_events')
BEGIN
	DROP TABLE dbo.match_events 
END

IF EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.TABLES t WHERE t.TABLE_NAME  = 'match_squads')
BEGIN
	DROP TABLE dbo.match_squads 
END

IF EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.TABLES t WHERE t.TABLE_NAME  = 'match')
BEGIN
	DROP TABLE dbo.[match] 
END

IF EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.TABLES t WHERE t.TABLE_NAME  = 'player')
BEGIN
	DROP TABLE dbo.player 
END

IF EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.TABLES t WHERE t.TABLE_NAME  = 'club')
BEGIN
	DROP TABLE dbo.club
END

IF EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.TABLES t WHERE t.TABLE_NAME  = 'nationality')
BEGIN
	DROP TABLE dbo.nationality 
END

IF EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.TABLES t WHERE t.TABLE_NAME  = 'event_type')
BEGIN
	DROP TABLE dbo.event_type 
END

IF EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.TABLES t WHERE t.TABLE_NAME  = 'position')
BEGIN
	DROP TABLE dbo.[position] 
END

IF EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.TABLES t WHERE t.TABLE_NAME  = 'match_org')
BEGIN
	DROP TABLE dbo.match_org 
END

IF EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.TABLES t WHERE t.TABLE_NAME = 'measurement_type')
BEGIN
    DROP TABLE dbo.measurement_type
END

IF EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.VIEWS v WHERE v.TABLE_NAME = 'vw_match_results')
BEGIN
    DROP VIEW dbo.vw_match_results
END

IF EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.VIEWS v WHERE v.TABLE_NAME = 'vw_matchdays_results')
BEGIN
    DROP VIEW dbo.vw_matchdays_results
END

CREATE TABLE fp.dbo.measurement_type (
    id tinyint NOT NULL,
    abbreviation varchar(4) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    description varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    CONSTRAINT PK__measurement_type PRIMARY KEY (id) 
)

CREATE TABLE fp.dbo.match_org (
	id tinyint NOT NULL,
	desciption varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	CONSTRAINT PK__match_or__3213E83FAD0577EA PRIMARY KEY (id)
) 

CREATE TABLE fp.dbo.[position] (
	id tinyint IDENTITY(1,1) NOT NULL,
	name varchar(30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CONSTRAINT PK__position__3213E83F9C2C22F6 PRIMARY KEY (id)
) 

CREATE TABLE fp.dbo.event_type (
	id tinyint NOT NULL,
	description varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	CONSTRAINT PK__event_ty__3213E83F086A36BA PRIMARY KEY (id)
) 

CREATE TABLE fp.dbo.nationality (
	id int IDENTITY(1,1) NOT NULL,
	name varchar(25) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	CONSTRAINT PK__national__3213E83F99B980CA PRIMARY KEY (id)
) 

CREATE TABLE fp.dbo.club (
	id int IDENTITY(1,1) NOT NULL,
	name varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CONSTRAINT PK__club__3213E83FEF775F83 PRIMARY KEY (id),
	CONSTRAINT club_UK_name UNIQUE (name)
) 

CREATE TABLE fp.dbo.player (
	id int IDENTITY(1,1) NOT NULL,
	full_name varchar(40) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	date_of_birth datetime NOT NULL,
    nationality_id int NULL,
    height tinyint NULL,
    height_measurement_type TINYINT NULL,
    weight tinyint NULL,
    weight_measurement_type tinyint NULL,
	name_hash int NOT NULL,
	CONSTRAINT PK__player__3213E83FEB6FF950 PRIMARY KEY (id),
    CONSTRAINT FK_player_nationaity FOREIGN KEY (nationality_id) REFERENCES fp.dbo.nationality(id),
    CONSTRAINT FK_player_height_measurement_type FOREIGN KEY (height_measurement_type) REFERENCES fp.dbo.measurement_type(id),
    CONSTRAINT FK_player_weight_measurement_type FOREIGN KEY (weight_measurement_type) REFERENCES fp.dbo.measurement_type(id)
) 
CREATE UNIQUE INDEX player_UK_namehash_dob ON dbo.player (name_hash, date_of_birth)  
CREATE INDEX player_IX_namehash ON dbo.player (name_hash)  

CREATE TABLE fp.dbo.[match] (
	id int IDENTITY(1,1) NOT NULL,
	home_teamid int NOT NULL,
	away_teamid int NOT NULL,
	played datetime NOT NULL,
	CONSTRAINT PK__match__3213E83F1D98B91C PRIMARY KEY (id),
	CONSTRAINT FK_club_matchaway FOREIGN KEY (away_teamid) REFERENCES fp.dbo.club(id),
	CONSTRAINT FK_club_matchhome FOREIGN KEY (home_teamid) REFERENCES fp.dbo.club(id)
) 

CREATE TABLE [dbo].[match_squads](
	[match_id] [int] NOT NULL,
	[player_id] [int] NOT NULL,
	[club_type] [tinyint] NOT NULL,
	[start_positionid] [tinyint] NOT NULL,
	[minutes_played] [tinyint] NULL,
    [rating] [float] NOT NULL,
    [total_passes] [int] NOT NULL,
    [key_passes] [int] NOT NULL,
    [pass_accuracy] [int] NOT NULL,
    [total_shots] [int] NOT NULL,
    [on_target] [int] NOT NULL,
    [total_tackles] [int] NOT NULL,
    [blocks] [int] NOT NULL,
    [interceptions] [int] NOT NULL,
    [dribbles] [int] NOT NULL,
    [successful_dribbles] [int] NOT NULL,
    [past_dribbles] [int] NOT NULL,
    [fouls_committed] [int] NOT NULL,
    [fouls_drawn][int] NOT NULL,
	CONSTRAINT PK_match_squads PRIMARY KEY (match_id,player_id),
	CONSTRAINT FK_matchsquads_match FOREIGN KEY (match_id) REFERENCES fp.dbo.[match](id),
	CONSTRAINT FK_matchsquads_matchorg FOREIGN KEY (club_type) REFERENCES fp.dbo.match_org(id),
	CONSTRAINT FK_matchsquads_player FOREIGN KEY (player_id) REFERENCES fp.dbo.player(id),
	CONSTRAINT FK_matchsquads_position FOREIGN KEY (start_positionid) REFERENCES fp.dbo.[position](id)
) 

CREATE TABLE fp.dbo.match_events (
	id int IDENTITY(1,1) NOT NULL,
	match_id int NOT NULL,
	event_type tinyint NOT NULL,
	[minute] tinyint NULL,
	player_id int NOT NULL,
	CONSTRAINT PK__match_ev__3213E83F71AB147D PRIMARY KEY (id),
	CONSTRAINT FK_match_matchevents FOREIGN KEY (match_id) REFERENCES fp.dbo.match(id),
	CONSTRAINT FK_eventtype_matchevents FOREIGN KEY (event_type) REFERENCES fp.dbo.event_type(id),
	CONSTRAINT FK_player_matchevents FOREIGN KEY (player_id) REFERENCES fp.dbo.player(id)
) 
GO

CREATE VIEW dbo.vw_match_results
AS select CASE WHEN DATEPART(MONTH, played) BETWEEN 8 AND 12 THEN CAST(DATEPART(YEAR, played) AS VARCHAR(4)) + '-' + CAST(DATEPART(YEAR, played) + 1 AS VARCHAR(4)) ELSE CAST(DATEPART(YEAR, played) - 1 AS VARCHAR(4)) + '-' + CAST(DATEPART(YEAR, played) AS VARCHAR(4)) END AS season,
m.id, m.played, m.home_teamid, m.away_teamid, c1.name AS home_team, c2.name AS away_team, COUNT(ms1.match_id) AS home_goals_scored, COUNT(ms2.match_id) AS away_goals_scored
from match m
inner join club c1 ON m.home_teamid = c1.id
inner join club c2 on m.away_teamid = c2.id
left join match_events me1 ON m.id = me1.match_id and event_type = 1
left join match_squads ms1 ON me1.player_id = ms1.player_id AND ms1.match_id = m.id AND ms1.club_type = 1
left join match_squads ms2 ON me1.player_id = ms2.player_id AND ms2.match_id = m.id AND ms2.club_type = 2
group by CASE WHEN DATEPART(MONTH, played) BETWEEN 8 AND 12 THEN CAST(DATEPART(YEAR, played) AS VARCHAR(4)) + '-' + CAST(DATEPART(YEAR, played) + 1 AS VARCHAR(4)) ELSE CAST(DATEPART(YEAR, played) - 1 AS VARCHAR(4)) + '-' + CAST(DATEPART(YEAR, played) AS VARCHAR(4)) END, 
m.id, m.played, m.home_teamid, m.away_teamid, c1.name, c2.name;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE VIEW [dbo].[vw_matchdays_results]
AS 
    SELECT DISTINCT  c.id AS target_club_id
            , c.name AS target_club_name
            , v1.*
            , ROW_NUMBER() over (
                PARTITION BY    c.id, v1.season
                ORDER BY        c.id
                                , v1.played) AS mday
    FROM        dbo.club c
    INNER JOIN  vw_match_results v1 
        ON (c.id = v1.home_teamid OR c.id = v1.away_teamid);
   

GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_matches_with_history]
AS
    WITH some_results
    AS
    (
    SELECT      m.id AS match_id
                , [current].season
                , m.played
                , m.home_teamid
                , [current].home_team
                , m.away_teamid
                , [current].away_team
                , [current].mday AS match_day
                , [current].home_goals_scored
                , [current].away_goals_scored
                , r.target_club_id
                , prev.name AS prev_target_clubname
                , r.home_teamid AS prev_home_teamid
                , c1.name AS prev_home
                , r.away_teamid AS prev_away_teamid
                , c2.name AS prev_away
                , r.home_goals_scored AS prev_home_goals
                , r.away_goals_scored AS prev_away_goals      
                , r.mday AS prev_match_day      
                , CASE
                    WHEN r.target_club_id = r.home_teamid AND r.home_goals_scored > r.away_goals_scored THEN 'W'
                    WHEN r.target_club_id = r.away_teamid AND r.away_goals_scored > r.home_goals_scored THEN 'W'
                    WHEN r.home_goals_scored = r.away_goals_scored THEN 'D'
                    WHEN r.home_goals_scored IS NULL THEN NULL
                    ELSE 'L'
                END AS results    
                , CASE
                    WHEN r.target_club_id = r.home_teamid AND r.home_goals_scored > r.away_goals_scored THEN 3
                    WHEN r.target_club_id = r.away_teamid AND r.away_goals_scored > r.home_goals_scored THEN 3
                    WHEN r.home_goals_scored = r.away_goals_scored THEN 1
                    WHEN r.home_goals_scored IS NULL THEN NULL
                    ELSE 0
                END AS prev_points
                , r2.target_club_id AS prev2_target_clubid
                , prev2.name AS prev2_target_clubname
                , r2.home_teamid AS prev2_home_teamid
                , c2_1.name AS prev2_home
                , r2.away_teamid AS prev2_away_teamid
                , c2_2.name AS prev2_away
                , r2.home_goals_scored AS prev2_home_goals
                , r2.away_goals_scored AS prev2_away_goals      
                , r2.mday AS prev2_match_day      
                , CASE
                    WHEN r2.target_club_id = r2.home_teamid AND r2.home_goals_scored > r2.away_goals_scored THEN 'W'
                    WHEN r2.target_club_id = r2.away_teamid AND r2.away_goals_scored > r2.home_goals_scored THEN 'W'
                    WHEN r2.home_goals_scored = r2.away_goals_scored THEN 'D'
                    WHEN r2.home_goals_scored IS NULL THEN NULL
                    ELSE 'L'
                END AS results2    
                , CASE
                    WHEN r2.target_club_id = r2.home_teamid AND r2.home_goals_scored > r2.away_goals_scored THEN 3
                    WHEN r2.target_club_id = r2.away_teamid AND r2.away_goals_scored > r2.home_goals_scored THEN 3
                    WHEN r2.home_goals_scored = r2.away_goals_scored THEN 1
                    WHEN r2.home_goals_scored IS NULL THEN NULL
                    ELSE 0 
                END AS prev2_points 
                , CASE WHEN r.target_club_id = r.home_teamid THEN r.home_goals_scored ELSE r.away_goals_scored END AS prev_goals_scored
                , CASE WHEN r2.target_club_id = r2.home_teamid THEN r2.home_goals_scored ELSE r2.away_goals_scored END AS prev2_goals_scored
                , CASE WHEN r.target_club_id = r.home_teamid THEN r.away_goals_Scored ELSE r.home_goals_scored END AS prev_goals_conceded
                , CASE WHEN r2.target_club_id = r2.home_teamid THEN r2.away_goals_scored ELSE r2.home_goals_scored END AS prev2_goals_conceded                                                
    FROM        match m
    INNER JOIN  vw_matchdays_results [current]
        ON          m.id = [current].id
        AND         m.home_teamid = [current].target_club_id    
    LEFT JOIN  vw_matchdays_results r
        ON          r.target_club_id = m.home_teamid    
        AND         r.mday < [current].mday
        AND         r.season = [current].season
    LEFT JOIN   club c1 
        ON          r.home_teamid = c1.id
    LEFT JOIN   club c2 
        ON          r.away_teamid = c2.id
    LEFT JOIN   vw_matchdays_results r2
        ON          r2.target_club_id = m.away_teamid
        AND         r.mday = r2.mday
        AND         r.season = r2.season
    LEFT JOIN   club c2_1 
        ON          r2.home_teamid = c2_1.id
    LEFT JOIN   club c2_2
        ON          r2.away_teamid = c2_2.id
    LEFT JOIN   club prev
        ON          r.target_club_id = prev.id
    LEFT JOIN   club prev2
        ON          r2.target_club_id = prev2.id
    )
    SELECT      m.match_id
                , m.season
                , m.played
                , m.home_teamid
                , m.home_team
                , m.away_teamid
                , m.away_team
                , m.home_goals_scored
                , m.away_goals_scored
                , SUM(CASE 
                    WHEN m.results = 'W' THEN 1 ELSE 0 
                END) AS home_wins
                , SUM(CASE
                    WHEN m.results = 'L' THEN 1 ELSE 0
                END) AS home_losses
                , SUM(CASE
                    WHEN m.results = 'D' THEN 1 ELSE 0
                END) AS home_draws
                , SUM(CASE 
                    WHEN m.results2 = 'W' THEN 1 ELSE 0 
                END) AS away_wins
                , SUM(CASE
                    WHEN m.results2 = 'L' THEN 1 ELSE 0
                END) AS away_losses
                , SUM(CASE
                    WHEN m.results2 = 'D' THEN 1 ELSE 0
                END) AS away_draws
                , SUM(m.prev_goals_scored) AS home_goals
                , SUM(m.prev2_goals_scored) AS away_goals
                , SUM(m.prev_goals_conceded) AS home_conceded
                , SUM(m.prev2_goals_conceded) AS away_conceded     
                , MAX(m.prev_match_day) + 1 AS home_matchday
                , MAX(m.prev2_match_day) + 1 AS away_matchday
                , SUM(m.prev_points) AS home_points
                , SUM(m.prev2_points) AS away_points  
                , STRING_AGG(m.results, '') AS home_form
                , STRING_AGG(m.results2, '') AS away_form
    FROM        some_results m        
    GROUP BY
                m.match_id
                , m.season
                , m.played
                , m.home_teamid
                , m.home_team
                , m.away_teamid
                , m.away_team
                , m.home_goals_scored
                , m.away_goals_scored;                    
GO


insert into dbo.match_org VALUES(1, 'Home Team')
GO
insert into dbo.match_org VALUES(2, 'Away Team')
GO
insert into dbo.event_type VALUES(1, 'Goal Scored')
GO
insert into dbo.event_type VALUES(2, 'Penalty Converted')
GO
insert into dbo.event_type VALUES(3, 'Penalty Missed')
GO
insert into dbo.event_type VALUES(4, 'Yellow Card')
GO
insert into dbo.event_type VALUES(5, 'Red Card')
GO
insert into dbo.event_type VALUES(6, 'Substitute On')
GO
insert into dbo.event_type VALUES(7, 'Substitute Off')
GO
insert into dbo.event_type VALUES(8, 'Assist')
GO
insert into dbo.measurement_type VALUES(1, 'cm', 'Centimetres')
GO
insert into dbo.measurement_type VALUES(2, 'kg', 'Kilogrammes')
GO

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
    , @Minutes TINYINT = 0
    , @Rating FLOAT = 0
    , @TotalPasses INT = 0
    , @KeyPasses INT = 0
    , @Accuracy INT = 0
    , @TotalShots INT = 0
    , @OnTarget INT = 0
    , @TotalTackles INT = 0
    , @Blocks INT = 0
    , @Interceptions INT = 0
    , @Dribbles INT = 0
    , @SuccessfulDribbles INT = 0
    , @PastDribbles INT = 0
    , @FoulsCommitted INT = 0
    , @FoulsDrawn INT = 0
)
AS
BEGIN
    INSERT INTO dbo.match_squads
    (
        match_id
        , club_type
        , player_id
        , start_positionid
        , minutes_played
        , rating
        , total_passes
        , key_passes
        , pass_accuracy
        , total_shots
        , on_target
        , total_tackles
        , blocks
        , interceptions
        , dribbles
        , successful_dribbles
        , past_dribbles
        , fouls_committed
        , fouls_drawn
    )
    VALUES
    (
        @MatchId
        , @MatchRole
        , @PlayerId
        , @PositionId
        , @Minutes
        , @Rating
        , @TotalPasses
        , @KeyPasses
        , @Accuracy
        , @TotalShots
        , @OnTarget
        , @TotalTackles
        , @Blocks
        , @Interceptions
        , @Dribbles
        , @SuccessfulDribbles
        , @PastDribbles
        , @FoulsCommitted
        , @FoulsDrawn
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
    , @Height INT = NULL
    , @HeightType TINYINT
    , @Weight INT = NULL
    , @WeightType TINYINT
)
AS
BEGIN
    INSERT INTO dbo.player
    (
        full_name
        , date_of_birth
        , name_hash
        , height
        , height_measurement_type
        , weight
        , weight_measurement_type
    )
    VALUES
    (
        @FullName
        , @DateOfBirth
        , @NameHash
        , @Height
        , @HeightType
        , @Weight
        , @WeightType
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
