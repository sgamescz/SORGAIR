<?php




/* 
Skript pro registraci lidi do databaze sorg_lidi.
Skript je umisten volne na webu, lidi se registruji sami.
Svaz lidi odkontroluje a schvali.
*/

 
require "function.php";
 $db_query = "SELECT * from sorgair_contest where category='".$_GET['cat']."' and soutezdne > NOW() - INTERVAL 2 DAY";
//echo $db_query;
 
  $db_result = mysql_query($db_query);
  
              $i=1;
  while ($zaznam = mysql_fetch_array($db_result)) {
  echo ($zaznam['jmenosouteze']."|".date('d.m.Y', strtotime($zaznam['soutezdne']))."|".$zaznam['mistosouteze']." |".$zaznam['interniID']."<br>");
   }
  mysql_free_result($db_result);
?>