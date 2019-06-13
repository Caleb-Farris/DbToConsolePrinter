SELECT name, description 
FROM employee as em JOIN position as po 
ON em.positionid = po.positionid 
AND 
( 
  ( 
    em.name = 'Bugs Bunny' 
    AND 
    po.description = 'Top Rabbit'
  ) 
    or 
	(
    em.name = 'Daffy Duck' 
    AND 
    po.description = 'Problem Child'
  )
);