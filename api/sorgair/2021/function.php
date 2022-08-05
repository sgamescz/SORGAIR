<?php

require "config.php";

@$_GLOBALS['db_cnx'] = mysql_connect("$db_server", "$db_user", "$db_pass") or die("Kritická chyba - nelze navázat spojení s databází");
@mysql_select_db($db_name, $_GLOBALS['db_cnx']) or die("Kritická chyba - nelze vybrat DB");
mysql_query("SET CHARACTER SET utf8");
mysql_query("SET NAMES utf8");

function osetrit_vstup() {
	foreach($_POST as $key => $value) {
		$_POST[$key] = get_magic_quotes_gpc() ? $_POST[$key] : addslashes($_POST[$key]);
	}
}

// vytiskne datum vrácené databází
function print_date ($date) {
	echo(date("j.n.Y", strtotime($date)));
}

// overi, zda je parametr cele cislo
function je_cele_cislo($cislo) {
    return ereg("^[1234567890]+$",$cislo);
}

?>