DELIMITER //

DROP PROCEDURE IF EXISTS curdemo//


CREATE PROCEDURE MoveElem(eid int(11),pid int(11), refdbname VARCHAR(45))
BEGIN
  
  DECLARE e,p,l INT;
  DECLARE done INT DEFAULT 0;
  
  DECLARE cur1 CURSOR FOR SELECT ElemID,ParentID,Level FROM refdbname WHERE ElemID = pid;
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
END