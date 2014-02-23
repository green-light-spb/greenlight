DELIMITER //

DROP PROCEDURE IF EXISTS SwapLinesInTableConfig//

CREATE PROCEDURE SwapLinesInTableConfig(id1 int(11),id2 int(11))
BEGIN
  
  UPDATE `tableconfig` SET TableConfigID = 999999 WHERE TableConfigID = id1;
  UPDATE `tableconfig` SET TableConfigID = id1 WHERE TableConfigID = id2;
  UPDATE `tableconfig` SET TableConfigID = id2 WHERE TableConfigID = 999999;

END//
DELIMITER ;
