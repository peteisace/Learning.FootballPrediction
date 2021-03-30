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
	name_hash int NOT NULL,
	CONSTRAINT PK__player__3213E83FEB6FF950 PRIMARY KEY (id)
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
	CONSTRAINT PK_match_squads PRIMARY KEY (match_id,player_id),
	CONSTRAINT FK_matchsquads_match FOREIGN KEY (match_id) REFERENCES fp.dbo.[match](id),
	CONSTRAINT FK_matchsquads_matchorg FOREIGN KEY (club_type) REFERENCES fp.dbo.match_org(id),
	CONSTRAINT FK_matchsquads_player FOREIGN KEY (player_id) REFERENCES fp.dbo.player(id),
	CONSTRAINT FK_matchsquads_position FOREIGN KEY (start_positionid) REFERENCES fp.dbo.[position](id)
) 

CREATE TABLE fp.dbo.match_events (
	id int IDENTITY(1,1) NOT NULL,
	event_type tinyint NOT NULL,
	[minute] tinyint NULL,
	player_id int NOT NULL,
	CONSTRAINT PK__match_ev__3213E83F71AB147D PRIMARY KEY (id),
	CONSTRAINT FK_eventtype_matchevents FOREIGN KEY (event_type) REFERENCES fp.dbo.event_type(id),
	CONSTRAINT FK_player_matchevents FOREIGN KEY (player_id) REFERENCES fp.dbo.player(id)
) 

ALTER VIEW dbo.vw_match_results
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