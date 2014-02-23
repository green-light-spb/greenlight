DELIMITER //

DROP PROCEDURE IF EXISTS sp_moveelem_[RefDBName]//

CREATE PROCEDURE sp_moveelem_[RefDBName](eid int(11),pid int(11))
BEGIN
  
  DECLARE e,p,l INT;
  DECLARE done INT DEFAULT 0;
  
  DECLARE cur1 CURSOR FOR SELECT ElemID,ParentID,Level FROM ref_hierarchy_[RefDBName] WHERE ElemID = pid;
  
  
  DECLARE CONTINUE HANDLER FOR SQLSTATE '02000' SET done = 1;

  DELETE FROM ref_hierarchy_[RefDBName] WHERE ElemID = eid;

  OPEN cur1;
  
REPEAT
  FETCH cur1 INTO e, p,l;
  IF NOT done THEN
    INSERT INTO ref_hierarchy_[RefDBName] VALUES (eid,p,l+1);
  END IF;
UNTIL done END REPEAT;

  INSERT INTO ref_hierarchy_[RefDBName] VALUES (eid,pid,1);
  
  CLOSE cur1;
  
  SET done = 0;      
  
END//

DROP PROCEDURE IF EXISTS sp_movetree_[RefDBName]//

CREATE PROCEDURE sp_movetree_[RefDBName](eid int(11),pid int(11))
BEGIN
  DECLARE done INT DEFAULT 0;
  DECLARE e,p INT;
  DECLARE cur1 CURSOR FOR SELECT ref_hierarchy_[RefDBName].ElemID,parentl1.ParentID FROM ref_hierarchy_[RefDBName] INNER JOIN ref_hierarchy_[RefDBName] as parentl1 
  ON ref_hierarchy_[RefDBName].ElemID = parentl1.ElemID AND parentl1.Level = 1 WHERE ref_hierarchy_[RefDBName].ParentID = 4 ORDER BY ref_hierarchy_[RefDBName].Level;
  
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

DROP PROCEDURE IF EXISTS sp_deletetree_[RefDBName]//

CREATE PROCEDURE sp_deletetree_[RefDBName](eid int(11))
BEGIN
    DECLARE done INT DEFAULT 0;
    DECLARE e INT(11);
    DECLARE cur1 CURSOR FOR SELECT DISTINCT ElemID FROM ref_hierarchy_[RefDBName] WHERE ParentID = eid OR ElemID = eid;
    DECLARE CONTINUE HANDLER FOR SQLSTATE '02000' SET done = 1;

    DELETE FROM ref_data_[RefDBName] WHERE ID IN (SELECT DISTINCT ElemID FROM ref_hierarchy_[RefDBName] WHERE ParentID = eid OR ElemID = eid);

    OPEN cur1;
  
  REPEAT
     FETCH cur1 INTO e;
    IF NOT done THEN
        DELETE FROM ref_hierarchy_[RefDBName] WHERE ElemID = e;
    END IF;
  UNTIL done END REPEAT;

CLOSE cur1;
    
   

END//

DROP TRIGGER IF EXISTS `[RefDBName]_after_insert`;//

CREATE TRIGGER `[RefDBName]_after_insert` AFTER INSERT ON `ref_data_[RefDBName]`
FOR EACH ROW
BEGIN
   CALL MoveElem(NEW.ID,NEW.ParentID);
END//

DROP TRIGGER IF EXISTS `[RefDBName]_after_update`;//

CREATE TRIGGER `[RefDBName]_after_update` AFTER UPDATE ON `ref_data_[RefDBName]`
FOR EACH ROW
BEGIN
IF NEW.ParentID <> OLD.ParentID THEN
   CALL MoveTree(NEW.ID,NEW.ParentID);
END IF;

END//

