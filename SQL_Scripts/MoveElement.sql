DELIMITER //

DROP PROCEDURE IF EXISTS MoveElem//

CREATE PROCEDURE MoveElem(eid int(11),pid int(11))
BEGIN
  
  DECLARE e,p,l INT;
  DECLARE done INT DEFAULT 0;
  
  DECLARE cur1 CURSOR FOR SELECT ElemID,ParentID,Level FROM ref_hierarchy_regions WHERE ElemID = pid;
  
  
  DECLARE CONTINUE HANDLER FOR SQLSTATE '02000' SET done = 1;

  DELETE FROM ref_hierarchy_regions WHERE ElemID = eid;

  OPEN cur1;
  
REPEAT
  FETCH cur1 INTO e, p,l;
  IF NOT done THEN
    INSERT INTO ref_hierarchy_regions VALUES (eid,p,l+1);
  END IF;
UNTIL done END REPEAT;

  INSERT INTO ref_hierarchy_regions VALUES (eid,pid,1);
  
  CLOSE cur1;
  
  SET done = 0;      
  
END//

DROP PROCEDURE IF EXISTS MoveTree//

CREATE PROCEDURE MoveTree(eid int(11),pid int(11))
BEGIN
  DECLARE done INT DEFAULT 0;
  DECLARE e,p INT;
  DECLARE cur1 CURSOR FOR SELECT ref_hierarchy_regions.ElemID,parentl1.ParentID FROM ref_hierarchy_regions INNER JOIN ref_hierarchy_regions as parentl1 
  ON ref_hierarchy_regions.ElemID = parentl1.ElemID AND parentl1.Level = 1 WHERE ref_hierarchy_regions.ParentID = 4 ORDER BY ref_hierarchy_regions.Level;
  
  DECLARE CONTINUE HANDLER FOR SQLSTATE '02000' SET done = 1;
  
  CALL MoveElem(eid,pid);
  
  OPEN cur1;
  
  REPEAT
    FETCH cur1 INTO e, p;
    IF NOT done THEN
        CALL MoveElem(e,p);
    END IF;
  UNTIL done END REPEAT;

CLOSE cur1;

END//

DROP PROCEDURE IF EXISTS DeleteTree//

CREATE PROCEDURE DeleteTree(eid int(11))
BEGIN
    DECLARE done INT DEFAULT 0;
    DECLARE e INT(11);
    DECLARE cur1 CURSOR FOR SELECT DISTINCT ElemID FROM ref_hierarchy_regions WHERE ParentID = eid OR ElemID = eid;
    DECLARE CONTINUE HANDLER FOR SQLSTATE '02000' SET done = 1;

    DELETE FROM ref_data_regions WHERE ID IN (SELECT DISTINCT ElemID FROM ref_hierarchy_regions WHERE ParentID = eid OR ElemID = eid);

    OPEN cur1;
  
  REPEAT
     FETCH cur1 INTO e;
    IF NOT done THEN
        DELETE FROM ref_hierarchy_regions WHERE ElemID = e;
    END IF;
  UNTIL done END REPEAT;

CLOSE cur1;
    
   

END//

