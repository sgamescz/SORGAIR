<?php

require "function.php";


 $db_query = "SELECT * from sorgair_results_contest where interniID='".$_GET['id']."'";

$dave= mysql_query($db_query) or die(mysql_error());

while($row = mysql_fetch_assoc($dave)){
  foreach($row as $cname => $cvalue){
        echo "$cvalue|";
      }
      echo "<br>";
    }

  mysql_free_result($dave);
  

  
?>