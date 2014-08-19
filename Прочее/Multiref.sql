delimiter $$

CREATE DEFINER=`gl_full_access`@`%` FUNCTION `isMultirefConsists`(ref_id int, multiref_string text) RETURNS bool
begin

if ref_id is null then return false; end if;

if POSITION(CONCAT('{', ref_id , '}') IN multiref_string) > 0 then return true; else return false; end if;

end$$

