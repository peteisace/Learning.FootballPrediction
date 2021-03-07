SELECT 	m.id AS match_id
		, m.played
		, h.name AS homeTeam 
		, a.name AS awayTeam
		, p.id AS player_id
		, p.full_name
		, p.date_of_birth 
		, ms.start_positionid
		, pos.name AS position
		, mo.id  AS match_orgid
		, mo.desciption AS match_org
FROM dbo.match m
INNER JOIN dbo.match_squads  ms ON m.id  = ms.match_id 
INNER JOIN dbo.player p ON ms.player_id  = p.id 
INNER JOIN dbo.club h ON m.home_teamid = h.id 
INNER JOIN dbo.club a ON m.away_teamid = a.id
INNER JOIN dbo.[position] pos ON ms.start_positionid  = pos.id 
INNER JOIN dbo.match_org mo ON ms.club_type = mo.id 
WHERE m.id = 21

SELECT 		m.id AS match_id
			, et.description AS event_type
			, me.[minute] 
			, p.full_name AS player_name
			, pos.name  AS player_position
			, hc.name AS player_club
FROM 		[match] m
INNER JOIN	match_events me ON m.id = me.match_id
INNER JOIN  event_type et ON me.event_type = et.id 
INNER JOIN  player p ON me.player_id = p.id 
INNER JOIN  match_squads msq ON p.id  = msq.player_id AND m.id = msq.match_id 
INNER JOIN  [position] pos ON msq.start_positionid  = pos.id 
INNER JOIN  match_org mo ON msq.club_type = mo.id 
INNER JOIN  club hc ON ((mo.id  = 1 AND m.home_teamid = hc.id ) OR (mo.id = 2 AND m.away_teamid = hc.id))
WHERE 		m.id  = 21



 