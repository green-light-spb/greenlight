DELIMITER //

DROP TRIGGER IF EXISTS `regions_after_insert`;//

CREATE TRIGGER `regions_after_insert` AFTER INSERT ON `ref_data_regions`
FOR EACH ROW
BEGIN
   CALL MoveElem(NEW.ID,NEW.ParentID);
END//

DROP TRIGGER IF EXISTS `regions_after_update`;//

CREATE TRIGGER `regions_after_update` AFTER UPDATE ON `ref_data_regions`
FOR EACH ROW
BEGIN
IF NEW.ParentID <> OLD.ParentID THEN
   CALL MoveTree(NEW.ID,NEW.ParentID);
END IF;

END// 

