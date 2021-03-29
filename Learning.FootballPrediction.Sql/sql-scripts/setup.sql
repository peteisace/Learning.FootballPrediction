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

CREATE TABLE fp.dbo.match_squads (
	match_id int NOT NULL,
	player_id int NOT NULL,
	club_type tinyint NOT NULL,
	start_positionid tinyint NOT NULL,
    minutes_played tinyint NULL,
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
